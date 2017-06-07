using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {


		public Camera[] cameras;
		public Canvas gamecanvas;
		public Canvas mapcanvas;


		void Start (){

			// set the cameras on and off
			cameras [0].gameObject.SetActive (true);    // main camera
			cameras [1].gameObject.SetActive (true);    // minimap camera
			cameras [2].gameObject.SetActive (false);   // map camera
			//set the canvas on and off
			gamecanvas.gameObject.SetActive (true);    // game canvas 
			mapcanvas.gameObject.SetActive (false);      // map canvas

		}
		

		public void MapCam(){

			// set the cameras on and off
			cameras [2].gameObject.SetActive (true);    // map camera
			cameras [0].gameObject.SetActive (false);   // main camera
			cameras [1].gameObject.SetActive (false);   // minimap camera
			//set the canvas on and off
			gamecanvas.gameObject.SetActive (false);    // game canvas 
			mapcanvas.gameObject.SetActive (true);      // map canvas
		}

		public void Return(){

			// set the cameras on and off
			cameras [0].gameObject.SetActive(true);
			cameras [1].gameObject.SetActive(true);
			cameras [2].gameObject.SetActive (false);
			// set the canvas on and off
			gamecanvas.gameObject.SetActive (true);    
			mapcanvas.gameObject.SetActive (false);    

		}
}

