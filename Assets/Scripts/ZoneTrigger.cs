using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{

    [SerializeField] private PrefabInstantiator[] Instantiators;
    [SerializeField] private PrefabInstantiator[] Destroyees;
    private GameObject[] Enemies;
    [SerializeField] private float waitToKillTimeout = 3;




    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < Destroyees.Length; i++)
        {
            Destroyees[i].enabled = false;
        }
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        StartCoroutine(destroyEnemies());
        for (int i = 0; i < Instantiators.Length; i++) {
            Instantiators[i].enabled = true;
        }
    }


    private IEnumerator destroyEnemies()
    {
        yield return new WaitForSeconds(waitToKillTimeout);
        for (int i = 0; i < Enemies.Length; i++)
        {
            if(Enemies[i])
            Enemies[i].GetComponent<Enemy>().Shot(-1000);
        }
    }

}