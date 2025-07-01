using UnityEngine;

public class MourseRotation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private Transform playerBody;

    [Header("Input")]
    [SerializeField] private PlayerInputSO inputSO;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 lookInput = inputSO.LookDelta;

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
