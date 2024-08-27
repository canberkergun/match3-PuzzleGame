using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Board : MonoBehaviour
{
    
#region Fields

    public int width;
    public int height;

    public GameObject bgTilePrefab;

#endregion

#region Unity Events

    private void Start()
    {
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
            }
        }
    }

#endregion

#region Public Methods

#endregion
}
