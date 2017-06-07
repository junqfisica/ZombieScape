using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.IO;

public class SceneBottunManeger : MonoBehaviour {

	private ModalPanelSystem modalPanelsystem;
	private ModalPanelLanguageSelection modalPanelLanguageSelection;
	private ModalPanelItens modalPanelItens;
	private ModalPanelMenu modalPanelMenu;
	private MenuSetupLanguages menuSetupLanguages;

	private string systemInfo;
	private string langSelcText;


	void Awake(){

		modalPanelsystem = ModalPanelSystem.Instance ();
		modalPanelLanguageSelection = ModalPanelLanguageSelection.Instance ();
		modalPanelItens = ModalPanelItens.Instance ();
		modalPanelMenu = ModalPanelMenu.Instance ();
		menuSetupLanguages = MenuSetupLanguages.Instance ();

	}

	void Settings(){

		GeneralLanguageSetup.setupLanguage.SetLanguage ("selclang", out langSelcText); // Set the message error with the apropriate language
		modalPanelLanguageSelection.SelectLanguage (langSelcText,() => {SelectLanguage(0);},() => {SelectLanguage(1);},() => {SelectLanguage(2);});

	}


	void SelectLanguage(int i){
		// i = 0 Set English
		// i = 1 Set Portuguese
		// i = 2 Not Define
		GeneralLanguageSetup.setupLanguage.currentLanguage = i;
		GeneralLanguageSetup.setupLanguage.ChangeLanguages (); // call the method inside GeneralLanguageSetup to change all the languages

	}

	void PlayerSt(){

		Debug.Log ("Show Player Status");
	}

	public void Menu(){

		modalPanelMenu.OpenPanel (Settings, PlayerSt);

	}

	public void Itens(){

		modalPanelItens.OpenModalPanel ();

	}
		

	public void NewGame(){

		LevelManager.lm.LoadLevel ("Game");
	}

	public void Save(){

		GameControl.control.SaveGame ();
	}

	public void Load(){


		if (File.Exists (Application.persistentDataPath + "/saveData.dat")) {

			GameControl.control.LoadGame ();
			LevelManager.lm.LoadLevel ("Game");

		} else {

			menuSetupLanguages.SetLanguage ("infoText", out systemInfo); // Set the message error with the apropriate language
			modalPanelsystem.InfoPanel (systemInfo);
			Debug.Log (systemInfo);

		}
	}

	public void QuitGame(){

		LevelManager.lm.QuitGame ();
	}
		
		
}
