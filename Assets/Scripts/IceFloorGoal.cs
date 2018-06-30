using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IceFloorGoal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.C) && Input.GetKey (KeyCode.Z)) {
			SceneManager.LoadScene("IceFloorStart");
		}
	}
}
