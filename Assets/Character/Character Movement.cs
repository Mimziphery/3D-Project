using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;

    private CharacterController characterController;
    private Camera playerCamera;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;

        // Lock cursor for better camera control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Player Movement
        MovePlayer();

        // Player Look
        RotatePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement = transform.TransformDirection(movement);
        characterController.SimpleMove(movement * speed);
    }

    private void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player around the Y-axis
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera around the X-axis
        float rotationX = playerCamera.transform.rotation.eulerAngles.x - mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit the camera's vertical rotation

        playerCamera.transform.rotation = Quaternion.Euler(rotationX, playerCamera.transform.rotation.eulerAngles.y, 0f);
    }
}