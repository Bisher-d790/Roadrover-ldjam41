using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : MonoBehaviour
{

    [SerializeField]private float time = 0;
    private GameManager gm;
    [SerializeField] private GameObject[] EnemyBases = new GameObject[5];
    [SerializeField] private float[] TimeOfOpenEachBase = new float[5];
    [SerializeField] private float WinTime = 500f;
    [SerializeField] private GameObject RemainingTimeTextField;


    private int nextBase = 0;


    // Use this for initialization
    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        for (int i = 0; i < EnemyBases.Length; i++)
        {
            EnemyBases[i].GetComponent<PrefabInstantiator>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RemainingTimeTextField.GetComponent<UnityEngine.UI.Text>().text = (" "+Mathf.FloorToInt(WinTime-time));
        time += Time.deltaTime;

        if (nextBase < EnemyBases.Length &&  time >= TimeOfOpenEachBase[nextBase])
        {
            EnemyBases[nextBase].GetComponent<PrefabInstantiator>().enabled = true;
            nextBase++;
        }

        if (time >= WinTime)
        {
            gm.GameWin();
        }


    }

}
