using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //singleton pattern
    public static Player Instance{ get; private set; }


    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    
    private bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one Player, cannot use Singleton Pattern");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;

    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact();
        }
       
        
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDir  != Vector3.zero)
        {
            //store to use for raycast function.
            //moveDir is a zero vector when the player does not move.
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        
        //Player cannot interact with object without moving if we use moveDir in raycast
        bool canInteract = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance,countersLayerMask);
        if (canInteract)
        {
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if(clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);

                }
            }
            else
            {
                SetSelectedCounter(null);


            }
        }
        else
        {
            SetSelectedCounter(null);

        }

    }

    private void HandleMovement()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        //use CapsuleCast instead of Raycast for better results
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                                            playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            // Cannot move towards moveDir

            //Check only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);

            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                                           playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //Can only move on X axis
                moveDir = moveDirX;

            }
            else
            {
                //Cannot move along the X axis
                //Check only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);

                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                                               playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    //Can only move on Z axis
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in any direction
                }
            }
        }
        if (canMove)
        {
            //player does not collide with any other objects
            gameObject.transform.position += moveDir * moveSpeed * Time.deltaTime;
        }


        isWalking = moveDir != Vector3.zero;

        //to rotate player, use Slerp to smooth rotation
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, -moveDir, Time.deltaTime * rotateSpeed);




    }


    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });

    }
    

    
}
