using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetFontSize : MonoBehaviour {

	public Text changeSizeOfThisFont;

	private Slider slider;

	void Start(){
		
		slider = GetComponent<Slider> ();
		slider.wholeNumbers = true;
		SetInitialSize ();
	}

	void SetInitialSize(){

		if (PlayerPrefs.HasKey ("FontSize")) {
			
			changeSizeOfThisFont.fontSize = PlayerPrefs.GetInt("FontSize"); // initial valeu
			slider.value = PlayerPrefs.GetInt("FontSize");

		} else {
			changeSizeOfThisFont.fontSize = 40; // initial valeu
			slider.value = 40f;
		}
	}

	public void ChangeValeu (){

		int value = (int) slider.value;
		PlayerPrefs.SetInt ("FontSize", value);
		PlayerPrefs.Save ();
		changeSizeOfThisFont.fontSize = value;
	}
}
