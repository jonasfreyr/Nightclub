using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPoint : MonoBehaviour
{
    public GameObject repairStatusCanvas;
    protected Image _repairButtonBackground;
    public Image timerImage;
    
    private float _timer;
    private float timeToFail = 10f;
    
    private void Start()
    {
        _repairButtonBackground = repairStatusCanvas.transform.Find("RepairButton").GetComponent<Image>();
    }

    private void SetImageFill(float value)
    {
        if (timerImage == null) return;
        
        
        timerImage.fillAmount = value;
    }
    
    private void Update()
    {
        if (!IsBroken())
        {
            SetImageFill(1);
            _timer = 0f;
            return;
        }

        if (IsFixing()) return;
        
        _timer += Time.deltaTime;
        SetImageFill(timerImage.fillAmount - 1.0f / timeToFail * Time.deltaTime);

        if (_timer >= timeToFail)
        {
            ForceFix();

            GameManager.Instance.AddSatisfaction(-15);
        }
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
    
    public virtual bool IsFixing()
    {
        throw new NotImplementedException();
    }

    public virtual void ForceFix()
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
    
    protected void _setRepairButtonState(bool repairing)
    {
        var color = repairing
            ? new Color(0, 1.0f, 0.02f, 0.517f)
            : new Color(0.07f, 0, 1.0f, 0.517f);
        _repairButtonBackground.color = color;
    }
}
