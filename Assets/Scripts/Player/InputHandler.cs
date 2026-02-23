using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerActions actions;

    private void Start()
    {
        actions = GetComponent<PlayerActions>();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        actions.UpdateMovementInput(value.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext value)
    { 
        actions.UpdateRotationInput(value.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        actions.DoJump();
    }

    public void OnMainAction(InputAction.CallbackContext value)
    { 
        
    }

    public void OnSecondaryAction(InputAction.CallbackContext value)
    { 
        
    }

}
