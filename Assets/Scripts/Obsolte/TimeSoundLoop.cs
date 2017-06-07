using UnityEngine;
using System.Collections;

public class TimeSoundLoop : MonoBehaviour {

	private AudioSource source;

	public float waitSeconds;
	public bool playonawake;

	void Awake () {
		
		source = GetComponent<AudioSource> ();

		if (playonawake) {
			source.playOnAwake = true;
			source.Play ();
		}
		StartCoroutine ("PlaySound");
	
	}

	IEnumerator PlaySound(){

		while (true) {

			yield return new WaitForSeconds (waitSeconds);
			source.Play ();
		}
	}

}
