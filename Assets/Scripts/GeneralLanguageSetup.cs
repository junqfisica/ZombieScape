using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GeneralLanguageSetup : MonoBehaviour {

	public static GeneralLanguageSetup setupLanguage;

	public TextAsset dictonary;

	public string usingLanguage;
	public string userLanguage;
	public int currentLanguage;

	private GameSetupLanguages gameSetupLanguages;

	private List<Dictionary<string,string>> languages = new List<Dictionary<string,string>>();

	void Awake () {

		#region Make the object persistent
		if (setupLanguage == null) {

			DontDestroyOnLoad (gameObject);
			setupLanguage = this;

		} else if (setupLanguage != this) {

			Destroy (gameObject);
		}
		#endregion

		FindGameSetupLangue ();

		// Set initial language
		userLanguage = Application.systemLanguage.ToString(); // Gets the language from the user's system

		//Read the xml file using the dll MyXmlReader
		MyXmlReader xmlreader = gameObject.AddComponent<MyXmlReader> ();
		languages = xmlreader.ReadXml (dictonary);

		// Set initial language
		SetUserLanguage ();

	}

	void OnLevelWasLoaded(){ // Garantie that after the scene is load the object is there

		FindGameSetupLangue ();

	}
		
	void FindGameSetupLangue(){

		#region Find GameSetupLanguages Langues Components
		gameSetupLanguages = FindObjectOfType (typeof(GameSetupLanguages)) as GameSetupLanguages;
		if (gameSetupLanguages) 
			gameSetupLanguages = GameSetupLanguages.Instance ();
		#endregion

	}

	void SetUserLanguage() {

		if (PlayerPrefs.HasKey ("UserLanguage")) {

			currentLanguage = PlayerPrefs.GetInt ("UserLanguage");

		} else {

			// This method check if the user language is within the language xml file..if is we set the user language if not 
			// we set the defout language.
			int i = 0 ; 
			foreach (Dictionary<string,string> dic  in languages ){

				if (dic.ContainsValue (userLanguage)) {

					currentLanguage = i;
				}

				i++;
			}

		}

		SetLanguage ("Name", out usingLanguage); // The names beteween "" must to have the same name that in the xml file
	}

	public void ChangeLanguages(){

		SetLanguage ("Name", out usingLanguage); // The names beteween "" must to have the same name that in the xml file

		// Save Player preference when language is changed
		PlayerPrefs.SetInt("UserLanguage",currentLanguage);
		PlayerPrefs.Save ();

		if (gameSetupLanguages)
			gameSetupLanguages.ChangeLanguages ();
	}

	public void SetLanguage(string name, out string value){

		languages [currentLanguage].TryGetValue (name, out value);

	}
}
