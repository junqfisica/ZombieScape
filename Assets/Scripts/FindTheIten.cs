using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FindTheIten : MonoBehaviour {

	public GameObject CancelClick;
	public GameObject ItenFound;
	public int imagePlace;
	public int pressedIn;

	private string nameClicked;
	private string nameItenBt;

	void Start(){

		PlaceTheIten ();

	}


	IEnumerator CancelClicks(GameObject obj){

		CancelClick.SetActive (true);
		obj.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		CancelClick.SetActive (false);
		obj.SetActive (false);
	}

	IEnumerator GotTheIten(){

		GameObject.Find ("TimeBar").SetActive (false);
		this.gameObject.GetComponent<TimeControl> ().enabled = false;
		ItenFound.SetActive (true);
		yield return new WaitForSeconds (2f);
		LevelManager.lm.LoadLevel ("Game");
		
	}

	void PlaceTheIten(){

		System.DateTime dat = System.DateTime.Now; // Gets the Time from the system
		Random.seed = dat.Millisecond; // Sets the millisencons as a seed to generate more random numbers.

		imagePlace = Random.Range (1, 16);
	}

	public void GetButton(Button bt){

		nameClicked = bt.name;
		nameItenBt = "Button" + imagePlace.ToString ();
		Debug.Log (bt.name + " was clicked");
		Debug.Log ("The iten is on " + nameItenBt);
		CheckIfThereIsIten (bt);
	}

	void CheckIfThereIsIten(Button bt){

		if (nameClicked != nameItenBt) {

			GameControl.control.findImageResult = "fail";
			GameObject btLayer = bt.transform.FindChild ("Layer").gameObject;
			StartCoroutine ("CancelClicks", btLayer);

		} else {

			GameControl.control.findImageResult = "success";
			StartCoroutine ("GotTheIten");
		}
	}
}
