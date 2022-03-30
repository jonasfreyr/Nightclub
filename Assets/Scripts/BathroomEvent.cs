using UnityEngine;

public class BathroomEvent : EventPoint
{
    private bool _isBroken;
    private BoxCollider2D _collider;
    
    public void Start()
    {
        _collider = gameObject.GetComponent<BoxCollider2D>();
    }

    public override void Break()
    {
        GameManager.Instance.bathroomBroken = true;
        
        gameObject.GetComponent<PointOfInterest>().enabled = false;
        
        repairStatusCanvas.SetActive(true);
    }
    
    public override void Fix()
    {
        GameManager.Instance.bathroomBroken = false;
        
        gameObject.GetComponent<PointOfInterest>().enabled = true;
        
        repairStatusCanvas.SetActive(false);
    }
    public override bool IsBroken()
    {
        return _isBroken;
    }

    public override bool IsFixing()
    {
        return false;
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
