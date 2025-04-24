using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput", order = 0)]
public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
{
    [SerializeField] private LayerMask whatIsGround;

    public event Action OnAttackPressed;

    public Vector2 MovementKey { get; private set; }
    private Controls _controls;

    private Vector3 _worldPos;//월드좌표w
    private Vector2 _screenPos;//화면좌표

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this); //자신(this)을 콜백하는 객체의 대상으로 설정한다
        }
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementKey = context.ReadValue<Vector2>();
        MovementKey = movementKey;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnAttackPressed?.Invoke();
    }

    public Vector3 GetWorldPosition()
    {
        Camera mainCam = Camera.main;
        Debug.Assert(mainCam != null, "No main Camera in this Scene");

        Ray cameraRay = mainCam.ScreenPointToRay(_screenPos);
        if (Physics.Raycast(cameraRay, out RaycastHit hit, mainCam.farClipPlane, whatIsGround))
        {
            _worldPos = hit.point;
        }

        return _worldPos;
    }
}