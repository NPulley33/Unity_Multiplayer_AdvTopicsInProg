using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClientPlayerMove : NetworkBehaviour
{
    //tutorial: https://www.youtube.com/watch?v=kVt0I6zZsf0&list=WL&index=5

    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerActions playerActions;
    //[SerializeField] private PlayerData playerData;

    private void Awake()
    {
        inputHandler.enabled = false;
        playerInput.enabled = false;
        playerActions.enabled = false;
        //playerData.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            inputHandler.enabled = true;
            playerInput.enabled = true;
            //playerData.enabled = true;
        }

        if (!IsLocalPlayer)
        { 
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            gameObject.GetComponentInChildren<AudioListener>().enabled = false;
        }

        if (IsServer)
        { 
            playerActions.enabled = true;
        }
    }

    [Rpc(SendTo.Server)]
    private void UpdateInputServerRpc(Vector2 move, Vector2 look, bool jump, bool sprint, bool mainAction)
    {
        inputHandler.MoveInput(move);
        inputHandler.LookInput(look);
        inputHandler.JumpInput(jump);
        inputHandler.SprintInput(sprint);
        inputHandler.MainActionInput(mainAction);
    }

    private void LateUpdate()
    {
        if (!IsOwner) return;

        UpdateInputServerRpc(inputHandler.move, inputHandler.look, inputHandler.jump, inputHandler.sprint, inputHandler.mainAction);
    }
}
