using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] PlayerInputAction controls;
    [SerializeField] AnimationController animationController;
    private Vector2 direction;
    private Rigidbody rb;
    private int speed =5;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerInputAction();

        // Subscribe to Move action
        controls.PlayerControl.Move.performed += SetDirection;
        controls.PlayerControl.Move.canceled += ResetDirection;
        rb = GetComponent<Rigidbody>();
        animationController = GetComponent<AnimationController>();
    }

    private void SetDirection(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        animationController.SetBool("Move", true);
    }
    private void ResetDirection(InputAction.CallbackContext context)
    {
        direction = Vector2.zero;
        animationController.SetBool("Move", false);
    }

    void OnEnable()
    {
        controls.PlayerControl.Enable();
    }

    void OnDisable()
    {
        controls.PlayerControl.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = direction.normalized;
        rb.velocity = new Vector3(direction.x * speed, 0, direction.y * speed);
    }
}
