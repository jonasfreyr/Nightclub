using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPoint : MonoBehaviour
{
    public GameObject repairStatusCanvas;
    protected Image _repairButtonBackground;
    
    private void Start()
    {
        _repairButtonBackground = repairStatusCanvas.transform.Find("RepairButton").GetComponent<Image>();
    }

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

    protected void _setRepairButtonState(bool repairing)
    {
        var color = repairing
            ? new Color(0, 1.0f, 0.02f, 0.517f)
            : new Color(0.07f, 0, 1.0f, 0.517f);
        _repairButtonBackground.color = color;
    }
}
