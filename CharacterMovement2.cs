using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    public Animator animator;
    public Transform cameraTarget;
    public float cameraDistance = 5f;
    public float cameraHeight = 2f;
    public float cameraRotationDamping = 10f;
    public Camera mainCamera;
    public Camera alternateCamera;

    private Camera currentCamera;

    void Start()
    {
        // Initialize the current camera to be the main camera
        currentCamera = mainCamera;
    }

    void Update()
    {
        // Check for camera switching input
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentCamera == mainCamera)
            {
                currentCamera = alternateCamera;
            }
            else
            {
                currentCamera = mainCamera;
            }
        }

        // Get input for movement
        float horizontalInput = 0f;
        float verticalInput = 0f;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                // Left side of screen controls movement
                horizontalInput = -touch.deltaPosition.x;
                verticalInput = touch.deltaPosition.y;
            }
            else
            {
                // Right side of screen controls rotation
                float yaw = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                transform.Rotate(0f, yaw, 0f);
            }
        }
        else
        {
            // Use keyboard/gamepad input for movement and rotation
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            float yaw = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, yaw, 0f);
        }

        // Calculate movement
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // Move the character
        transform.Translate(movement, Space.Self);

        // Update animation
        if (animator != null)
        {
            animator.SetFloat("Speed", movement.magnitude);
        }

        // Update camera position and rotation
        if (cameraTarget != null && currentCamera != null)
        {
            // Calculate camera position
            Vector3 cameraPosition = cameraTarget.position - transform.forward * cameraDistance + Vector3.up * cameraHeight;
            currentCamera.transform.position = Vector3.Lerp(currentCamera.transform.position, cameraPosition, Time.deltaTime * cameraRotationDamping);

            // Calculate camera rotation
            Quaternion cameraRotation = Quaternion.LookRotation(cameraTarget.position - currentCamera.transform.position, Vector3.up);
            currentCamera.transform.rotation = Quaternion.Slerp(currentCamera.transform.rotation, cameraRotation, Time.deltaTime * cameraRotationDamping);
        }
    }
}
