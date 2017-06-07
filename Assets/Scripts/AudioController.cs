using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public AudioSource background;
	public AudioSource zombies;
	public AudioSource dead;
	public AudioSource deadScrem; 

	private Coroutine playLoop;

	private static AudioController audioController;

	public static AudioController Instance(){

		if (!audioController) {

			audioController = FindObjectOfType (typeof(AudioController)) as AudioController;
			if (!audioController) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active AudioController Script on a GameObject in your scene");
		}

		return audioController;
		
	}

	IEnumerator PlayLoop(AudioSource source, float waitsec){


		while (true) {

			float wait = waitsec + Random.value*10f;
			 //Random.Range (1, 9);
			yield return new WaitForSeconds (wait);
			source.Play ();

		}
	}

	public void PlayBackground(){

		background.Play ();
		playLoop = StartCoroutine(PlayLoop(zombies,15f));

	}

	public void StopPlayBackground(){
		
		zombies.Stop ();
		background.Stop ();
		StopCoroutine (playLoop);
	}

	public void PausePlayBackground(){
		
		zombies.Stop ();
		background.Pause ();
		StopCoroutine (playLoop);
	}

	public void UnPausePlayBackground(){

		background.UnPause ();
		playLoop = StartCoroutine(PlayLoop(zombies,15f));
	}

	public void PlayDead(){

		deadScrem.Play ();
		dead.Play ();
		
	}

		

}
