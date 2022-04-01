using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipmentEvent : EventPoint
{
    private bool _isPackageWaiting;
    private SpriteRenderer _spriteRenderer;
    private bool _isCollecting;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _setSpriteVisibility(false);
    }

    public override void Break()
    {
        _setSpriteVisibility(true);
        GameManager.Instance.shipmentWaiting = true;
        _isPackageWaiting = true;
        repairStatusCanvas.SetActive(true);
    }
    
    public override void Fix()
    {
        StartCoroutine(_fix());
    }

    public override void ForceFix()
    {
        _setSpriteVisibility(false);
        _destroyArrow();
        _isPackageWaiting = false;
        repairStatusCanvas.SetActive(false);
    }

    private IEnumerator _fix()
    {
        GameManager.Instance.minigames.PlayMinigame(MinigameType.MoveBoxes);
        _setRepairButtonState(true);
        _isCollecting = true;

        while (GameManager.Instance.minigames.IsPlayingMinigame)
        {
            yield return new WaitForSeconds(0.1f);            
        }
        
        _isCollecting = false;
        _setRepairButtonState(false);
        if (GameManager.Instance.minigames.Succeeded)
        {
            GameManager.Instance.shipmentWaiting = false;
            _setSpriteVisibility(false);
            _isPackageWaiting = false;
            repairStatusCanvas.SetActive(false);
        }
    }
    
    public override Vector3 GetEventPosition()
    {
        return transform.position;
    }

    public override bool IsBroken()
    {
        return _isPackageWaiting;
    }

    public override bool IsFixing()
    {
        return _isCollecting;
    }

    private void _setSpriteVisibility(bool visible)
    {
        var color = _spriteRenderer.color;
        var alpha = visible ? 1f : 0f;
        _spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
    }
}
