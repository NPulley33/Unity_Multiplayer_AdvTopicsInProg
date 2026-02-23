using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float speedMultiplyer = 1.5f;
    [SerializeField] private float lookSpeed = 5f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;

    private Vector3 velocity;
    private Vector3 movement = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float xRotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        velocity.y += gravity * Time.deltaTime;

        //apply movement based on current player rotation
        Vector3 moveDirection = (transform.forward * movement.z) + (transform.right * movement.x);
        controller.Move(((moveDirection * moveSpeed) + velocity) * Time.deltaTime);
    }

    public void DoJump()
    {
        if (controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }

    public void UpdateMovementInput(Vector2 input)
    {
        movement = new Vector3(input.x, 0, input.y);
    }

    private void HandleRotation()
    {
        //turn player model ONLY
        float lookX = rotation.x * lookSpeed * Time.fixedDeltaTime;
        float lookY = rotation.y * lookSpeed * Time.fixedDeltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        GetComponentInChildren<Camera>().transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //this.transform.Rotate(0f, lookX, 0f);
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, lookX, 0f));
        transform.rotation *= deltaRotation;
    }

    public void UpdateRotationInput(Vector2 input)
    {
        rotation = input;   
    }
}
