using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] PlayerInputAction controls;
    // [SerializeField] AnimationController animationController;
    private Vector2 direction;
    private Vector3 cacheDirection;
    private Rigidbody rb;
    private Unit baseUnit;
    private int speed =5;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerInputAction();

        // Subscribe to Move action
        controls.PlayerControl.Move.performed += SetDirection;
        controls.PlayerControl.Move.canceled += ResetDirection;
        rb = GetComponent<Rigidbody>();
        // animationController = GetComponent<AnimationController>();
        baseUnit = GetComponent<Unit>();
    }

    private void SetDirection(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>().normalized;
        baseUnit.SwitchState(baseUnit.unitMoveState);
        cacheDirection = new Vector3(direction.x, 0, direction.y);
    }
    private void ResetDirection(InputAction.CallbackContext context)
    {
        direction = Vector2.zero;
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
    void Update()
    {
        transform.LookAt(cacheDirection + transform.position);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.position = transform.position + new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;
        rb.velocity = new Vector3(direction.x, 0, direction.y) * speed;
        // transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
