using System;
using System.Security.Claims;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private PlayerInputSO playerInput;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float mouseSensitivity = 50f;
    [SerializeField] private float maxAngle = 80f;

    [SerializeField] private float aimMultipleVal;
    [SerializeField] private bool canAim;

    private float _xRotation = 0f;
    private float aimValue = 1f;
    public void HandleAim(bool value)
    {
        if (!canAim)
        {
            aimValue = 1f;
        }
        aimValue = value ? aimMultipleVal : 1f;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector2 lookDelta = playerInput.LookDelta;

        float mouseX = lookDelta.x * mouseSensitivity * Time.deltaTime * aimValue;
        float mouseY = lookDelta.y * mouseSensitivity * Time.deltaTime * aimValue;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -maxAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
