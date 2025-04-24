using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInputSO plrInput;
    [SerializeField] private CharacterController characterController;

    [Header("Player Setting")]
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;

    private void Update()
    {
        Move();
        CheckAnim();
    }

    private void Move()
    {
        movement = plrInput.MovementKey;

        Vector3 moveVector = new Vector3(movement.x, 0f, 0f);
        characterController.Move(moveVector * moveSpeed * Time.deltaTime);
    }

    private void CheckAnim()
    {
        float moveAmount = movement.x;
        animator.SetFloat("Move", moveAmount);
    }
}
