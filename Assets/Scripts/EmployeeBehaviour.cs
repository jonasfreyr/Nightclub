using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.Rendering;

public class EmployeeBehaviour : MonoBehaviour
{
    public float maxSpeed = 10.0f;
    public float speedUpSpeed = 0.4f;
    public Animator animator;
    private Rigidbody2D _rigidbody;
    private static readonly int Walking = Animator.StringToHash("Walking");
    public EventPoint CurrentTask { get; private set; }
    public Vector2 velocity => _rigidbody.velocity;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var velocity = _rigidbody.velocity;

        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input = input.normalized;
        
        if (input.y > 0.1)
        {
            if (velocity.y < maxSpeed) velocity.y += speedUpSpeed;
        }
        else if (input.y < -0.1)
        {
            if (velocity.y > -maxSpeed) velocity.y -= speedUpSpeed;
        }
        else
        {
            velocity.y = 0;
        }
        
        if (input.x > 0.1)
        {
            if (velocity.x < maxSpeed) velocity.x += speedUpSpeed;
        }
        else if (input.x < -0.1)
        {
            if (velocity.x > -maxSpeed) velocity.x -= speedUpSpeed;
        }
        else
        {
            velocity.x = 0;
        }

        if (velocity.x != 0 || velocity.y != 0)
        {
            animator.SetBool(Walking, true);
        }
        else
        {
            animator.SetBool(Walking, false);
        }

        _rigidbody.velocity = velocity;

        if (CurrentTask != null)
        {
            _taskUpdate();
        }
    }

    public void SetTask(EventPoint task)
    {
        _setTask(task);
    }

    public void ResetEmployee()
    {
        CurrentTask = null;
        // _targetSetter.SetTarget(initalPosition);
    }
    
    private void _taskUpdate()
    {
        switch (CurrentTask)
        {
            case null: break;
            default:
                if (!CurrentTask.IsBroken() || !CurrentTask.IsFixing())
                {
                    CurrentTask = null;
                }
                break;
        }
    }
    
    private void _onTaskChange(EventPoint newTask, EventPoint oldTask)
    {
        switch (newTask)
        {
            case null: break;
            default:
                newTask.Fix();
                break;
        }
    }

    private void _setTask(EventPoint task)
    {
        _onTaskChange(task, CurrentTask);
        CurrentTask = task;
    }
}