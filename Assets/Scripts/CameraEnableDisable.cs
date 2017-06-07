using UnityEngine;
using System.Collections;

public class CameraEnableDisable : MonoBehaviour {

	void OnEnable(){

		GetComponent<AudioListener> ().enabled = true;

	}

	void OnDisable(){

		GetComponent<AudioListener> ().enabled = false;
	}
}
