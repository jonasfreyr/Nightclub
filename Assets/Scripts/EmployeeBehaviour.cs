using System;
using Pathfinding;
using UnityEngine;

class GetShipmentEventPoint : EventPoint
{
    public bool isBroken = false;
    
    public override void Fix()
    {
        isBroken = false;
    }

    public override void Break()
    {
        isBroken = true;
    }

    public override Vector3 Position()
    {
        return new Vector3(0, 0, 0);
    }
}

public class EmployeeBehaviour : MonoBehaviour
{
    public Animator animator;
    public EmployeeManager employeeManager;
    private AIDestinationSetter _targetSetter;
    private bool _goingToTask = false;
    private float _taskStartTime;
    private CapsuleCollider2D _collider;
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Standing = Animator.StringToHash("Standing");

    public EventPoint CurrentTask { get; private set; }
    
    private void Awake()
    {
        _targetSetter = GetComponent<AIDestinationSetter>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (_goingToTask && _targetSetter.done)
        {
            animator.SetTrigger(Standing);
            _goingToTask = false;
            _onTaskReached();
        }

        if (!_goingToTask)
        {
            _taskUpdate();
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            var mousePos = GameManager.GetMouseTo2DWorldPos();
            if (_collider.bounds.Contains(mousePos))
            {
                employeeManager.taskMenu.OpenMenu(this);
            }
        }
    }

    public void SetTask(EventPoint task)
    {
        _onTaskChange(task, CurrentTask);
        CurrentTask = task;
    }

    private void _taskUpdate()
    {
        switch (CurrentTask)
        {
            case null: break;
            default:
                if (!CurrentTask.IsBroken())
                {
                    CurrentTask = null;
                }
                break;
        }
    }

    private void _onTaskReached()
    {
        switch (CurrentTask)
        {
            case null: break;
            default:
                CurrentTask.Fix();
                break;
        }
    }

    private void _onTaskChange(EventPoint newTask, EventPoint oldTask)
    {
        switch (newTask)
        {
            case null:
                animator.SetTrigger(Standing);
                break;
            default:
                _walkToTask(newTask);
                break;
        }
    }

    private void _walkToTask(EventPoint task)
    {
        var position = task.Position();
        _targetSetter.SetTarget(position);
        animator.SetTrigger(Walking);
        _goingToTask = true;
    }
}