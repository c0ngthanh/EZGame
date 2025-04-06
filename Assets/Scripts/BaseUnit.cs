using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BaseUnitState
{
    Idle,
    Move,
    Attack,
    Dead
}
public class BaseUnit : MonoBehaviour
{
    public BaseUnitState state = BaseUnitState.Idle;
    public BaseUnit targetUnit;
    public BoxCollider attackRange;
    [SerializeField] AnimationController animationController;
    // Start is called before the first frame update
    void Awake()
    {
        animationController = GetComponent<AnimationController>();
    }
    public void SetBaseUnitState(BaseUnitState state)
    {
        this.state = state;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(state == BaseUnitState.Idle){
            Collider[] colliders =  Physics.OverlapBox(attackRange.transform.position, attackRange.size/2, Quaternion.identity,LayerMask.GetMask("Unit"));
            animationController.SetBool("Attack", false);
        }
    }
}
