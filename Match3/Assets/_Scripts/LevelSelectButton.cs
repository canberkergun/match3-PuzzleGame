using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{

#region Fields

    public string levelToLoad;
    public GameObject star1, star2, star3;

#endregion

#region Unity Events

    private void Start()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        if (PlayerPrefs.HasKey(levelToLoad + "_Star1"))
        {
            star1.SetActive(true);
        }
        if (PlayerPrefs.HasKey(levelToLoad + "_Star2"))
        {
            star2.SetActive(true);
        }
        if (PlayerPrefs.HasKey(levelToLoad + "_Star3"))
        {
            star3.SetActive(true);
        }
    }

#endregion

#region Private Methods

#endregion

#region Public Methods

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
#endregion

}
