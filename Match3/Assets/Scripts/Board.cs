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

    public float gemSpeed;
    
    [HideInInspector]
    public MatchFinder matchFind;
    
#endregion

#region Unity Events

    private void Awake()
    {
        matchFind = FindObjectOfType<MatchFinder>();
    }

    private void Start()
    {
        allGems = new Gem[width, height];
        
        Setup();
    }

    private void Update()
    {
        matchFind.FindAllMatches();
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

                int iterations = 0;
                
                while (MatchesAt(new Vector2Int(i,j), gems[gemToUse]) && iterations < 100)
                {
                    gemToUse = Random.Range(0, gems.Length);
                    iterations++;
                }
                
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

    private bool MatchesAt(Vector2Int posToCheck, Gem gemToCheck)
    {
        if (posToCheck.x > 1)
        {
            if (allGems[posToCheck.x - 1, posToCheck.y].type == gemToCheck.type && allGems[posToCheck.x - 2, posToCheck.y].type == gemToCheck.type)
            {
                return true;
            }
        }
        
        if (posToCheck.y > 1)
        {
            if (allGems[posToCheck.x, posToCheck.y - 1].type == gemToCheck.type && allGems[posToCheck.x, posToCheck.y - 2].type == gemToCheck.type)
            {
                return true;
            }
        }
        
        return false;
    }

    private void DestroyMatchedGemAt(Vector2Int pos)
    {
        if (allGems[pos.x, pos.y] != null)
        {
            if (allGems[pos.x, pos.y].isMatched)
            {
                Destroy(allGems[pos.x, pos.y].gameObject);
                allGems[pos.x, pos.y] = null;
            }
        }
    }

#endregion

#region Public Methods

    public void DestroyMatches()
    {
        for (int i = 0; i < matchFind.currentMatches.Count; i++)
        {
            if (matchFind.currentMatches[i] != null)
            {
             DestroyMatchedGemAt(matchFind.currentMatches[i].posIndex);   
            }
        }
    }

#endregion
}
