using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GetGameVersion : MonoBehaviour {

	public Text gameVersion;
	public string version;


	void Start (){
		LoadVersionData ();
	}

	public void LoadVersionData(){

		if (File.Exists (Application.persistentDataPath + "/Version.dat")) {

			BinaryFormatter bf = new BinaryFormatter (); //create a binary formart
			FileStream file = File.Open (Application.persistentDataPath + "/Version.dat", FileMode.Open); //open the file called SavaData.dat
			VerionData data = (VerionData)bf.Deserialize (file); //Gets the data and put on the data variable
			file.Close ();

			version = data.gameVersion;
			Debug.Log ("Version " + version + " Loaded");
			SetText ();

		} else {

			Debug.Log ("Data doesn't exit");
		}

	}

	void SetText(){

		gameVersion.text = "V " + version;
	}


}
