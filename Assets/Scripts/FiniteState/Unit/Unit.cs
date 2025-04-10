using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Faction
{
    Player,
    Enemy
}
public class Unit : MonoBehaviour
{
    #region Unit Attribute
    public float Speed = 1;
    public float HP { get; private set; } = 100;
    public float Damage { get; private set; } = 10;
    public float HPMax { get; private set; } = 100;
    #endregion Unit Attribute
    UnitBaseState currentState;
    public UnitIdleState unitIdleState = new UnitIdleState();
    public UnitMoveState unitMoveState = new UnitMoveState();
    public UnitAttackState unitAttackState = new UnitAttackState();
    public SphereCollider attackRange;
    public Faction faction;
    private Vector3 direction;
    private Rigidbody rb;
    public EventHandler<float> onUnitAttacked;
    [SerializeField] public Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // private void Start()
    // {
    //     currentState = unitIdleState;
    //     currentState.EnterState(this);
    //     direction = Vector3.forward;
    // }
    private void OnEnable()
    {
        currentState = unitIdleState;
        currentState.EnterState(this);
        direction = Vector3.forward;
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
        Vector3 boxCenter = transform.position + direction*0.3f + new Vector3(0,0.5f,0);
        float radius = attackRange.radius;
        // Vector3 boxSize = attackRange.size / 2;
        // Quaternion boxRotation = attackRange.transform.rotation;
        // Debug.Log(boxCenter + " " + boxSize + " " + boxRotation);


        // Perform the OverlapBox check
        Collider[] colliders = Physics.OverlapSphere(
            boxCenter,
            radius,
            LayerMask.GetMask("Unit")
        );
        // Debug.Log(transform + " " + transform.forward);
        DebugDrawSphere(boxCenter, radius, Color.red);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.TryGetComponent<Unit>(out Unit checkUnit))
            {
                if (checkUnit.faction != faction)
                {
                    // Debug.Log("Hit " + collider.gameObject.name);
                    // Apply damage to the enemy unit
                    return true;
                }
            }
        }
        return false;
    }
    public void OnReceiveDamege(float damage)
    {
        HP -= damage;
        onUnitAttacked?.Invoke(this, HP / (float)HPMax);
        if (HP <= 0)
        {
            gameObject.SetActive(false);
            GameManager.instance.CheckGameOver();
        }
    }
    public void SetUnitAttribute(UnitAttriBute unitAttribute)

    {
        Speed = unitAttribute.GetSpeed();
        HP = unitAttribute.GetHealth();
        HPMax = unitAttribute.GetHealth();
        Damage = unitAttribute.GetDamage();
        onUnitAttacked?.Invoke(this, HP / (float)HPMax);
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
    private void DebugDrawSphere(Vector3 center, float radius, Color color, int segments = 36)
    {
        // Draw a circle in the XZ plane
        for (int i = 0; i < segments; i++)
        {
            float angle1 = (i * Mathf.PI * 2) / segments;
            float angle2 = ((i + 1) * Mathf.PI * 2) / segments;

            Vector3 point1 = center + new Vector3(Mathf.Cos(angle1), 0, Mathf.Sin(angle1)) * radius;
            Vector3 point2 = center + new Vector3(Mathf.Cos(angle2), 0, Mathf.Sin(angle2)) * radius;

            Debug.DrawLine(point1, point2, color);
        }

        // Draw a circle in the XY plane
        for (int i = 0; i < segments; i++)
        {
            float angle1 = (i * Mathf.PI * 2) / segments;
            float angle2 = ((i + 1) * Mathf.PI * 2) / segments;

            Vector3 point1 = center + new Vector3(Mathf.Cos(angle1), Mathf.Sin(angle1), 0) * radius;
            Vector3 point2 = center + new Vector3(Mathf.Cos(angle2), Mathf.Sin(angle2), 0) * radius;

            Debug.DrawLine(point1, point2, color);
        }

        // Draw a circle in the YZ plane
        for (int i = 0; i < segments; i++)
        {
            float angle1 = (i * Mathf.PI * 2) / segments;
            float angle2 = ((i + 1) * Mathf.PI * 2) / segments;

            Vector3 point1 = center + new Vector3(0, Mathf.Cos(angle1), Mathf.Sin(angle1)) * radius;
            Vector3 point2 = center + new Vector3(0, Mathf.Cos(angle2), Mathf.Sin(angle2)) * radius;

            Debug.DrawLine(point1, point2, color);
        }
    }
}
