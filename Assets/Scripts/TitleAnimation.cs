using UnityEngine;
using System.Collections;

public class TitleAnimation : MonoBehaviour {

	Animator animator;
	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();

	}

	// Update is called once per frame
	void Update () {

		if( animator.GetCurrentAnimatorStateInfo(0).IsName("Exit"))
		{
			LevelManager.lm.LoadLevel ("Menu");
		}
	
	}
}
