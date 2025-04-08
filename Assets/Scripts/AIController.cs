using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private float timer = 0;
    private float timerMax = 0.5f;
    private float min;
    [SerializeField] Unit target;
    private Unit unit;
    private float attackRange;

    void Start()
    {
        unit = GetComponent<Unit>();
        attackRange = unit.attackRange.size.z / 2;
    }

    void Update()
    {
        // Periodically find the closest unit
        if (timer <= 0)
        {
            timer = timerMax;
            FindClosestUnit();
        }
        else
        {
            timer -= Time.deltaTime;
        }

        // If there's no target, stay idle
        if ((target == null || unit.CheckIfCanAttack()) && unit.GetCurrentState() != unit.unitAttackState)
        {
            unit.SwitchState(unit.unitIdleState);
            return;
        }
        if(!unit.CheckIfCanAttack() && unit.GetCurrentState() == unit.unitIdleState){
            unit.SwitchState(unit.unitMoveState);
            SetDirection();
        }
    }
    private void SetDirection(){
        if(target != null){
            unit.SetDirection((target.transform.position - transform.position).normalized);
        }
        Debug.Log(unit.GetCurrentState());
    }
    void FindClosestUnit()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, LayerMask.GetMask("Unit"));
        min = float.MaxValue;
        target = null;

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                float distance = GetDistanceSquare(transform.position, collider.transform.position);
                if (distance < min)
                {
                    min = distance;
                    target = collider.GetComponent<Unit>();
                }
            }
        }
        SetDirection();
    }

    float GetDistanceSquare(Vector3 a, Vector3 b)
    {
        return (a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z);
    }
}
