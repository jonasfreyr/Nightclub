using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarEvent : EventPoint
{
    private bool _isBroken;
    private bool _isFixing;
    private BoxCollider2D _collider;
    
    public void Start()
    {
        _repairButtonBackground = repairStatusCanvas.transform.Find("RepairButton").GetComponent<Image>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
    }
    
    public override void Break()
    {
        GameManager.Instance.barBroken = true;
        
        gameObject.GetComponent<PointOfInterest>().enabled = false;
        
        repairStatusCanvas.SetActive(true);

        _isBroken = true;
    }
    
    public override void Fix()
    {
        if (_isFixing) return;
        _isFixing = true;
        StartCoroutine(_fix());
    }

    public override void ForceFix()
    {
        GameManager.Instance.barBroken = false;
        _destroyArrow();
        
        gameObject.GetComponent<PointOfInterest>().enabled = true;
        
        repairStatusCanvas.SetActive(false);

        _isBroken = false;
    }

    private IEnumerator _fix()
    {
        GameManager.Instance.minigames.PlayMinigame(MinigameType.BarGame);
        _setRepairButtonState(true);

        while (GameManager.Instance.minigames.IsPlayingMinigame)
        {
            yield return new WaitForSeconds(0.1f);            
        }

        _isFixing = false;
        _setRepairButtonState(false);
        if (GameManager.Instance.minigames.Succeeded)
        {
            _isBroken = false;
            repairStatusCanvas.SetActive(false);
            GameManager.Instance.barBroken = false;
        
            gameObject.GetComponent<PointOfInterest>().enabled = true;
        }
    }
    
    public override bool IsBroken()
    {
        return _isBroken;
    }

    public override bool IsFixing()
    {
        return _isFixing;
    }

    public override Vector3 GetEventPosition()
    {
        var size = _collider.size;
        var position = (Vector2) transform.position + _collider.offset;
        var x = Random.Range(position.x - (size.x / 2), position.x + (size.x / 2));
        var y = Random.Range(position.y - (size.y / 2), position.y + (size.y / 2));

        return new Vector3(x, y);
    }
    
}
