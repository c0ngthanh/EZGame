using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Dictionary<string, int> hashTable = new Dictionary<string, int>();
    public void Awake()
    {
        hashTable["Move"] = Animator.StringToHash("Move");
        hashTable["Attack"] = Animator.StringToHash("Attack");
    }
    public void SetBool(string key, bool value)
    {
        animator.SetBool(hashTable[key], value);
    }
}
