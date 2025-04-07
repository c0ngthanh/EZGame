using UnityEngine;

public class Unit : MonoBehaviour
{
    UnitBaseState currentState;
    public UnitIdleState unitIdleState = new UnitIdleState();
    public UnitMoveState unitMoveState = new UnitMoveState();
    public UnitAttackState unitAttackState = new UnitAttackState();
    public Unit targetUnit;
    public BoxCollider attackRange;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
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
    }
    public void PlayAnimation(int animationNameHash)
    {
        animator.Play(animationNameHash);
    }
}
