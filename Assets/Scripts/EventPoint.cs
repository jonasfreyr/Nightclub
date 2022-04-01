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
    public RectTransform arrow;
    public Transform arrowPrefab;

    private float _timer;
    protected Collider2D _collider;
    
    private void Start()
    {
        if (repairStatusCanvas != null)
        {
            _repairButtonBackground = repairStatusCanvas.transform.Find("RepairButton").GetComponent<Image>();   
        }
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
            _destroyArrow();
            SetImageFill(1);
            _timer = 0f;
            return;
        }

        if (IsFixing()) return;
        
        _timer += Time.deltaTime;
        SetImageFill(timerImage.fillAmount - 1.0f / GameManager.Instance.timeToFail * Time.deltaTime);

        if (_timer >= GameManager.Instance.timeToFail)
        {
            ForceFix();

            GameManager.Instance.AddSatisfaction(-15);
        }
        
        // Render arrow
        var screenPos = GameManager.Instance.camera.WorldToViewportPoint(transform.position); //get viewport positions
 
        if(screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1){
            if (arrow != null)
            {
                _destroyArrow();
            }
            return;
        }
 
        var onScreenPos = new Vector2(screenPos.x-0.5f, screenPos.y-0.5f)*2; //2D version, new mapping
        var max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y)); //get largest offset
        onScreenPos = (onScreenPos/(max*2))+new Vector2(0.5f, 0.5f); //undo mapping
        onScreenPos *= 0.9f;
        
        if (arrow == null)
        {
            var arrowObject = Instantiate(arrowPrefab, GameManager.Instance.arrowsContainer.transform);
            arrow = arrowObject.GetComponent<RectTransform>();
        }

        var dir = (transform.position - GameManager.Instance.employee.transform.position).normalized;
        var rotation = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
        arrow.eulerAngles = new Vector3(0, 0, rotation);

        if (onScreenPos.x < 0.1) onScreenPos.x = 0.1f;
        arrow.anchoredPosition = new Vector2(
            ((onScreenPos.x*GameManager.Instance.canvas.sizeDelta.x)-(GameManager.Instance.canvas.sizeDelta.x*0.5f)),
            ((onScreenPos.y*GameManager.Instance.canvas.sizeDelta.y)-(GameManager.Instance.canvas.sizeDelta.y*0.5f)));
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

    public bool IsInBox(Collider2D otherCollider)
    {
        return _collider.IsTouching(otherCollider);
    }
    
    protected void _setRepairButtonState(bool repairing)
    {
        var color = repairing
            ? new Color(0, 1.0f, 0.02f, 0.517f)
            : new Color(0.07f, 0, 1.0f, 0.517f);
        _repairButtonBackground.color = color;
    }

    protected void _destroyArrow()
    {
        if (arrow == null) return;
        
        Destroy(arrow.gameObject);
        arrow = null;
    }
}
