using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SetButtonText2 : MonoBehaviour {

	//public variables
	public Text button1text;
	public Text button2text;

	//private variables

	bool IsClicked = false;
	string gettext;

	//------- Methods ----------
	void Awake(){

		button1text.text = GameControl.control.path1;
		button2text.text = GameControl.control.path2;
	}

	void Update(){

		if (IsClicked) {

			IsClicked = false;
			GoTo();
		}

	}

	#region Get the parameters
	public void GetText(Text pickedText){

		gettext = pickedText.text;
		IsClicked = true;
	}
	#endregion

	void GoTo(){

		if (gettext == button1text.text) {
			// the line bellow converts a string into an enum
			GameControl.control.mystate = (GameControl.State) System.Enum.Parse(typeof(GameControl.State), button1text.text);
			LevelManager.lm.LoadLevel (GameControl.control.scene_forChoice[0]);

		} else if (gettext == button2text.text) {
			GameControl.control.mystate = (GameControl.State) System.Enum.Parse(typeof(GameControl.State), button2text.text);
			LevelManager.lm.LoadLevel (GameControl.control.scene_forChoice[1]);

		}


	}
}
