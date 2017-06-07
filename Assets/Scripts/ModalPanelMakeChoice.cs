using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanelMakeChoice : MonoBehaviour {

	public GameObject modalPanelObject;
	public Text infoText;
	public Text timebarText;
	public Button choice1;
	public Button choice2;
	public Button choice3;
	public AudioSource audioSource;

	private AudioController audioController; 


	private static ModalPanelMakeChoice modalPanel;

	public static ModalPanelMakeChoice Instance(){

		if (!modalPanel) {

			modalPanel = FindObjectOfType (typeof(ModalPanelMakeChoice)) as ModalPanelMakeChoice;
			if (!modalPanel) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active ModalPanelMakeChoice Script on a GameObject in your scene");
		}

		return modalPanel;
	}

	void Awake(){ // This is necessary to enable the checkbox on the inspector

		audioController = AudioController.Instance ();
	}

	//=====================================================================
	/// <summary>
	/// Makes your choice. This method call the modal panel MakeChoice. 
	/// </summary>
	/// <param name="isThree">If set <c>true</c> you have 3 options otherwise you have only 2.</param>
	/// <param name="Events">Events. You can enter with a method in here.</param>
	//=====================================================================
	public void MakeYourChoice(bool isThree,params UnityAction [] Events){


		// Add listener on button1
		choice1.onClick.RemoveAllListeners ();
		choice1.onClick.AddListener (Events[0]);
		choice1.onClick.AddListener (ClosePanel);

		// Add listener on button 2
		choice2.onClick.RemoveAllListeners ();
		choice2.onClick.AddListener (Events[1]);
		choice2.onClick.AddListener (ClosePanel);

		// Set the buttons as Active or not
		choice1.gameObject.SetActive (true);
		choice2.gameObject.SetActive (true);

		if (isThree){
			// Add listener on button 3
			choice3.onClick.RemoveAllListeners ();
			choice3.onClick.AddListener (Events[2]);
			choice3.onClick.AddListener (ClosePanel);
			choice3.gameObject.SetActive (true); // Turn on the button
		} else {
			choice3.gameObject.SetActive (false); // Turn off the button
		} 
	}

	//=============================================================
	/// <summary>
	/// Sets the buttons' text for the choice.
	/// </summary>
	/// <param name="isthree">If set to <c>true</c> you have 3 options otherwise you have only 2.</param>
	/// <param name="paths">The string name of the <c>State</c> that you can choose.</param>
	//=============================================================
	public void SetButtonsText(bool isthree,params string[] paths){
		
		modalPanelObject.SetActive (true);
		PlaySong ();
		audioController.PausePlayBackground (); // Pause the main sound

		choice1.gameObject.GetComponentInChildren <Text> ().text = paths[0];
		choice2.gameObject.GetComponentInChildren <Text> ().text = paths[1];

		if(isthree)
			choice3.gameObject.GetComponentInChildren <Text> ().text = paths[2];
	}

	void ClosePanel(){

		StopSong ();
		audioController.UnPausePlayBackground ();
		modalPanelObject.SetActive (false);
	}

	void PlaySong(){

		audioSource.Play ();
	}

	void StopSong(){

		audioSource.Stop ();
	}
		
}
