using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour {
    private GameObject camera;
    [SerializeField] private float RotationSpeed = 0.1f;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindWithTag("MainCamera");
		
	}
	
	// Update is called once per frame
	void Update () {
        camera.transform.Rotate(new Vector3(0,RotationSpeed,0));
	}
}
