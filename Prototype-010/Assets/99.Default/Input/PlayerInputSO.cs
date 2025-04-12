using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO")]
public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
{
    public event Action OnJumpPressed;
    public event Action OnCrouchPressed;
    public event Action OnInteractPressed;

    public event Action<bool> OnAimInteracted;
    public event Action OnTriggerPressed;
    public event Action OnCockingPressed;
    public event Action OnChangemagPressed;

    public event Action OnSprintPressed;

    public Vector2 MovementKey { get; private set; }
    public Vector2 LookDelta { get; private set; }

    private Controls _controls;
    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }
    public void OnMove(InputAction.CallbackContext context)     //�̵�
    {
        Vector2 movementKey = context.ReadValue<Vector2>();
        MovementKey = movementKey;
    }
    public void OnLook(InputAction.CallbackContext context)     //ī�޶� ȸ��
    {
        Vector2 CameraMovementKey = context.ReadValue<Vector2>();
        LookDelta = CameraMovementKey;
    }
    public void OnSprint(InputAction.CallbackContext context)   //�޸���
    {
        if (context.performed)
            OnSprintPressed?.Invoke();
    }

    public void OnCrouch(InputAction.CallbackContext context)   //��ũ����
    {
        if (context.performed)
            OnCrouchPressed?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context) //��ȣ�ۿ�
    {
        if (context.performed)
            OnInteractPressed?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)     //����
    {
        if (context.performed)
            OnJumpPressed?.Invoke();
    }
    public void OnTrigger(InputAction.CallbackContext context)  //�߻�
    {
        if (context.performed)
            OnTriggerPressed?.Invoke();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnAimInteracted?.Invoke(true);
        if(context.canceled)
            OnAimInteracted?.Invoke(false);
    }

    public void OnCocking(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnCockingPressed?.Invoke();
    }

    public void OnChangeMag(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnChangemagPressed?.Invoke();
    }
}
