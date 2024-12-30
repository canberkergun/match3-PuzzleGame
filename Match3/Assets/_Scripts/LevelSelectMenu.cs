using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{

#region Fields
	public string mainMenu = "MainMenu";
#endregion

#region Unity Events
	
#endregion

#region Private Methods
	
#endregion

#region Public Methods

	public void GoToMainMenu()
	{
		SceneManager.LoadScene(mainMenu);
	}
#endregion

}
