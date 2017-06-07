using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanelMenu : MonoBehaviour {
	
	public GameObject modalPanelObject;
	public Button resume;
	public Button idiom;
	public Button playerSt;

	private static ModalPanelMenu modalPanel;

	public static ModalPanelMenu Instance(){

		if (!modalPanel) {

			modalPanel = FindObjectOfType (typeof(ModalPanelMenu)) as ModalPanelMenu;
			if (!modalPanel) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active ModalPanelMenu Script on a GameObject in your scene");
		}

		return modalPanel;
	}

	public void OpenPanel(UnityAction Event1, UnityAction Event2){

		modalPanelObject.SetActive (true);

		resume.onClick.RemoveAllListeners ();
		resume.onClick.AddListener (ClosePanel);

		idiom.onClick.RemoveAllListeners ();
		idiom.onClick.AddListener (Event1);
		idiom.onClick.AddListener (ClosePanel);

		playerSt.onClick.RemoveAllListeners ();
		playerSt.onClick.AddListener (Event2);
		playerSt.onClick.AddListener (ClosePanel);

	}

	void ClosePanel(){

		modalPanelObject.SetActive (false);
	}
}
