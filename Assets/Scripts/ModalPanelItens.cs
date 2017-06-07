using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class ModalPanelItens : MonoBehaviour {

	public GameObject modalPanelObject;
	public Color backgroungColor;
	public Button exit;
	public Image[] images;


	private List<Sprite> loadedSprites;

	private static ModalPanelItens modalPanel;

	public static ModalPanelItens Instance(){

		if (!modalPanel) {

			modalPanel = FindObjectOfType (typeof(ModalPanelItens)) as ModalPanelItens;
			if (!modalPanel) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active ModalPanelItens Script on a GameObject in your scene");
		}

		return modalPanel;
	}

	void Awake(){ // This is necessary to enable the checkbox on the inspector
	}
		

	public void OpenModalPanel(){

		loadedSprites = new List<Sprite> ();
		ResetImages ();
		SetImages ();
		modalPanelObject.SetActive (true);

		exit.onClick.RemoveAllListeners ();
		exit.onClick.AddListener (ClosePanel);

	}

	void ClosePanel(){

		modalPanelObject.SetActive (false);
	}

	void LoadImages(string name){
		
		//Load the sprites'images from Resouces -- It only works if the image is within Resource/Sprites/name.
		Sprite spt = Resources.Load<Sprite> ("Sprites/" + name);
		loadedSprites.Add (spt);

	}

	void SetImages(){

		GameControl.control.itens.ForEach (LoadImages);

		int i = 0;
		foreach (Sprite spt in loadedSprites) {

			images[i].sprite = spt;
			images [i].color = new Color (1f,1f,1f,1f);
			i++;
		}
		
	}

	void ResetImages(){

		foreach (Image img in images) {

			img.sprite = null;
			img.color = backgroungColor;

		}

	}



}
