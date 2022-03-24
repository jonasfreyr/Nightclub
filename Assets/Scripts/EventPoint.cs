using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPoint : MonoBehaviour
{
    public GameObject repairStatusCanvas;
    
    public virtual void Break()
    {
        throw new NotImplementedException();
    }

    public virtual void Fix()
    {
        throw new NotImplementedException();
    }

    public virtual bool IsBroken()
    {
        throw new NotImplementedException();
    }

    public void EmployeeFix()
    {
        GameManager.Instance.employee.SetTask(this);
    }

    public virtual Vector3 GetEventPosition()
    {
        throw new NotImplementedException();
    }
}
