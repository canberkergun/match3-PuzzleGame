using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

#region Fields

	public string levelToLoad;
	
	
#endregion

#region Unity Events
	
#endregion

#region Private Methods
	
#endregion

#region Public Methods

	public void StartGame()
	{
		SceneManager.LoadScene(levelToLoad);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
	
#endregion

}
