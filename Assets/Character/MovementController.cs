using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;
    public float gravity = -20f;
    public float verticalSpeed = 3;
    public float jumpSpeed = 15f;

    private CharacterController characterController;
    private Animator animator;
    public Camera playerFollowCamera;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInParent<Animator>();

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
        playerFollowCamera.transform.position = characterController.transform.position + new Vector3(6, 4, 0);
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement == Vector3.zero)
        {
            animator.SetFloat(Speed, 0f);
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat(Speed, 0.5f);
        }
        else
        {
            animator.SetFloat(Speed, 1f);
        }

        movement = transform.TransformDirection(movement);
        characterController.SimpleMove(movement * speed);

        var jump = Input.GetKey(KeyCode.Space);
        var moveVelocity = transform.forward * (verticalSpeed * vertical);
        if (jump)
        {
            moveVelocity.y = jumpSpeed;
            animator.SetBool(IsJumping, true);
        }
        else
        {
            animator.SetBool(IsJumping, false);
        }

        moveVelocity.y += gravity * Time.deltaTime;


        characterController.Move(moveVelocity * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        var mouseX = Input.GetAxis("Mouse X") * sensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player around the Y-axis
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera around the X-axis
        var rotationX = playerFollowCamera.transform.rotation.eulerAngles.x - mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit the camera's vertical rotation

        playerFollowCamera.transform.rotation = characterController.transform.rotation;
    }
}