using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gem : MonoBehaviour
{
	
#region Fields

	public enum GemType
	{
		blue,
		green,
		red,
		yellow,
		purple,
		bomb
	}

	public GemType type;
	public bool isMatched;
	
	[HideInInspector]
	public Vector2Int posIndex;
	[HideInInspector]
	public Board board;


	private Vector2 firstTouchPosition;
	private Vector2 lastTouchPosition;

	private bool mousePressed;
	private float swipeAngle = 0;

	private Gem otherGem;
	
	public GameObject destroyEffect;

	public int blastSize = 2;
	
	[HideInInspector]
	public Vector2Int previousPos;

	public int scoreValue = 10;

	#endregion

#region Unity Events

	private void Update()
	{
		if (Vector2.Distance(transform.position, posIndex) > .01f)
		{
			transform.position = Vector2.Lerp(transform.position, posIndex, board.gemSpeed * Time.deltaTime);
		}
		else
		{
			transform.position = new Vector3(posIndex.x, posIndex.y);
		}
		
		if (mousePressed && Input.GetMouseButtonUp(0))
		{
			mousePressed = false;

			if (board.currentState == Board.BoardState.move)
			{
				lastTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				CalculateAngle();
			}
		}
	}

#endregion

#region Private Methods
	private void OnMouseDown()
	{
		if (board.currentState == Board.BoardState.move && board.roundMan.roundTime > 0)
		{
			firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePressed = true;
		}
	}

	private void CalculateAngle()
	{
		swipeAngle = Mathf.Atan2(lastTouchPosition.y - firstTouchPosition.y, lastTouchPosition.x - firstTouchPosition.x);
		swipeAngle = swipeAngle * 180 / Mathf.PI;

		if (Vector3.Distance(firstTouchPosition,lastTouchPosition) > 0.5f)
		{
			MovePieces();
		}
		
	}

	private void MovePieces()
	{
		previousPos = posIndex;
		
		if (swipeAngle < 45 && swipeAngle > -45 && posIndex.x < board.width - 1)
		{
			otherGem = board.allGems[posIndex.x + 1, posIndex.y];
			otherGem.posIndex.x--;
			posIndex.x++;
		}
		else if (swipeAngle > 45 && swipeAngle <= 135 && posIndex.y < board.height - 1)
		{
			otherGem = board.allGems[posIndex.x, posIndex.y + 1];
			otherGem.posIndex.y--;
			posIndex.y++;
		}
		else if (swipeAngle < -45 && swipeAngle >= -135 && posIndex.y  > 0)
		{
			otherGem = board.allGems[posIndex.x, posIndex.y - 1];
			otherGem.posIndex.y++;
			posIndex.y--;
		}
		else if(swipeAngle > 135 || swipeAngle < -135 && posIndex.x > 0)
		{
			otherGem = board.allGems[posIndex.x - 1, posIndex.y];
			otherGem.posIndex.x++;
			posIndex.x--;
		}

		board.allGems[posIndex.x, posIndex.y] = this;
		board.allGems[otherGem.posIndex.x, otherGem.posIndex.y] = otherGem;

		StartCoroutine(CheckMoveCo());
	}

	private IEnumerator CheckMoveCo()
	{
		board.currentState = Board.BoardState.wait;
		
		yield return new WaitForSeconds(.5f);
		
		board.matchFind.FindAllMatches();

		if (otherGem != null)
		{
			if (!isMatched && !otherGem.isMatched)
			{
				otherGem.posIndex = posIndex;
				posIndex = previousPos;

				board.allGems[posIndex.x, posIndex.y] = this;
				board.allGems[otherGem.posIndex.x, otherGem.posIndex.y] = otherGem;

				yield return new WaitForSeconds(.5f);

				board.currentState = Board.BoardState.move;
			}
			else
			{
				board.DestroyMatches();
			}
		}
	}
	
#endregion

#region Public Methods

	public void SetupGem(Vector2Int pos, Board theBoard)
	{
		posIndex = pos;
		board = theBoard;
	}

	

	#endregion

}
