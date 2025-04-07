using UnityEngine;

public class Unit : MonoBehaviour
{
    UnitBaseState currentState;
    public UnitIdleState unitIdleState = new UnitIdleState();
    public UnitMoveState unitMoveState = new UnitMoveState();
    public UnitAttackState unitAttackState = new UnitAttackState();
    public Unit targetUnit;
    public BoxCollider attackRange;
    private Vector3 direction;
    private Rigidbody rb;
    private int speed = 5;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        currentState = unitIdleState;
        currentState.EnterState(this);
    }
    public void SwitchState(UnitBaseState newState)
    {
        if(currentState == newState) return;
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
    public UnitBaseState GetCurrentState()
    {
        return currentState;
    }
    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        SetUnityRotation();
    }
    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }
    public void PlayAnimation(int animationNameHash)
    {
        animator.Play(animationNameHash);
    }
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    public void SetVelocity(bool isIdle = false)
    {
        if(isIdle) {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.velocity = speed*direction;
    }
    public void SetUnityRotation()
    {
        transform.LookAt(direction + transform.position);
    }
}
