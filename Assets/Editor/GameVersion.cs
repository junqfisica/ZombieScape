using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[InitializeOnLoad]
public class GameVersion: MonoBehaviour {

	static string version;

	static GameVersion() {
		
		GetVersion ();

	}
	static void  GetVersion (){

		version = PlayerSettings.bundleVersion.ToString ();
		SaveVersion ();

	}

	static void SaveVersion(){

		BinaryFormatter bf = new BinaryFormatter (); //create a binary formart
		FileStream file = File.Create (Application.persistentDataPath + "/Version.dat"); //create a file called SavaData.dat
		VerionData data = new VerionData(); // Create a data object using a serializable class
		data.gameVersion = version; //pass the variable mystate to data

		bf.Serialize (file, data); //write data on file
		file.Close();
	}
		
}