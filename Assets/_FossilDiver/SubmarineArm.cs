using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SubmarineArm : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void LowerArm()
    {

    }

    public void RaiseArm()
    {

    }

    public void MoveArmRight()
    {

    }

    public void MoveArmLeft()
    {

    }
    
    public void MoveArmForward()
    {

    }

    public void MoveArmBackward()
    {

    }

    public void Grab()
    {
        _animator.SetTrigger("Grab");
    }

}

