using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.Rendering;

public class EmployeeBehaviour : MonoBehaviour
{
    public Animator animator;
    private AIDestinationSetter _targetSetter;
    private bool _goingToTask;
    private List<EventPoint> _scheduledActions;
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Standing = Animator.StringToHash("Standing");

    public EventPoint CurrentTask { get; private set; }
    
    private void Awake()
    {
        _targetSetter = GetComponent<AIDestinationSetter>();
        _scheduledActions = new List<EventPoint>();
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
    }

    public void ScheduleTask(EventPoint task)
    {
        _scheduledActions.Add(task);
    }

    private void _taskUpdate()
    {
        switch (CurrentTask)
        {
            case null:
                if (_scheduledActions.Count != 0)
                {
                    _switchToNextTask();
                }
                break;
            default:
                if (!CurrentTask.IsBroken() || !CurrentTask.IsFixing())
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

    private void _switchToNextTask()
    {
        if (_scheduledActions.Count == 0) return;

        var task = _scheduledActions.First();
        _scheduledActions.RemoveAt(0);
        
        _onTaskChange(task, CurrentTask);
        CurrentTask = task;
    }

    private void _walkToTask(EventPoint task)
    {
        var position = _eventPointPosition(task);
        _targetSetter.SetTarget(position);
        animator.SetTrigger(Walking);
        _goingToTask = true;
    }

    private Vector3 _eventPointPosition(EventPoint task)
    {
        return task.GetEventPosition();
    }
}