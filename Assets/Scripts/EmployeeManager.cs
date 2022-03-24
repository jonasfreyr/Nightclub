using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EmployeeTask
{
    Idle,
    Bartender,
    CleanBathroom,
    CleanPuke,
    ReturnToStaffArea,
}

[Serializable]
public class EmployeeTaskInfo
{
    public string name;
    public EmployeeTask task;
    public object argument;
}

public class EmployeeManager : MonoBehaviour
{
    public EmployeeTaskMenu taskMenu;
    public bool isEmployeeWorkingAtBar;
    [Header("Task attributes")] 
    public float cleanBathroomTime;
    public float cleaningPukeTime;
    [Header("Task positions")] 
    public PointOfInterest bar;
    public PointOfInterest idlePoint;
    [Header("Tasks")]
    public List<EmployeeTaskInfo> tasks;
}