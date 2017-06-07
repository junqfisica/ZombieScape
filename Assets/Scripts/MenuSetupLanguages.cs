using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;



public class MenuSetupLanguages : MonoBehaviour {

	public TextAsset dictonary;

	public Text TextButton1;
	public Text TextButton2;
	public Text TextButton3;
	 
	private string button1;  
	private string button2; 
	private string button3;  

	private int currentLanguage;

	private List<Dictionary<string,string>> languages = new List<Dictionary<string,string>>();

	#region Make the Script public and assure that it will exites in the scene
	private static MenuSetupLanguages menuSetupLanguages;

	public static MenuSetupLanguages Instance(){

		if (!menuSetupLanguages) {

			menuSetupLanguages = FindObjectOfType (typeof(MenuSetupLanguages)) as MenuSetupLanguages;
			if (!menuSetupLanguages) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active MenuSetupLanguages Script on a GameObject in your scene");
		}

		return menuSetupLanguages;
	}
	#endregion

	void Awake () {
		
		//Read the xml file using the dll MyXmlReader
		MyXmlReader xmlreader = gameObject.AddComponent<MyXmlReader> ();
		languages = xmlreader.ReadXml (dictonary);
	
	}

	void Update () {

		this.currentLanguage = GeneralLanguageSetup.setupLanguage.currentLanguage;
		SetLanguage ("button1", out button1); // The names beteween "" must to have the same name that in the xml file
		SetLanguage ("button2", out button2);
		SetLanguage ("button3", out button3);

		SetBottunText ();

	}

	public void SetLanguage(string name, out string value){

		languages [currentLanguage].TryGetValue (name, out value);

	}

	void SetBottunText () {

		TextButton1.text = button1;
		TextButton2.text = button2;
		TextButton3.text = button3;
	}
		
}
