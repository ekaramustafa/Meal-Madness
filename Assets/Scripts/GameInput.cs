using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    //publisher
    public event EventHandler OnInteractAction;


    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        //subscribe to input event 
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
          OnInteractAction?.Invoke(this, EventArgs.Empty);
     
    }

    // Start is called before the first frame update
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); ;
        //diagonal movement
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
