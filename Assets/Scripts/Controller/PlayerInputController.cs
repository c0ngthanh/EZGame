using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] PlayerInputAction controls;
    private Vector2 direction;
    private Unit baseUnit;
    void Awake()
    {
        controls = new PlayerInputAction();

        // Subscribe to Move action
        controls.PlayerControl.Move.performed += SetDirection;
        controls.PlayerControl.Move.canceled += ResetDirection;
        baseUnit = GetComponent<Unit>();
    }

    private void SetDirection(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>().normalized;
        baseUnit.SwitchState(baseUnit.unitMoveState);
        baseUnit.SetDirection(new Vector3(direction.x, 0, direction.y));
    }
    private void ResetDirection(InputAction.CallbackContext context)
    {
        baseUnit.SwitchState(baseUnit.unitIdleState);
    }

    void OnEnable()
    {
        controls.PlayerControl.Enable();
    }

    void OnDisable()
    {
        controls.PlayerControl.Disable();
    }
}
