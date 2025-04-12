using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float moveSpeed = 8f, gravity = -9.81f;
    [SerializeField] private CharacterController characterController;



    public float _sprintValue = 1f; //<-- 나중에 바꿔야함 (달리기나 웅크리기 등)




    public bool CanManualMovement { get; set; } = true;

    public bool IsGround => characterController.isGrounded;

    private Vector3 _velocity;
    public Vector3 Velocity => _velocity;

    private float _verticalVelocity;
    private Vector3 _movementDirection;

    private Vector3 _autoMovement;

    private Entity _entity;

    public void SetMovementDirection(Vector2 movementInput)
    {
        _movementDirection = new Vector3(movementInput.x , 0, movementInput.y).normalized;
    }

    private void FixedUpdate()
    {
        CalculateMovement();
        ApplyGravity();
        Move();
    }

    private void CalculateMovement()
    {
        Vector3 moveDir = (_entity.transform.right * _movementDirection.x + _entity.transform.forward * _movementDirection.z).normalized;

        _velocity = moveDir * moveSpeed * _sprintValue * Time.fixedDeltaTime;
    }


    private void Move()
    {
        characterController.Move(_velocity);
    }

    public void StopImmediattely()
    {
        _movementDirection = Vector3.zero;
    }

    private void ApplyGravity()
    {
        if (IsGround && _verticalVelocity < 0)
            _verticalVelocity = -0.03f;
        else
            _verticalVelocity += gravity * Time.fixedDeltaTime;

        _velocity.y = _verticalVelocity;
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }
}