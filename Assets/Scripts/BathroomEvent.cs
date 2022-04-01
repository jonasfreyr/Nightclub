using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BathroomEvent : EventPoint
{
    public List<PointOfInterest> bathrooms;
    private bool _isBroken;
    private bool _isFixing;
    private PointOfInterest _brokenBathroom;

    public override void Break()
    {
        GameManager.Instance.bathroomBroken = true;

        var indexToBreak = Random.Range(0, bathrooms.Count);
        _brokenBathroom = bathrooms[indexToBreak];
        _brokenBathroom.enabled = false;

        repairStatusCanvas = _brokenBathroom.transform.Find("RepairStatusBar").gameObject;
        _repairButtonBackground = repairStatusCanvas.transform.Find("RepairButton").GetComponent<Image>();
        repairStatusCanvas.SetActive(true);

        timerImage = _brokenBathroom.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        timerImage.fillAmount = 1f;
        
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
        if (!_isBroken) return;
        _isBroken = false;
        repairStatusCanvas.SetActive(false);
        GameManager.Instance.bathroomBroken = false;
        
        _brokenBathroom.enabled = true;
        _brokenBathroom = null;
    }

    private IEnumerator _fix()
    {
        GameManager.Instance.minigames.PlayMinigame(MinigameType.UnclogToilet);
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
            GameManager.Instance.bathroomBroken = false;
        
            _brokenBathroom.enabled = true;
            _brokenBathroom = null;
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
        if (_brokenBathroom == null) return Vector3.zero;
        
        var collider = _brokenBathroom.GetComponent<BoxCollider2D>();
        var size = collider.size;
        var position = (Vector2) _brokenBathroom.transform.position + collider.offset;
        var x = Random.Range(position.x - (size.x / 2), position.x + (size.x / 2));
        var y = Random.Range(position.y - (size.y / 2), position.y + (size.y / 2));

        return new Vector3(x, y);
    }
}
