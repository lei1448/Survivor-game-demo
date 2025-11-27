using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private const string IsAttack = "IsAttack";
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        _animator.SetTrigger(IsAttack);
    }
}
