using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanelLanguageSelection : MonoBehaviour {

	public Text text;
	public Button language1;
	public Button language2;
	public Button language3;
	public GameObject modalPanelObject;

	private static ModalPanelLanguageSelection modalPanel;

	public static ModalPanelLanguageSelection Instance(){

		if (!modalPanel) {

			modalPanel = FindObjectOfType (typeof(ModalPanelLanguageSelection)) as ModalPanelLanguageSelection;
			if (!modalPanel) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active ModalPanel Script on a GameObject in your scene");
		}

		return modalPanel;
	}

	void Start(){ // This is necessary to enable the checkbox on the inspector
	}

	public void SelectLanguage (string text, UnityAction Event1,UnityAction Event2,UnityAction Event3){

		modalPanelObject.SetActive (true);

		language1.onClick.RemoveAllListeners (); //remove possible events attached to it.
		language1.onClick.AddListener(Event1);
		language1.onClick.AddListener(ClosePanel);

		language2.onClick.RemoveAllListeners (); //remove possible events attached to it.
		language2.onClick.AddListener(Event2);
		language2.onClick.AddListener(ClosePanel);

		language3.onClick.RemoveAllListeners (); //remove possible events attached to it.
		language3.onClick.AddListener(Event3);
		language3.onClick.AddListener(ClosePanel);

		this.text.text = text;

		// Set the buttons as Active or not
		language1.gameObject.SetActive (true);
		language2.gameObject.SetActive (true);
		language3.gameObject.SetActive (false); // The thirth button is not active

	}

	void ClosePanel(){
		
		modalPanelObject.SetActive (false);
	}
}
