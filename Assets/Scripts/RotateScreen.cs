using UnityEngine;
using System.Collections;

public class RotateScreen : MonoBehaviour {


	void Awake(){

		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
		
}
