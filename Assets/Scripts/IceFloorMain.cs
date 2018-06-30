using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IceFloorMain : MonoBehaviour {
	public GameObject camCube, camSet, goalSet, goal1Set, goal2Set, poisonTag, getTag, get1Tag, get2Tag, exitSet, CamBody, GW, gwY, gwB, gwR;
	public GameObject[] pr = new GameObject[12]; 
	float posX, posZ, moveX, moveZ, camRot;
	int pXi, pZi;
	bool canMove = true, poison = false, goal = false, goal1 = false, goal2 = false, prj1 = true, prj2 = false;

//	Quaternion Gyro;

	// Use this for initialization
	void Start () {
		moveX = 0;
		moveZ = 0;

//		Input.gyro.enabled = true;
	}
	// Update is called once per frame
	void Update () {
//		Gyro = Input.gyro.attitude;
//		Gyro.x *= -1;
//		Gyro.y *= -1;
//		Camera.main.transform.localRotation = Quaternion.Euler(90, 0, -90) * Gyro;

		if (Input.GetKey (KeyCode.UpArrow) && (Camera.main.transform.localEulerAngles.x > 335 || Camera.main.transform.localEulerAngles.x < 90)) {
			Camera.main.transform.Rotate (-2, 0, 0);
		}
		if (Input.GetKey (KeyCode.DownArrow) && (Camera.main.transform.localEulerAngles.x > 325 || Camera.main.transform.localEulerAngles.x < 80)) {
			Camera.main.transform.Rotate (2, 0, 0);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			CamBody.transform.Rotate (0, -2, 0);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			CamBody.transform.Rotate (0, 2, 0);
		}

//		Debug.Log (Camera.main.transform.localEulerAngles.x);

		camSet.transform.position += new Vector3 (moveX, 0, moveZ);
        if ((Input.GetKeyUp (KeyCode.C) || Input.GetKeyUp (KeyCode.Z) || Input.GetKeyUp(KeyCode.Space)) && canMove == true) {
			camRot = CamBody.transform.localEulerAngles.y;
//			Debug.Log (camRot);
			if (camRot > 225 && camRot < 315) {
				moveX = -1.5f;
			} else if (camRot > 135 && camRot < 225) {
				moveZ = -1.5f;
			} else if (camRot > 45 && camRot < 135) {
				moveX = 1.5f;
			} else if (camRot > 315 || camRot < 45) {
				moveZ = 1.5f;
			}

			if (poison == true) {
				poisonTag.SetActive (false);
			}

			canMove = false;
		}

		for (int i = 0; i < 6; i++) {
			if (prj1 == true) {
				pr [i].transform.position += new Vector3(0, 0.03f, 0);
			} else if (prj1 == false) {
				pr [i].transform.position -= new Vector3(0, 0.03f, 0);
			}
		}
		for (int i = 6; i < 12; i++) {
			if (prj2 == true) {
				pr [i].transform.position += new Vector3(0, 0.03f, 0);
			} else if (prj2 == false) {
				pr [i].transform.position -= new Vector3(0, 0.03f, 0);
			}
		}
		if (pr [0].transform.position.y >= 1.3f) {
			prj1 = false;
			prj2 = true;
		} else if (pr [0].transform.position.y <= -0.2f) {
			prj1 = true;
			prj2 = false;
		}

		if (Input.GetKey (KeyCode.C) && Input.GetKey (KeyCode.Z)) {
			SceneManager.LoadScene("IceFloorStart");
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Stone") {
			posX = camSet.transform.position.x / 10;
			posZ = camSet.transform.position.z / 10;
			pXi = (int)posX;
			pZi = (int)posZ;
			if (posX > 0) {
				posX = (float)pXi * 10 + 5;
			} else if (posX < 0) {
				posX = (float)pXi * 10 - 5;
			}
			if (posZ > 0) {
				posZ = (float)pZi * 10 + 5;
			} else if (posZ < 0) {
				posZ = (float)pZi * 10 - 5;
			}
			camSet.transform.position = new Vector3 (posX, 0, posZ);
			moveX = 0;
			moveZ = 0;
			canMove = true;
		}

		if (other.gameObject.tag == "Poison") {
			poison = true;
			camSet.transform.position = new Vector3 (-15, 0, -55);
			moveX = 0;
			moveZ = 0;
			goal = false; goal1 = false; goal2 = false;
			goalSet.SetActive (true); goal1Set.SetActive (true); goal2Set.SetActive (true);
			poisonTag.SetActive (true);
			getTag.SetActive (false); get1Tag.SetActive (false); get2Tag.SetActive (false);
			exitSet.SetActive (false);
			gwY.SetActive (true); gwB.SetActive (true); gwR.SetActive (true);
			GW.SetActive (true);
			canMove = true;
		}

		if (other.gameObject.tag == "Goal") {
			goal = true;
			getTag.SetActive (true);
			goalSet.SetActive (false);
			gwY.SetActive (false);
			if (goal1 == true && goal2 == true) {
				exitSet.SetActive (true);
				GW.SetActive (false);
			}
		}

		if (other.gameObject.tag == "Goal1") {
			goal1 = true;
			get1Tag.SetActive (true);
			goal1Set.SetActive (false);
			gwB.SetActive (false);
			if (goal == true && goal2 == true) {
				exitSet.SetActive (true);
				GW.SetActive (false);
			}
		}

		if (other.gameObject.tag == "Goal2") {
			goal2 = true;
			get2Tag.SetActive (true);
			goal2Set.SetActive (false);
			gwR.SetActive (false);
			if (goal1 == true && goal == true) {
				exitSet.SetActive (true);
				GW.SetActive (false);
			}
		}

		if (other.gameObject.tag == "Exit") {
			SceneManager.LoadScene("IceFloorGoal");
		}
	}
}
