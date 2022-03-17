using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using TMPro;
using UnityEngine.UI;

public class AssetManager : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public Sprite sprite;
        public Transform prefab;
        public string name;
        public double price;
    }
    
    public GameObject assetList;
    public Vector2 gridGap;
    public Vector2 tileSize;
    public int itemsPerRow;
    public Transform assetStoreItemPrefab;
    public List<Item> assets;
    public GameObject assetStoreMenu;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < assets.Count; i++)
        {
            _createStoreItem(i);
        }
    }

    public void CloseMenu()
    {
        assetStoreMenu.SetActive(false);
    }

    private void _createStoreItem(int i)
    {
        var item = Instantiate(assetStoreItemPrefab, assetList.transform);
        item.Find("Image").GetComponent<Image>().sprite = assets[i].sprite;
        item.Find("Title").GetComponent<TextMeshProUGUI>().text = assets[i].name;
        item.Find("Price").GetComponent<TextMeshProUGUI>().text =
            assets[i].price.ToString("C0", new CultureInfo("en-US", false));
        item.GetComponent<RectTransform>().anchoredPosition = _gridPositionForIndex(i);
        item.GetComponent<Button>().onClick.AddListener(() => _createItemFromStore(assets[i]));
    }

    private void _createItemFromStore(Item item)
    {
        Instantiate(item.prefab);
    }

    private Vector2 _gridPositionForIndex(int index) 
        => new Vector2((index % itemsPerRow) * (tileSize.x + gridGap.x) + (tileSize.x + gridGap.x) / 2,  -(index/itemsPerRow) * (tileSize.y + gridGap.y) - (tileSize.y + gridGap.y));
}

