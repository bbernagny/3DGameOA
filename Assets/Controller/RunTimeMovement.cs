using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class RunTimeMovement : MonoBehaviour
{
    private Movement _input; //Inputu Movement scriptinde aldığımız için Movement diyoruz.
    private CharacterController _controller;
    private Animator animator;
    [SerializeField] private float fraction;

    private void Start()
    {
        _input = GetComponent<Movement>();
        _controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _controller.Move(new Vector3((_input.moveValue.x * _input.moveSpeed)/fraction, 0f, (_input.moveValue.y * _input.moveSpeed)/fraction));
        animator.SetFloat("speed", Mathf.Abs(_controller.velocity.x) + Mathf.Abs(_controller.velocity.z));
        Debug.Log(_controller.velocity.x +" "+ _controller.velocity.z);
    }

}
 