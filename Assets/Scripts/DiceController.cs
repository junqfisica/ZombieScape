using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DiceController: MonoBehaviour {

	// Use this for initialization
	public int diff_level;
	public string chalange_result;
	// deal with the dice button
	public Button diceButton;
	public Image[] rollimage;
	public Text diceButtonText;
	public Image diceImageColor;
	public AudioSource heartbeat;
	public AudioSource oneClick;
	public AudioSource iniClick;

	public Text titleText;
	public Color HighlightColor;
	public Color WinColor;
	public Color loseColor;

	//privite variables
	List<int> player_choise = new List<int>();
	List<Image> images = new List<Image> ();
	int number_of_choices = 0;
    public int dice_number;
	int rollxtimes = 0;
	bool IsClicked = false;
	string gettext;
	int numerical_val;
	Image image;
	//Color buttoncolor;
	Button clickButton;
	Sprite clickeBtimage;
	Sprite btimage;
	Sprite[] weel; 

	void Awake (){

		//Load the sprites'images from Resouces -- It only works if the image is within Resource/Sprites/name.
		clickeBtimage = Resources.Load<Sprite>("Sprites/skull_clicked");
		btimage = Resources.Load<Sprite>("Sprites/skull_button");
		weel = Resources.LoadAll<Sprite> ("Sprites/weels");
	}

	void Start() {

	
		diff_level = GameControl.control.dice_lv;
		//diff_level = 2; // for test

		if (diff_level == 1) {
			
			titleText.text = "Choose 1 skull";

		} else {
			
			titleText.text =	"Choose " + diff_level + " skulls";
		}
	}

	void Update(){

		if (diceButton.enabled) { // If the weel is not enable do nothing

			if (IsClicked) {

				PickYourNumbers ();
				if (number_of_choices == diff_level) {diceButtonText.enabled = true;
				} else {diceButtonText.enabled = false;}
				IsClicked = false;
			}

		}		
	}

	IEnumerator RollingDice(){

		diceButton.enabled = false;  // disable button to be clicked
		diceButtonText.enabled = false;  // disable the text
		int max = 48 + dice_number;
		//float sigma = max / 4f;
		heartbeat.Play ();  // play the audio
		iniClick.Play();

		while ( rollxtimes != max) {

			int i = Mathf.RoundToInt ( Mathf.Repeat (rollxtimes, 8)); 
			diceButton.image.sprite = weel [i+1];
			rollimage [i].color = HighlightColor; // turn red.
			float waitsec = 0.6f*Mathf.Exp(-Mathf.Pow((rollxtimes - max),2f)/(2f*Mathf.Pow(16f,2f))); // compute waiting time
			//if (!iniClick.isPlaying)
			if (waitsec > 0.3f)
				oneClick.Play ();
			rollxtimes += 1;

			//float waitsec = 0.004f*rollxtimes + 0.001f;
			yield return new WaitForSeconds (waitsec);
			rollimage [i].color = Color.white; // turn back to white after waitsec secs.

		}

		heartbeat.Stop ();

		if (player_choise.Contains (dice_number)) {
			
			rollimage [dice_number - 1].color = loseColor; // set skull with lose color
		} else {
			
			rollimage [dice_number - 1].color = WinColor; // set skull with win color
		}
		yield return new WaitForSeconds(2f);
		CheckResults ();
	}

	#region Get the parameters
	public void GetText(Text pickedText){

		gettext = pickedText.text;
		numerical_val = int.Parse (gettext);
		IsClicked = true;
	}

	public void GetImage(Image imageIn){

		image = imageIn;
	}

	public void GetButton(Button button){

		//buttoncolor = button.colors.normalColor;
		clickButton = button;
	
	}
	#endregion
		

	void PickYourNumbers () {


		if (player_choise.Contains (numerical_val)) {
			// Remove that number from the list and change the color back to normal
			// ------ Deal with the number --------
			//player_choise.RemoveAt (number_of_choices-1);
			int index = player_choise.IndexOf (numerical_val);
			player_choise.RemoveAt (index);
			number_of_choices--;

			// ------ Deal with the images --------
			//images [index].color = buttoncolor;
			//images.RemoveAt (index);
			clickButton.image.sprite = btimage;
		
		
		} else if (!player_choise.Contains (numerical_val) && number_of_choices != diff_level) {

			// In the case the number is not in the list it add and change its color.
			player_choise.Add (numerical_val);
			images.Add (image);
			number_of_choices++;
			//image.color = HighlightColor;
			clickButton.image.sprite = clickeBtimage; //change image of the button


		}

	}

	public void RollDice() { // this method is call when the weel bottun is clicked

		if (number_of_choices == diff_level) {

			System.DateTime dat = System.DateTime.Now; // Gets the Time from the system
			Random.seed = dat.Millisecond; // Sets the millisencons as a seed to generate more random numbers.

			dice_number = Random.Range (1, 9); // generetes integers numbers between 1-8
			//dice_number = 1; //for test reasons
			StartCoroutine (RollingDice ());
		}

	}

	void CheckResults() {

		chalange_result = "success"; //assumes the player won

		//print ("dice = " + dice_number); // check the results
		if (player_choise.Contains (dice_number)) {

			chalange_result = "fail";
		}// player lost

		//GameControl.control.CheckDiceResult (chalange_result);
		GameControl.control.diceresult = chalange_result;
		LevelManager.lm.LoadLevel (GameControl.control.scene_forDice);

	}

}
