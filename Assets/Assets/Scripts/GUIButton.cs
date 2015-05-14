using UnityEngine;
using System.Collections;


public class GUIButton : MonoBehaviour {

	public Texture2D buttonImage = null;
	public Texture2D otherImage = null;
	public bool debugMode = false;
	public bool centerButton = false;

	public Rect position = new Rect (400, 500, 150, 150);
	public Rect positionTwo = new Rect (400, 500, 150, 150);
	private Rect currentRect = new Rect (400, 500, 150, 150);

	public GUISkin skin = null;
	private bool invertClick = false;
	private Texture2D currentImage = null;

	private void Start()
	{
		currentImage = buttonImage;
		currentRect = position;
	}

	private void OnGUI()
	{
		GUI.skin = skin;
		if (debugMode || Application.isPlaying) {

			if (centerButton) {
				currentRect.x = (Screen.width * 0.1f);
				currentRect.y = (Screen.height * 0.5f) - (currentRect.height * 0.5f);
			}

			if (GUI.Button (currentRect, currentImage)) {
				transform.position += new Vector3 (0, 0, 0.1f);
				invertClick = !invertClick;
				if (invertClick){
					currentImage = otherImage;
					currentRect = positionTwo;
				}
				else{
					currentImage = buttonImage;
					currentRect = position;
				}

			}
		}


		
	}
}
