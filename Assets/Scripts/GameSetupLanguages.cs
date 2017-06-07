using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

public class GameSetupLanguages : MonoBehaviour {

	public TextAsset dictonary;

	public Text textButton1;
	public Text textButton2;
	public Text textModalMCC;
	[HideInInspector]
	public string start, A1, A2, A3, A4, A5, B1, B2, B3, B4, B5, C1, C2, C3, C4, C5, C6, D1, D2, dead;
	[HideInInspector]
	public string enemyA4_1, enemyA4_2, enemyA4_3, enemyA4_4, enemyA4_5;



	private int currentLanguage;

	private string button1;
	private string button2;
	private string modalMCC;


	private List<Dictionary<string,string>> languages = new List<Dictionary<string,string>>();

	private static GameSetupLanguages gameSetupLanguage;

	public static GameSetupLanguages Instance(){

		if (!gameSetupLanguage) {

			gameSetupLanguage = FindObjectOfType (typeof(GameSetupLanguages)) as GameSetupLanguages;
			if (!gameSetupLanguage) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active GameSetupLanguages Script on a GameObject in your scene");
		}

		return gameSetupLanguage;

	}
	// Use this for initialization
	void Awake () {
		
		//Read the xml file using the dll MyXmlReader
		MyXmlReader xmlreader = gameObject.AddComponent<MyXmlReader> ();
		languages = xmlreader.ReadXml (dictonary);
		ChangeLanguages ();

	}


	public void ChangeLanguages(){

		this.currentLanguage = GeneralLanguageSetup.setupLanguage.currentLanguage;
		SetLanguage ("button1", out button1);   // The names beteween "" must to have the same name that in the xml file
		SetLanguage ("button2", out button2);
		SetLanguage ("chooseText", out modalMCC);
		SetLanguage ("start", out start);
		SetLanguage("dead", out dead);
		SetLanguage ("A1", out A1);
		SetLanguage ("A2", out A2);
		SetLanguage ("A3", out A3);
		SetLanguage ("A4", out A4);
		SetLanguage ("A5", out A5);
		SetLanguage ("B1", out B1);
		SetLanguage ("B2", out B2);
		SetLanguage ("B3", out B3);
		SetLanguage ("B4", out B4);
		SetLanguage ("B4", out B5);
		SetLanguage ("C1", out C1);
		SetLanguage ("C2", out C2);
		SetLanguage ("C3", out C3);
		SetLanguage ("C4", out C4);
		SetLanguage ("C5", out C5);
		SetLanguage ("C6", out C6);
		SetLanguage ("D1", out D1);
		SetLanguage ("D2", out D2);
		SetLanguage ("enemyA4_1", out enemyA4_1);
		SetLanguage ("enemyA4_2", out enemyA4_2);
		SetLanguage ("enemyA4_3", out enemyA4_3);
		SetLanguage ("enemyA4_4", out enemyA4_4);
		SetLanguage ("enemyA4_5", out enemyA4_5);

		SetText ();
	}

	public void SetLanguage(string name, out string value){

		languages [currentLanguage].TryGetValue (name, out value);

	}

	void SetText () {

		textButton1.text = button1;
		textButton2.text = button2;
		textModalMCC.text = modalMCC;

	}
		
}
