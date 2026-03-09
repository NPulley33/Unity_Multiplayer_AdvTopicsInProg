using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerActions actions;

    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool mainAction;
    public bool secondAction;
    public bool escape;

    private void Start()
    {
        actions = gameObject.GetComponent<PlayerActions>();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        MoveInput(value.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        LookInput(value.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        JumpInput(value.performed);
    }

    public void OnMainAction(InputAction.CallbackContext value)
    {
        MainActionInput(value.performed);
    }

    public void OnSecondaryAction(InputAction.CallbackContext value)
    { 
        
    }

    public void OnSprint(InputAction.CallbackContext value)
    {
        SprintInput(value.performed);
    }

    public void OnEscape(InputAction.CallbackContext value)
    { 
        EscapeInput(value.started);
    }



    public void MoveInput(Vector2 input)
    { 
        move = input;
        if (actions.enabled) actions.UpdateMovementInput(move);
    }

    public void LookInput(Vector2 input)
    { 
        look = input;
        if (actions.enabled) actions.UpdateRotationInput(look);
    }

    public void JumpInput(bool input)
    { 
        jump = input;
        if (jump && actions.enabled)
        {
            actions.DoJump();
        }
    }

    public void SprintInput(bool input)
    { 
        sprint = input;
        if (actions.enabled) actions.IsSprinting(sprint);
    }

    public void MainActionInput(bool input)
    { 
        mainAction = input;
        if (actions.enabled && mainAction) actions.ExecuteMainAction();
    }

    public void EscapeInput(bool input)
    {
        escape = input;
        if (actions.enabled && escape) actions.Escape();
    }

}
