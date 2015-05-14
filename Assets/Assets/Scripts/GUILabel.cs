using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GUILabel : MonoBehaviour {

	public Rect position = new Rect (200, 15, 75, 25);
	public bool debugMode = false;
	public string text = "Hello Stu";
	public GUISkin skin = null;


	private void OnGUI()
	{

		if (debugMode || Application.isPlaying) {
			GUI.skin = skin;
			GUI.Label (position, text);
		}
	}
}
