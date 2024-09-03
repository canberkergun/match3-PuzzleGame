using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    public enum BoardState { wait, move}

    public BoardState currentState = BoardState.move;

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

                while (MatchesAt(new Vector2Int(i, j), gems[gemToUse]) && iterations < 100)
                {
                    gemToUse = Random.Range(0, gems.Length);
                    iterations++;
                }

                SpawnGem(new Vector2Int(i, j), gems[gemToUse]);
            }
        }
    }

    private void SpawnGem(Vector2Int pos, Gem gemToSpawn)
    {
        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y + height, 0), Quaternion.identity);
        gem.transform.parent = transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y;
        allGems[pos.x, pos.y] = gem;
        gem.SetupGem(pos, this);
    }

    private bool MatchesAt(Vector2Int posToCheck, Gem gemToCheck)
    {
        if (posToCheck.x > 1)
        {
            if (allGems[posToCheck.x - 1, posToCheck.y].type == gemToCheck.type &&
                allGems[posToCheck.x - 2, posToCheck.y].type == gemToCheck.type)
            {
                return true;
            }
        }

        if (posToCheck.y > 1)
        {
            if (allGems[posToCheck.x, posToCheck.y - 1].type == gemToCheck.type &&
                allGems[posToCheck.x, posToCheck.y - 2].type == gemToCheck.type)
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

    private IEnumerator DecreaseRowCo()
    {
        yield return new WaitForSeconds(.2f);

        int nullCounter = 0;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allGems[i, j] == null)
                {
                    nullCounter++;
                }
                else if (nullCounter > 0)
                {
                    allGems[i, j].posIndex.y -= nullCounter;
                    allGems[i, j - nullCounter] = allGems[i, j];
                    allGems[i, j] = null;
                }
            }

            nullCounter = 0;
        }

        StartCoroutine(FillBoardCo());
    }

    private IEnumerator FillBoardCo()
    {
        yield return new WaitForSeconds(.5f);

        RefillBoard();

        yield return new WaitForSeconds(.5f);

        matchFind.FindAllMatches();

        if (matchFind.currentMatches.Count > 0)
        {
            yield return new WaitForSeconds(1.5f);
            DestroyMatches();
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            currentState = BoardState.move;
        }
    }

    private void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allGems[i, j] == null)
                {
                    int gemToUse = Random.Range(0, gems.Length);

                    SpawnGem(new Vector2Int(i, j), gems[gemToUse]);
                }
            }
        }

        CheckMisplacedGems();
    }

    private void CheckMisplacedGems()
    {
        List<Gem> foundGems = new List<Gem>();

        foundGems.AddRange(FindObjectsOfType<Gem>());

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (foundGems.Contains(allGems[i, j]))
                {
                    foundGems.Remove(allGems[i, j]);
                }
            }
        }

        foreach (Gem g in foundGems)
        {
            Destroy(g.gameObject);
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

        StartCoroutine(DecreaseRowCo());
    }

    #endregion
}