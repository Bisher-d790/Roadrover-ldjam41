using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour {

    private Level1 level;

    // Use this for initialization
    void Start()
    {
        level = GameObject.FindWithTag("GameController").GetComponent<Level1>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            level.FinishLine();
        }
    }
}
