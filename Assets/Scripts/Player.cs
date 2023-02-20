using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    
    private bool isWalking; 
    private void Update()
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

    public bool IsWalking()
    {
        return isWalking;
    }
}
