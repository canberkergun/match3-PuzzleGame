using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchFinder : MonoBehaviour
{

#region Fields

    private Board board;
    public List<Gem> currentMatches = new List<Gem>();
#endregion

#region Unity Events

    private void Awake()
    {
        board = FindObjectOfType<Board>();
    }

#endregion

#region Private Methods

#endregion

#region Public Methods

    public void FindAllMatches()
    {
        currentMatches.Clear();
        
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                Gem currentGem = board.allGems[i, j];
                if (currentGem != null)
                {
                    if (i > 0 && i < board.width - 1)
                    {
                        Gem leftGem = board.allGems[i - 1, j];
                        Gem rightGem = board.allGems[i + 1, j];
                        if (leftGem != null && rightGem != null)
                        {
                            if (leftGem.type == currentGem.type && rightGem.type == currentGem.type && currentGem.type != Gem.GemType.stone)
                            {
                                currentGem.isMatched = true;
                                leftGem.isMatched = true;
                                rightGem.isMatched = true;
                                
                                currentMatches.Add(currentGem);
                                currentMatches.Add(leftGem);
                                currentMatches.Add(rightGem);
                            }
                        }
                    }
                    if (j > 0 && j < board.height - 1)
                    {
                        Gem aboveGem = board.allGems[i, j + 1];
                        Gem belowGem = board.allGems[i, j - 1];
                        if (aboveGem != null && belowGem != null)
                        {
                            if (aboveGem.type == currentGem.type && belowGem.type == currentGem.type && currentGem.type != Gem.GemType.stone)
                            {
                                currentGem.isMatched = true;
                                aboveGem.isMatched = true;
                                belowGem.isMatched = true;
                                
                                currentMatches.Add(currentGem);
                                currentMatches.Add(aboveGem);
                                currentMatches.Add(belowGem);
                            }
                        }
                    }
                }
            }
        }

        if (currentMatches.Count > 0 )
        {
            currentMatches = currentMatches.Distinct().ToList();
        }
        
        CheckForBombs();
    }

    public void CheckForBombs()
    {
        for (int i = 0; i < currentMatches.Count; i++)
        {
            Gem gem = currentMatches[i];

            int x = gem.posIndex.x;
            int y = gem.posIndex.y;

            if (gem.posIndex.x > 0)
            {
                if (board.allGems[x - 1, y] != null)
                {
                    if (board.allGems[x - 1, y].type == Gem.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x - 1, y), board.allGems[x - 1, y]);
                    }
                }
            }
            
            if (gem.posIndex.x < board.width - 1)
            {
                if (board.allGems[x + 1, y] != null)
                {
                    if (board.allGems[x + 1, y].type == Gem.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x + 1, y), board.allGems[x + 1, y]);
                    }
                }
            }
            
            if (gem.posIndex.y > 0)
            {
                if (board.allGems[x, y - 1] != null)
                {
                    if (board.allGems[x, y - 1].type == Gem.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x, y - 1), board.allGems[x, y - 1]);
                    }
                }
            }
            
            if (gem.posIndex.y < board.height - 1)
            {
                if (board.allGems[x, y + 1] != null)
                {
                    if (board.allGems[x, y + 1].type == Gem.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x, y + 1), board.allGems[x, y + 1]);
                    }
                }
            }
        }
    }

    public void MarkBombArea(Vector2Int bombPos, Gem theBomb)
    {
        for (int i = bombPos.x - theBomb.blastSize; i <= bombPos.x + theBomb.blastSize; i++)
        {
            for (int j = bombPos.y - theBomb.blastSize; j <= bombPos.y + theBomb.blastSize; j++)
            {
                if (i >= 0 && i < board.width && j >= 0 && j < board.height)
                {
                    if (board.allGems[i, j] != null)
                    {
                        board.allGems[i, j].isMatched = true;
                        currentMatches.Add(board.allGems[i, j]);
                    }
                }
            }
        }

        currentMatches = currentMatches.Distinct().ToList();
    }

#endregion

}
