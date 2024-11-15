using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TilesPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    private GameObject m_firstTile;

    public static TilesPooling Instance;

    Transform childTransform;
    string childText;

    private List<GameObject> tilePool = new List<GameObject>();
    private int poolSize = 10; 
    private List<GameObject> activeTiles = new List<GameObject>(); 
    private int maxActiveTiles = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }

        activeTiles.Add(m_firstTile);
    }
    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject tile = Instantiate(m_prefab);
            tile.transform.SetParent(transform);
            tile.SetActive(false); 
            tilePool.Add(tile);
        }
    }

    public void CreatTile(Transform parentTransform, WallTrigger wall)
    {

        Vector3 position = parentTransform.position;

        if (wall.name == "North"){
            position += new Vector3(0, 0, 250);
            childText = "South";
        }

        if (wall.name == "South"){
            position += new Vector3(0, 0, -250);
            childText = ("North");
        }

        if (wall.name == "West"){
            position += new Vector3(250, 0, 0);
            childText = ("East");
        }

        if (wall.name == "East"){
            position += new Vector3(-250, 0, 0);
            childText = ("West");
        }

        if (activeTiles.Count >= maxActiveTiles)
        {
            GameObject oldestTile = activeTiles[0];
            oldestTile.SetActive(false);
            activeTiles.RemoveAt(0);

            if (m_firstTile != oldestTile)
                tilePool.Add(oldestTile);
        }

        int randomIndex = UnityEngine.Random.Range(0, tilePool.Count);
        GameObject Tile = tilePool[randomIndex];

        Tile.transform.position = position;
        Tile.transform.rotation = parentTransform.rotation;
        Tile.SetActive(true);

        tilePool.Remove(Tile);
        activeTiles.Add(Tile);

        childTransform = Tile.transform.Find(childText);
        childTransform.GetComponent<Collider>().enabled = false;

    }
}

