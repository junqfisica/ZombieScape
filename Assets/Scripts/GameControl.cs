using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;


public class GameControl : MonoBehaviour {

	public static GameControl control;

	#region public variables
	// IF a new State is desire it should be included in the list below 
	public enum State {start,A1,A2,A3,A4,A5,B1,B2,B3,B4,B5,C1,C2,C3,C4,C5,C6,D1,D2,dead}; // screen flow states 
	public List<string> itens = new List<string>();
	public State mystate;
	public int max_enemies_A4;

	[HideInInspector]
	public int num_enemies_A4, dice_lv;
	[HideInInspector]
	public string diceresult, findImageResult, scene_forDice;
	[HideInInspector]
	public string [] scene_forChoice;
	[HideInInspector]
	public string path1, path2, path3;
	#endregion

	public enum Battles{A4}; //States where we have battles.
	private Dictionary<Battles,int> battle;
	//private variables
	//SetButtonText setbutton;
// -------------- Methods -------------------

	void Awake () {
		
		#region Make the object persistente
		if (control == null) {

			DontDestroyOnLoad (gameObject);
			control = this;

		} else if (control != this) {

			Destroy (gameObject);
		}
		#endregion

		battle = new Dictionary<Battles,int> ();
		battle.Add (Battles.A4, max_enemies_A4); // Add to a dictionary

	}

	#region Game Save and Load methos
	public void SaveGame(){

		BinaryFormatter bf = new BinaryFormatter (); //create a binary formart
		FileStream file = File.Create (Application.persistentDataPath + "/saveData.dat"); //create a file called SavaData.dat
		PlayerData data = new PlayerData(); // Create a data object using a serializable class
		data.mystate = mystate; //pass the variable mystate to data

		bf.Serialize (file, data); //write data on file
		file.Close();
		Debug.Log ("GameSaved");
		Debug.Log (mystate);
	}

	public void LoadGame(){

		if (File.Exists (Application.persistentDataPath + "/saveData.dat")) {
			
			BinaryFormatter bf = new BinaryFormatter (); //create a binary formart
			FileStream file = File.Open (Application.persistentDataPath + "/saveData.dat", FileMode.Open); //open the file called SavaData.dat
			PlayerData data = (PlayerData)bf.Deserialize (file); //Gets the data and put on the data variable
			file.Close ();

			mystate = data.mystate;
			Debug.Log ("GameLoad");
			Debug.Log (mystate);


		} else {

			Debug.Log ("Game save doesn't exit");
		}
			
	}
	#endregion

	public void Restart (){

		Awake ();
		mystate = State.start;
		LevelManager.lm.LoadLevel ("Menu"); 

	}

	//=======================================================================
	///<summary>
	///This method calls the scene Challenge, set its level and start it.
	///</summary>
	///<param name="name">Name of the Scene to be returned.</param>
	///<param name="lv">dificult level of the challenge. From 1 to 4.</param>
	//=======================================================================
	public void RunTheDice(string name, int lv) {
		
		diceresult = null;
		scene_forDice = name;
		dice_lv = lv;
		LevelManager.lm.LoadLevel ("Challenge");
	}

	//===========================================================================
	/// <summary>
	/// Finds the iten in the scene FindImage.
	/// </summary>
	/// <param name="st1">State in the case of sucees.</param>
	/// <param name="st2">State in the case of fail.</param>
	//===========================================================================
	public void FindItenInTheScene(){

		findImageResult = null;
		LevelManager.lm.LoadLevel ("FindImage");

	}

	#region Obsolet part of the code
	/*
	public void MakeThreeChoice(string name, State st1, State st2, State st3){
		/* name = Scene name where you want to return after the choice
		 * st1 = choice number 1 for change the state
		 * st2 = Choice number 2 for change the state
		
		scene_forChoice = new string[3];
		scene_forChoice[0] = name;
		scene_forChoice[1] = name;
		scene_forChoice[2] = name;

		path1 = st1.ToString ();
		path2 = st2.ToString ();
		path3 = st3.ToString ();

		LevelManager.lm.LoadLevel("ThreeChoices");

	}

	public void MakeTwoChoice(string name, State st1, State st2){
		/* name = Scene name where you want to return after the choice
		 * st1 = choice number 1 for change the state
		 * st2 = Choice number 2 for change the state
		
		scene_forChoice = new string[2];
		scene_forChoice[0] = name;
		scene_forChoice[1] = name;

		path1 = st1.ToString ();
		path2 = st2.ToString ();
		LevelManager.lm.LoadLevel("TwoChoices");

	}


	public void MakeTwoChoiceGoChallenge(string name, State st1, State st2, int lv){
		/ name = Scene name where you want to return if you choose 2nd option
		  st1 = choice number 1 for change the state
		  st2 = Choice number 2 for change the state
		  lv = level for the challenge
		/
		scene_forChoice = new string[2];
		scene_forChoice[0] = "Challenge";
		scene_forChoice [1] = name;

		diceresult = null;
		scene_forDice = name;
		dice_lv = lv;

		path1 = st1.ToString ();
		path2 = st2.ToString ();

		LevelManager.lm.LoadLevel("TwoChoices");

    }*/
	#endregion
	//=============================================================
	/// <summary>
	/// Check a given battle state, then return the 
	/// number of enemies alive in that state, depending on the dice results.
	/// </summary>
	/// <returns> The <c>int</c> number of enemies still alive in a given state.</returns>
	/// <param name="st"> Name of the <c>enum</c> Battles</param>
	//=============================================================
	public int BattleZumbies(Battles st){

		if (!battle.ContainsKey (st))
			Debug.Log ("Error, this battle do not exit");
	
	
		if (diceresult == "success") {

			diceresult = null;
			battle [st]--;
			num_enemies_A4--;  // subtract one enemy

		} else if (diceresult == "fail"){

			diceresult = null;  // set result to null
			mystate = State.dead;
		}

		return battle[st];

	}

}
	
// This class will hold the information to be saved. It has to be serializable
[Serializable]
class PlayerData{

	public GameControl.State mystate;
}

[Serializable]
public class VerionData{

	public string gameVersion;
}
