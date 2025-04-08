using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour
{
    #region Unit Attribute
    public int Speed = 1;
    public int HP { get; private set; } = 100;
    public int Damage { get; private set; } = 10;
    public int HPMax { get; private set; } = 100;
    #endregion Unit Attribute
    UnitBaseState currentState;
    public UnitIdleState unitIdleState = new UnitIdleState();
    public UnitMoveState unitMoveState = new UnitMoveState();
    public UnitAttackState unitAttackState = new UnitAttackState();
    public BoxCollider attackRange;
    private Vector3 direction;
    private Rigidbody rb;
    public EventHandler<float> onUnitAttacked;
    [SerializeField] public Animator animator;
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
        if (currentState == newState) return;
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
        SetUnitRotation();
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
        if (isIdle)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.velocity = Speed * direction;
    }
    public void SetUnitRotation()
    {
        transform.LookAt(direction + transform.position);
    }
    public bool CheckIfCanAttack()
    {
        // Collider[] colliders =  Physics.OverlapBox(transform.TransformPoint(attackRange.transform.position), attackRange.size/2, Quaternion.identity,LayerMask.GetMask("Unit"));
        Vector3 boxCenter = transform.TransformPoint(attackRange.center);
        Vector3 boxSize = attackRange.size / 2;
        // Debug.Log(transform + " " + boxCenter);
    
        // Perform the OverlapBox check
        Collider[] colliders = Physics.OverlapBox(
            boxCenter,
            boxSize,
            Quaternion.Euler(transform.forward),
            LayerMask.GetMask("Unit")
        );
        Debug.Log(transform + " " + transform.forward);
        DebugDrawBox(boxCenter, boxSize, Quaternion.Euler(transform.forward), Color.red);
        if (colliders.Length > 1)
        {
            return true;
        }
        return false;
    }
    public void OnReceiveDamege(int damage)
    {
        HP -= damage;
        onUnitAttacked?.Invoke(this, HP / (float)HPMax);
        if (HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void DebugDrawBox(Vector3 center, Vector3 halfExtents, Quaternion rotation, Color color)
    {
        // Get the 8 corners of the box
        Vector3[] corners = new Vector3[8];
        corners[0] = center + rotation * new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
        corners[1] = center + rotation * new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);
        corners[2] = center + rotation * new Vector3(halfExtents.x, -halfExtents.y, halfExtents.z);
        corners[3] = center + rotation * new Vector3(-halfExtents.x, -halfExtents.y, halfExtents.z);
        corners[4] = center + rotation * new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
        corners[5] = center + rotation * new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
        corners[6] = center + rotation * new Vector3(halfExtents.x, halfExtents.y, halfExtents.z);
        corners[7] = center + rotation * new Vector3(-halfExtents.x, halfExtents.y, halfExtents.z);

        // Draw the edges of the box
        Debug.DrawLine(corners[0], corners[1], color);
        Debug.DrawLine(corners[1], corners[2], color);
        Debug.DrawLine(corners[2], corners[3], color);
        Debug.DrawLine(corners[3], corners[0], color);
        Debug.DrawLine(corners[4], corners[5], color);
        Debug.DrawLine(corners[5], corners[6], color);
        Debug.DrawLine(corners[6], corners[7], color);
        Debug.DrawLine(corners[7], corners[4], color);
        Debug.DrawLine(corners[0], corners[4], color);
        Debug.DrawLine(corners[1], corners[5], color);
        Debug.DrawLine(corners[2], corners[6], color);
        Debug.DrawLine(corners[3], corners[7], color);
    }
}
