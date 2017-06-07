using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour {

	//public variables
	public Image loadingBar;
	public Image background;
	public float waitingTime;
	[HideInInspector]
	public enum Scene{Game,FindImage};
	public Scene myScene; 


	private float alpha;
	private float amp;
	private float smooth;
	private float speedy;
	private Material material;

	void Awake(){

		SetParameter ();
		// Randomilly choose beteween 2 material to be displayed
		/*int rn = Random.Range (0, 2);
		if (rn == 0) {
			material = Resources.Load<Material> ("Textures/Materials/organic34");
		} else {
			material = Resources.Load<Material> ("Textures/Materials/organic40");
		}

		background.material = material;
		*/
	}

	void OnEnable(){
		
		SetParameter ();
	}

	void OnDisable(){

		SetParameter ();
	}


	void SetParameter(){

		if (background != null) {
			
			alpha = 0f;
			background.color = new Color (0.73f, 0f, 0.53f, alpha);
			smooth = 1f;
			amp = 1f / (Mathf.Exp (smooth) - 1f);
		}

		speedy = 1f / waitingTime;
		loadingBar.fillAmount = 0f;

	}
		
	void FixedUpdate(){
		
		PassTime ();
	}

	void PassTime(){

		if (background != null) {
			
			alpha = amp * (Mathf.Exp (loadingBar.fillAmount * smooth) - 1f);
			background.color = new Color (0.73f, 0f, 0.53f, alpha);

		}

		loadingBar.fillAmount +=  speedy * Time.fixedDeltaTime;

		switch (myScene) {

		case Scene.Game:
			CheckTimeDead ();
			break;

		case Scene.FindImage:
			CheckTimeInFindIten ();
			break;
		}

	}

	void CheckTimeDead(){

		if (loadingBar.fillAmount >= 1) {

			GameControl.control.mystate = GameControl.State.dead;
			this.gameObject.SetActive (false);
		}
	}

	void CheckTimeInFindIten(){

		if (loadingBar.fillAmount >= 1) {

			GameControl.control.findImageResult = "fail";
			LevelManager.lm.LoadLevel ("Game");
		}

	}
		
}