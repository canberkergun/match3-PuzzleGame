using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
	
#region Fields

	[HideInInspector]
	public Vector2Int posIndex;
	[HideInInspector]
	public Board board;

#endregion

#region Unity Events

#endregion

#region Private Methods

#endregion

#region Public Methods

	public void SetupGem(Vector2Int pos, Board theBoard)
	{
		posIndex = pos;
		board = theBoard;
	}

#endregion

}
