using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Mainmenu : MonoBehaviour {

    [SerializeField] private int raceLevel;
    [SerializeField] private int survivalLevel;


    public void Survival()
    {
        SceneManager.LoadScene(survivalLevel);
    }

    public void Race()
    {
        SceneManager.LoadScene(raceLevel);
    }
}
