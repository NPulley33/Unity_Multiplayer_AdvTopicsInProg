using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float speedMultiplyer = 1.5f;
    [SerializeField] private float lookSpeed = 5f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float lookSpeedMultiplier = 1f;

    private CharacterController controller;

    private Vector3 velocity;
    private Vector3 movement = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float xRotation;
    private Camera cam;

    private bool isSprinting;
    private bool escapeToggled;

    public GameObject projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
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
        if (isSprinting) moveDirection *= speedMultiplyer;
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

    public void IsSprinting(bool isSprinting) => this.isSprinting = isSprinting;

    private void HandleRotation()
    {
        //turn player model ONLY
        float lookX = rotation.x * lookSpeed * lookSpeedMultiplier * Time.fixedDeltaTime;
        float lookY = rotation.y * lookSpeed * lookSpeedMultiplier * Time.fixedDeltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, lookX, 0f));
        this.transform.rotation *= deltaRotation;
    }

    public void UpdateRotationInput(Vector2 input)
    {
        rotation = input;   
    }

    public void ExecuteMainAction()
    {
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = this.transform.position + this.cam.transform.forward; //add forward just to super prevent collision issues
        projectile.transform.rotation = this.transform.rotation;
    }

    public void Excape()
    {
        escapeToggled = !escapeToggled;
        if(escapeToggled) Cursor.lockState = CursorLockMode.Locked;
    }
}
