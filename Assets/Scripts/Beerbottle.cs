using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Beerbottle : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool placed;

    public BarStockMinigame barStockMinigame;

    private BoxCollider2D _collider;

    private Vector3 _offset = new Vector3(-5, 30);

    public AudioClip clang;
    private AudioSource _audioSource;
    
    public void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnDrag(PointerEventData eventData)
    {   
        if (placed) return;
        var pos = transform.position;
        
        transform.position = new Vector3(pos.x + eventData.delta.x, pos.y + eventData.delta.y, pos.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var tile = barStockMinigame.GetTile(_collider);
        if (tile != null)
        {
            placed = true;
            tile.placed = true;

            transform.position = tile.transform.position + _offset;
            
            _audioSource.PlayOneShot(clang, 1f);
            
        }
    }
}
