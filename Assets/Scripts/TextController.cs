using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class TextController : MonoBehaviour {
	

	//public variables
	public Text text;
	public Button continueBut;

	//private variables
	private bool playDeadAudio = true;
	private enum SubState{A1_0,B1_0,B2_0,D1_0,D1_1,D1_2,D1_3};
	public enum Dead{Deafout,TimeOut,A3,A4,B4,C2,C5};
	private enum ItemList{knife,bone};
	private List<SubState> mySubState = new List<SubState>();
	public Dead myDead;

	private GameSetupLanguages gameSetupL;
	private ModalPanelMakeChoice makeChoicePanel;

	void Start () {
		
		gameObject.GetComponent<AudioController> ().PlayBackground ();
		gameSetupL = GameSetupLanguages.Instance ();
		makeChoicePanel = ModalPanelMakeChoice.Instance ();
	}

	
	// Update is called once per frame
	void Update () {

		GoToState ();

	}
		

	void GoToState(){


		switch (GameControl.control.mystate){

		case GameControl.State.start:
			StateStart ();
			break;

		case GameControl.State.dead:
			StateDead ();
			break;

		case GameControl.State.A1:
			StateA1 ();
			break;

		case GameControl.State.A2:
			StateA2 ();
			break;

		case GameControl.State.A3:
			StateA3 ();
			break;

		case GameControl.State.A4:
			StateA4 ();
			break;

		case GameControl.State.A5:
			StateA5 ();
			break;

		case GameControl.State.B1: 
			StateB1 ();
			break;

		case GameControl.State.B2:
			StateB2 ();
			break;

		case GameControl.State.B3:
			StateB3 ();
			break;
		
		case GameControl.State.B4: 
			StateB4 ();
			break;

		case GameControl.State.B5:
			StateB5 ();
			break;
		
		case GameControl.State.C1:
			StateC1 ();
			break;

		case GameControl.State.C2:
			StateC2 ();
			break;
		
		case GameControl.State.C3:
			StateC3 ();
			break;

		case GameControl.State.C4:
			StateC4 ();
			break;

		case GameControl.State.C5:
			StateC5 ();
			break;

		case GameControl.State.C6:
			StateC6 ();
			break;
		
		case GameControl.State.D1:
			StateD1 ();
			break;

		case GameControl.State.D2:
			StateD2 ();
			break;
		}
			
	}

	#region Usefull Methods
	//==================================================================
	/// <summary>
	/// This function says what to do when the continue button is clicked.
	/// </summary>
	/// <param name="Event">Event. Event1 can be any method or function</param>
	//==================================================================
	void Continue(UnityAction Event){
		
		continueBut.onClick.RemoveAllListeners ();
		if (Event != null)
			continueBut.onClick.AddListener (Event); //Add a listener to the continue button
	}

	//==================================================
	/// <summary>
	/// Goes to a given State.
	/// </summary>
	/// <param name="next">The State to go.</param>
	//==================================================
	void GoNextLevel(GameControl.State next){

		GameControl.control.mystate = next;

	}
		
	//==========================================================================
	/// <summary>
	/// Checks the dice go to next level for success or falilure.
	/// </summary>
	/// <param name="success"> State for Success.</param>
	/// <param name="fail">State for Fail.</param>
	//==========================================================================
	void CheckDiceGoToNextLevel(GameControl.State success, GameControl.State fail){

		if (GameControl.control.diceresult == "success") {

			GameControl.control.diceresult = null;
			GameControl.control.mystate = success;

		} else if (GameControl.control.diceresult == "fail"){

			GameControl.control.diceresult = null;
			GameControl.control.mystate = fail;
		}
	}

	//==============================================================================
	/// <summary>
	/// Checks the find image to go to next level for success or falilure.
	/// </summary>
	/// <param name="success"> State for Success.</param>
	/// <param name="fail">State for Fail.</param>
	//==============================================================================
	void CheckFindImageToNextLevel(GameControl.State success, GameControl.State fail){

		if (GameControl.control.findImageResult == "success") {

			GameControl.control.findImageResult = null;
			GameControl.control.mystate = success;

		} else if (GameControl.control.findImageResult == "fail"){

			GameControl.control.findImageResult = null;
			GameControl.control.mystate = fail;
		}
	}

	//=============================================================
	/// <summary>
	/// Set your choices for the next level. This method sets the possible options for the buttons, 
	/// then turn the choice panel on.
	/// </summary>
	/// <param name="st">State []. States choices for the next level.</param>
	//=============================================================
	//void MakeYourChoice(int numberOfChoice, GameControl.State st1, GameControl.State st2, GameControl.State st3){
	void MakeYourChoice(params GameControl.State [] st){	

		int lenght = st.Length;
		string[] pt = new string[lenght];
		myDead = Dead.TimeOut; // in the case the player died in the choice panel.

		for (int i = 0; i < lenght; i++) {
			pt[i] = st[i].ToString ();
		}

		if(lenght == 2){
			makeChoicePanel.SetButtonsText(false,pt[0], pt[1]);
			makeChoicePanel.MakeYourChoice (false,() => {AfterChoiceGoTo(pt[0]);},() => {AfterChoiceGoTo(pt[1]);});
		} else if (lenght == 3){
			makeChoicePanel.SetButtonsText(true,pt[0], pt[1], pt[2]);
			makeChoicePanel.MakeYourChoice (true,() => {AfterChoiceGoTo(pt[0]);},() => {AfterChoiceGoTo(pt[1]);},() => {AfterChoiceGoTo(pt[2]);});
		}

	}
	//===========================================================
	/// <summary>
	/// Set the text from your choice in the ModalPanelMakeYourChoice and set it as your new State.
	/// </summary>
	/// <param name="st"> String with the name of the choosen State</param>
	//===========================================================
	void AfterChoiceGoTo(string st){

		// transform a string variable to GameControl.State
		GameControl.control.mystate = (GameControl.State) System.Enum.Parse(typeof(GameControl.State), st);

	}
	//================================================================
	/// <summary>
	/// Adds the itens to the player's list.
	/// </summary>
	/// <param name="itemName">Name of the item.</param>
	//================================================================

	void AddItens(string itemName){

		if (!GameControl.control.itens.Contains (itemName))
			GameControl.control.itens.Add (itemName);
	}

	//================================================================
	/// <summary>
	/// Removes the itens from the player's list
	/// </summary>
	/// <param name="itemName">Name of the item to be removed.</param>
	//================================================================
	void RemoveItens(string itemName){

		if (GameControl.control.itens.Contains (itemName))
			GameControl.control.itens.Remove (itemName);
		
	}

	//=================================================================
	/// <summary>
	/// Determines whether this instance has the intem the specified itemName.
	/// </summary>
	/// <returns><c>true</c> if this instance has the intem the specified itemName; otherwise, <c>false</c>.</returns>
	/// <param name="itemName">Item name.</param>
	//=================================================================
	bool HasTheIntem(string itemName){

		if (GameControl.control.itens.Contains (itemName)) {

			return true;
		} else {

			return false;
		}

	}
	//===============================================================
	/// <summary>
	/// Adds a substates to mylist.
	/// </summary>
	/// <param name="mylist">Mylist.</param>
	/// <param name="sbt">The subState to be add.</param>
	/// <typeparam name="T">Gets what it's the type of mylist.</typeparam>
	//===============================================================
	void AddSubStatesToMyList<T>(List<T> mylist, T sbt){

		if (!mylist.Contains (sbt))
			mylist.Add (sbt);

	}

	//==================================================================
	/// <summary>
	///  Removes all the substates from mylist. 
	/// </summary>
	/// <param name="mylist">Mylist.</param>
	/// <typeparam name="T"> Gets what it's the type of mylist.</typeparam>
	//==================================================================
	void RemoveSubStatesFromMyList<T>(List<T>mylist){

		if (mylist != null) {

			mylist.Clear();
		}
	}

	void NextScene(){

		Debug.Log("Next will be load"); 
	}
	#endregion

	#region State methods
	void StateStart () {
		
		text.text = gameSetupL.start;
		RemoveSubStatesFromMyList (mySubState);
		Continue (() => {MakeYourChoice (GameControl.State.A1,GameControl.State.B1,GameControl.State.C1);});

	}

	void StateA1 () {

		if (mySubState.Contains (SubState.A1_0)) {

			text.text = "Insert A1_0 text here...";
		} else {
			
			text.text = gameSetupL.A1;
		}

		Continue (() => {GameControl.control.RunTheDice("Game",2);});

		CheckDiceGoToNextLevel (GameControl.State.A2, GameControl.State.A3);
			
	}

	void StateA2 (){

		text.text = gameSetupL.A2;
		AddItens(ItemList.knife.ToString());
		RemoveSubStatesFromMyList (mySubState); // Avoid a substate of being stored before go to a certain state. 

		Continue (() => {MakeYourChoice (GameControl.State.A4, GameControl.State.B2);});

	}

	void StateA3 () {

		text.text = gameSetupL.A3;

		Continue (() => {GameControl.control.RunTheDice("Game",4);});

		myDead = Dead.A3;

		CheckDiceGoToNextLevel (GameControl.State.A2, GameControl.State.dead);

	}

	void StateA4 () {


		int lv; 

		int enemyAlive = GameControl.control.BattleZumbies (GameControl.Battles.A4);

		#region Text
		switch (enemyAlive){

		case 0:
			text.text = gameSetupL.A4;
			Continue (() => {GoNextLevel(GameControl.State.A5);});
			break;

		case 1:
			lv = 2;
			text.text = gameSetupL.enemyA4_1; 

			Continue (() => {GameControl.control.RunTheDice("Game",lv);});
			myDead = Dead.A4;
			CheckDiceGoToNextLevel (GameControl.State.A4, GameControl.State.dead);
			break;

		case 2:
			lv = 2;
			text.text = gameSetupL.enemyA4_2; 

			Continue (() => {GameControl.control.RunTheDice("Game",lv);});
			myDead = Dead.A4;
			CheckDiceGoToNextLevel (GameControl.State.A4, GameControl.State.dead);
			break;

		case 3:
			lv = 1; 
			text.text = gameSetupL.enemyA4_3;

			Continue (() => {GameControl.control.RunTheDice("Game",lv);});
			myDead = Dead.A4;
			CheckDiceGoToNextLevel (GameControl.State.A4, GameControl.State.dead);
			break;

		case 4:
			lv = 1;
			text.text = gameSetupL.enemyA4_4;

			Continue (() => {GameControl.control.RunTheDice("Game",lv);});
			myDead = Dead.A4;
			CheckDiceGoToNextLevel (GameControl.State.A4, GameControl.State.dead);
			break;

		case 5:
			lv = 1;
			text.text = gameSetupL.enemyA4_5;

			Continue (() => {GameControl.control.RunTheDice("Game",lv);});
			myDead = Dead.A4;
			CheckDiceGoToNextLevel (GameControl.State.A4, GameControl.State.dead);
			break;

		}

		#endregion

	}

	void StateA5 (){

		text.text = gameSetupL.A5;

		AddSubStatesToMyList (mySubState,SubState.B2_0);
		Continue (() => {MakeYourChoice(GameControl.State.B2,GameControl.State.C4);});

	}

	void StateB1 (){

		if (mySubState.Contains (SubState.B1_0)) {

			text.text = "Insert B1_0 text here....";
		} else {

			text.text = gameSetupL.B1;
		}

		Continue (() => {MakeYourChoice(GameControl.State.B3,GameControl.State.B4);});

	}

	void StateB2 (){

		if (mySubState.Contains(SubState.B2_0)) {

			text.text = "Insert B2_0 text here...";
			
		} else {

			text.text = gameSetupL.B2;
		}

		Continue (() => {MakeYourChoice(GameControl.State.B4,GameControl.State.B5,GameControl.State.B3);});

	}

	void StateB3 (){

		// Here we must test the imagem serch mecanichs
		text.text = gameSetupL.B3;

		if (HasTheIntem (ItemList.knife.ToString()))
			AddSubStatesToMyList (mySubState,SubState.D1_0);

		Continue(GameControl.control.FindItenInTheScene);

		CheckFindImageToNextLevel (GameControl.State.D1,GameControl.State.B4);

	}

	void StateB4 (){

		text.text = gameSetupL.B4;


		Continue (() => {GameControl.control.RunTheDice("Game",3);});

		myDead = Dead.B4;

		if (HasTheIntem (ItemList.knife.ToString())) {

			AddSubStatesToMyList (mySubState,SubState.D1_1);
		} else {
			
			AddSubStatesToMyList (mySubState,SubState.D1_2);
		}

		CheckDiceGoToNextLevel (GameControl.State.D1, GameControl.State.dead);

	}

	void StateB5 () {

		text.text = gameSetupL.B5;

		Continue (() => {GameControl.control.RunTheDice("Game",3);});

		AddSubStatesToMyList (mySubState,SubState.D1_3);
		CheckDiceGoToNextLevel (GameControl.State.D1, GameControl.State.D2);

	}
		
	void StateC1 (){

		text.text = gameSetupL.C1;

		Continue (() => {MakeYourChoice(GameControl.State.B1,GameControl.State.C2);});

	} 

	void StateC2 () {

		text.text = gameSetupL.C2;

		Continue (() => {GameControl.control.RunTheDice("Game",3);});

		myDead = Dead.C2;
		CheckDiceGoToNextLevel (GameControl.State.C3, GameControl.State.dead);

	}

	void StateC3 (){

		text.text = gameSetupL.C3;

		AddSubStatesToMyList (mySubState,SubState.B1_0);
		AddSubStatesToMyList (mySubState,SubState.A1_0);

		Continue (() => {MakeYourChoice(GameControl.State.B1,GameControl.State.A1);});

	}

	void StateC4 (){

		text.text = gameSetupL.C4;

		AddSubStatesToMyList (mySubState,SubState.B2_0);
		Continue (() => {MakeYourChoice(GameControl.State.B2,GameControl.State.C5);});
	}

	void StateC5 () {

		text.text = gameSetupL.C5;

		Continue (() => {GameControl.control.RunTheDice("Game",2);});

		myDead = Dead.C5;
		CheckDiceGoToNextLevel (GameControl.State.C6, GameControl.State.dead);

	}

	void StateC6 (){

		text.text = gameSetupL.C6;

		AddSubStatesToMyList (mySubState, SubState.B2_0);
		Continue (() => {GoNextLevel (GameControl.State.B2);});
	}

	void StateD1 () {


		if (mySubState.Contains (SubState.D1_0)) {

			text.text = "Instert D1_0 text....";
		} else if (mySubState.Contains (SubState.D1_1)) {

			text.text = "Instert D1_1 text....";
		} else if (mySubState.Contains (SubState.D1_2)) {

			text.text = "Instert D1_2 text....";
		} else if (mySubState.Contains (SubState.D1_3)) {

			text.text = "Instert D1_3 text....";
		} else {

			text.text = gameSetupL.D1;
		}

		Continue (NextScene);

	}

	void StateD2 (){

		text.text = gameSetupL.D2;

		RemoveItens (ItemList.knife.ToString());

		Continue (NextScene);
	}
		
	void StateDead () {

		if (playDeadAudio) {
			gameObject.GetComponent<AudioController> ().StopPlayBackground ();
			gameObject.GetComponent<AudioController> ().PlayDead ();
			playDeadAudio = false;
		}


		switch (myDead) {

		case Dead.Deafout:

			text.text = "Deafout Dead...";
			break;

		case Dead.TimeOut:

			text.text = gameSetupL.dead;
			break;

		case Dead.A3:

			text.text = "Insert A3 Dead...."; 
			break;

		case Dead.A4:

			text.text = "Insert A4 Dead....";
			break;

		case Dead.B4:

			text.text = "Insert B4 Dead....";
			break;

		case Dead.C2:

			text.text = "Insert C2 Dead....";
			break;

		case Dead.C5:

			text.text = "Insert C5 Dead....";
			break;
		}
			
		Continue (GameControl.control.Restart);

	}
	#endregion
		
}
