using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanelSystem : MonoBehaviour {

	public Text info;
	public Button closeWindow;
	public GameObject modalPanelObject;

	private static ModalPanelSystem modalPanel;


	public static ModalPanelSystem Instance(){

		if (!modalPanel) {

			modalPanel = FindObjectOfType (typeof(ModalPanelSystem)) as ModalPanelSystem;
			if (!modalPanel) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active ModalPanel Script on a GameObject in your scene");
		}

		return modalPanel;
	}

	void Start(){ // This is necessary to enable the checkbox on the inspector
	}

	public void InfoPanel (string info){

		modalPanelObject.SetActive (true);
		closeWindow.onClick.RemoveAllListeners (); //remove possible events attached to it.
		closeWindow.onClick.AddListener(ClosePanel);

		this.info.text = info;

	}
		

	void ClosePanel(){
		
		modalPanelObject.SetActive (false);
	}
}
