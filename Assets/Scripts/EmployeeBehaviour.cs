using System;
using Pathfinding;
using UnityEngine;

public class EmployeeBehaviour : MonoBehaviour
{
    public Animator animator;
    public EmployeeManager employeeManager;
    private AIDestinationSetter _targetSetter;
    private bool _goingToTask = false;
    private object _currentTaskArgument;
    private float _taskStartTime;
    private CapsuleCollider2D _collider;
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Standing = Animator.StringToHash("Standing");

    public EmployeeTask CurrentTask { get; private set; }
    
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

    public void SetTask(EmployeeTask task, object taskArgument = null)
    {
        _currentTaskArgument = taskArgument;
        _onTaskChange(task, CurrentTask);
        CurrentTask = task;
    }

    private void _taskUpdate()
    {
        switch (CurrentTask)
        {
            case EmployeeTask.CleanBathroom:
                if (_taskStartTime + employeeManager.cleanBathroomTime < Time.time)
                {
                    if (_currentBathroom() >= GameManager.Instance.customerManager.bathrooms.Length - 1)
                    {
                        SetTask(EmployeeTask.Idle);
                    }
                    else
                    {
                        SetTask(EmployeeTask.CleanBathroom, _currentBathroom() + 1);
                    }
                }
                break;
            case EmployeeTask.CleanPuke:
                if (_taskStartTime + employeeManager.cleaningPukeTime < Time.time)
                {
                    SetTask(EmployeeTask.Idle);
                }
                break;
        }
    }

    private void _onTaskReached()
    {
        switch (CurrentTask)
        {
            case EmployeeTask.CleanBathroom:
                _taskStartTime = Time.time;
                break;
            case EmployeeTask.ReturnToStaffArea:
                SetTask(EmployeeTask.Idle);
                break;
            case EmployeeTask.CleanPuke:
                _taskStartTime = Time.time;
                break;
        }
    }

    private void _onTaskChange(EmployeeTask newTask, EmployeeTask oldTask)
    {
        switch (oldTask)
        {
            case EmployeeTask.Bartender:
                employeeManager.isEmployeeWorkingAtBar = false;
                break;
        }
        
        switch (newTask)
        {
            case EmployeeTask.Bartender:
                _walkToTask(newTask);
                employeeManager.isEmployeeWorkingAtBar = true;
                break;
            case EmployeeTask.CleanBathroom:
                _currentTaskArgument ??= 0;
                _walkToTask(newTask, _currentTaskArgument);
                break;
            case EmployeeTask.CleanPuke:
                _walkToTask(newTask, _currentTaskArgument);
                break;
            case EmployeeTask.Idle:
                animator.SetTrigger(Standing);
                break;
            case EmployeeTask.ReturnToStaffArea:
                _walkToTask(newTask);
                break;
        }
    }

    private void _walkToTask(EmployeeTask task, object taskArgument = null)
    {
        var position = employeeManager.TaskPosition(task, taskArgument);
        _targetSetter.SetTarget(position);
        animator.SetTrigger(Walking);
        _goingToTask = true;
    }

    private int _currentBathroom() => (int)_currentTaskArgument;
}