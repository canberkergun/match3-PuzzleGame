using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SFXManager : MonoBehaviour
{

#region Fields

    public static SFXManager instance;
    public AudioSource gemSound, explodeSound, stoneSound, roundOverSound;
    
#endregion

#region Unity Events

    private void Awake()
    {
        instance = this;
    }

#endregion

#region Private Methods

#endregion

#region Public Methods

    public void PlayGemBreak()
    {
        gemSound.Stop();

        gemSound.pitch = Random.Range(.8f, 1.2f);
        
        gemSound.Play();
    }
    public void PlayExplode()
    {
        explodeSound.Stop();

        explodeSound.pitch = Random.Range(.8f, 1.2f);
        
        explodeSound.Play();
    }
    public void PlayStoneBreak()
    {
        stoneSound.Stop();

        stoneSound.pitch = Random.Range(.8f, 1.2f);
        
        stoneSound.Play();
    }
    public void PlayRoundOver()
    {
        roundOverSound.Play();
    }

#endregion

}
