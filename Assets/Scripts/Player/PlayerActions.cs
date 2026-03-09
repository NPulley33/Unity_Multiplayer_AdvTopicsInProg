using System;
using Unity.Netcode;
using UnityEditor.ShaderGraph.Internal;
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
    private bool canMove = true;

    public GameObject projectilePrefab;
    /// <summary>
    /// time to shoot a projectile (actual 1/shootTime)
    /// </summary>
    [SerializeField] private float shootTime = 4f;
    private float nextTimeToFire;
    [SerializeField] private bool canFire;


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

        if (Time.time > nextTimeToFire)
        {
            canFire = true;
        }
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
        if (!canFire) return;

        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = this.cam.transform.position + this.transform.forward; //add forward just to super prevent collision issues
        projectile.transform.rotation = this.cam.transform.rotation;
        projectile.GetComponent<NetworkObject>().Spawn(true);

        canFire = false;
        nextTimeToFire = Time.time + 1/shootTime;
    }

    public void Escape()
    {
        Debug.Log("reached");

        escapeToggled = !escapeToggled;
        if (escapeToggled)
        { 
            Cursor.lockState = CursorLockMode.None;
        }
        else Cursor.lockState = CursorLockMode.Locked;
        
        FindFirstObjectByType<NetworkManagerUI>().ToggleLeaveSession(escapeToggled);
    }

    public void ToggleMove(bool canMove) => this.canMove = canMove;
    //TODO add toggle cursor for death
}
