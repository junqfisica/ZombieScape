using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

	public static LevelManager lm;


	void Awake () {

		if (lm == null) {

			DontDestroyOnLoad (gameObject);
			lm = this;

		} else if (lm != this) {

			Destroy (gameObject);
		}
			
	}
		

	public void LoadLevel (string name) {

		SceneManager.LoadScene (name);
	
	}

	public void QuitGame (){

		Application.Quit();

		#if UNITY_EDITOR 
		// set the play mode to stop
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
