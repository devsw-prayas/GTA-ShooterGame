using UnityEngine;

public class FreeFlyController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform; // Assign your Main Camera here

    private float verticalRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player (left/right)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera (up/down)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        float moveY = 0f;

        if (Input.GetKey(KeyCode.E)) moveY += 1f; // Up
        if (Input.GetKey(KeyCode.Q)) moveY -= 1f; // Down

        Vector3 move = transform.right * moveX + transform.forward * moveZ + transform.up * moveY;
        transform.position += move * movementSpeed * Time.deltaTime;
    }
}
