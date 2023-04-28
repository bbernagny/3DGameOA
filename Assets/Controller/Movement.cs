using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Vector2 moveValue;
    public float moveSpeed = 10f;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Moving(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            //Debug.Log("performed");
            moveValue = value.ReadValue<Vector2>();
            //Debug.Log(moveValue.x + " " + moveValue.y);
            //_rigidbody.AddForce(new Vector3(moveValue.x * moveSpeed, 0f, moveValue.y * moveSpeed), ForceMode.Impulse);
        }

        if (value.canceled)
        {
            moveValue = value.ReadValue<Vector2>();
        }
    }

}
