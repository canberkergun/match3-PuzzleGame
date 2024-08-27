using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    
#region Fields

    public int width;
    public int height;

    public GameObject bgTilePrefab;
    
    public Gem[] gems;

    public Gem[,] allGems;

#endregion

#region Unity Events

    private void Start()
    {
        allGems = new Gem[width, height];
        
        Setup();
    }

#endregion

#region Private Methods

    private void Setup()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 pos = new Vector2(i, j);
                GameObject bgTile = Instantiate(bgTilePrefab, pos, Quaternion.identity);
                bgTile.transform.parent = transform;
                bgTile.name = "BG Tile - " + i + ", " + j;

                int gemToUse = Random.Range(0, gems.Length);
                
                SpawnGem(new Vector2Int(i,j), gems[gemToUse]);
            }
        }
    }

    private void SpawnGem(Vector2Int pos, Gem gemToSpawn)
    {
        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        gem.transform.parent = transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y;
        allGems[pos.x, pos.y] = gem;
        gem.SetupGem(pos, this);
    }

#endregion

#region Public Methods

#endregion
}
