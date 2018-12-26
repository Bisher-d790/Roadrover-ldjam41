using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{

    [SerializeField] private float time = 0;
    private GameManager gm;
    [SerializeField] private GameObject TimeText;


    // Use this for initialization
    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        time++;
        TimeText.GetComponent<UnityEngine.UI.Text>().text = (" " + time);
        StartCoroutine(Timer());
    }

    public void FinishLine()
    {
        gm.GameWin();
    }

}
