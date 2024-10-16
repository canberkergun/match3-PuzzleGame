using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

#region Fields

    public TMP_Text timeText;
    public TMP_Text scoreText;

    public GameObject roundOverScreen;

    public TMP_Text winScore;
    public TMP_Text winText;
    public GameObject winStars1, winStars2, winStars3;

    #endregion

    #region Unity Events

    private void Start()
    {
        winStars1.SetActive(false);
        winStars2.SetActive(false);
        winStars3.SetActive(false);
    }

    #endregion

    #region Private Methods

    #endregion

    #region Public Methods

    #endregion

}
