using UnityEngine;

public class cameraController : MonoBehaviour
{
    float mouseSensitivity = 100.0f;
    public Transform playerBody;
    float xRotation = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        bool crouch = Input.GetKey(KeyCode.LeftControl); 
        float headVal = 0.62f;
        if (crouch) headVal = 0.1f;
        transform.localPosition = new Vector3(0, Mathf.Lerp(transform.localPosition.y, headVal, 5 * Time.deltaTime), 0);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
