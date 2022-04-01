using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkEvent : EventPoint
{
    private bool _isBroken;
    private bool _isFixing;
    
    public override void Break()
    {
        _isBroken = true;
        GameManager.Instance.sinkBroken = true;
        repairStatusCanvas.SetActive(true);
    }
    
    public override void Fix()
    {
        if (_isFixing) return;
        _isFixing = true;
        StartCoroutine(_fix());
    }

    public override void ForceFix()
    {
        _isBroken = false;
        repairStatusCanvas.SetActive(false);
    }

    private IEnumerator _fix()
    {
        GameManager.Instance.minigames.PlayMinigame(MinigameType.FixPipes);
        _setRepairButtonState(true);

        while (GameManager.Instance.minigames.IsPlayingMinigame)
        {
            yield return new WaitForSeconds(0.1f);            
        }

        _isFixing = false;
        _setRepairButtonState(false);
        if (GameManager.Instance.minigames.Succeeded)
        {
            GameManager.Instance.sinkBroken = false;
            _isBroken = false;
            repairStatusCanvas.SetActive(false);
        }
    }

    public override Vector3 GetEventPosition()
    {
        return transform.position;
    }

    public override bool IsBroken()
    {
        return _isBroken;
    }

    public override bool IsFixing()
    {
        return _isFixing;
    }
}
