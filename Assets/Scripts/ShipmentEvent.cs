using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipmentEvent : EventPoint
{
    private bool _isPackageWaiting;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _setSpriteVisibility(false);
    }

    public override void Break()
    {
        _setSpriteVisibility(true);
        _isPackageWaiting = true;
        repairStatusCanvas.SetActive(true);
    }
    
    public override void Fix()
    {
        _setSpriteVisibility(false);
        _isPackageWaiting = false;
        repairStatusCanvas.SetActive(false);
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
        return false;
    }

    private void _setSpriteVisibility(bool visible)
    {
        var color = _spriteRenderer.color;
        var alpha = visible ? 1f : 0f;
        _spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
    }
}
