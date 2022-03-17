using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class AssetManager : MonoBehaviour
{
    public GameObject Canvas;
    public Vector2 gridGap;
    public Vector2 tileSize;
    public Transform assetStoreItemPrefab;
    public List<Item> Assets;




    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Assets.Count; i++)
        {
            //Instantiate(Assets[0].sprite, Canvas.transform);
            Debug.Log(Assets.Count);
            CreateStoreItem(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public class Item
    {
        public Sprite sprite;
        public string Price;
    }

    void CreateStoreItem(int i)
    {
        var item = Instantiate(assetStoreItemPrefab, Canvas.transform);//Canvas.transform.position + new Vector3(i*3, i*3, 0), Quaternion.identity, Canvas.transform); // instantiate prefab
        Debug.Log(item);
        item.Find("AssetStoreItem").GetComponent<Image>().sprite = Assets[i].sprite;
        item.Find("Price").GetComponent<TextMeshProUGUI>().text = Assets[i].Price;
        //item.transform.position.x += gridGap.x;

    }
}

