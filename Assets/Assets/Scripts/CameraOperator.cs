using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraOperator : MonoBehaviour {
	
	public Texture2D selectionHighlight = null;
	public static Rect selection = new Rect(0, 0, 0, 0);
	private Vector3 startClick = -Vector3.one;

	public float zoomMaxY = 0;
	public float zoomMinY = 0;
	public float zoomSpeed = 0.05f;
	public float zoomTime = 0.25f;
	public Vector3 zoomDestination = Vector3.zero;


	private static Vector3 moveToDestination = Vector3.zero;
	private static List<string> passables = new List<string>() { "Floor" };
	
	// Update is called once per frame
	private void Update () {
		CheckCamera();
		ZoomCamera ();
		Cleanup();

	}

	private void ZoomCamera()
	{


		float moveY = (Input.GetAxis("Mouse ScrollWheel"));

		if (moveY != 0)
		{
			zoomDestination = transform.position + (transform.forward * moveY) * zoomSpeed;
		}

		if (zoomDestination != Vector3.zero && zoomDestination.y < zoomMaxY && zoomDestination.y > zoomMinY) 
		{
			transform.position = Vector3.Lerp (transform.position, zoomDestination, zoomTime);

			if (transform.position == zoomDestination)
				zoomDestination = Vector3.zero;
		}




		if (transform.position.y > zoomMaxY)
		    transform.position = new Vector3(transform.position.x, zoomMaxY, transform.position.z);
		if (transform.position.y < zoomMinY)
			transform.position = new Vector3(transform.position.x, zoomMinY, transform.position.z);
	}
	
	private void CheckCamera()
	{
		if (Input.GetMouseButtonDown(0))
			startClick = Input.mousePosition;
		else if (Input.GetMouseButtonUp(0))
		{startClick = -Vector3.one;
	}


		
		if (Input.GetMouseButton(0))
			selection = new Rect(startClick.x, InvertMouseY(startClick.y), Input.mousePosition.x - startClick.x, InvertMouseY(Input.mousePosition.y) - InvertMouseY(startClick.y));
			if (selection.width < 0)
			{
			selection.x += selection.width;
			selection.width = -selection.width;
			}
			if (selection.height < 0)
			{
			selection.y += selection.height;
			selection.height = -selection.height;
			}
			
		  
		
	}

	public static float InvertMouseY(float y)
	{
		return Screen.height - y;
		
	}

	private void Cleanup()
	{
		if (!Input.GetMouseButtonUp(1))
			moveToDestination = Vector3.zero;
	}

	public static Vector3 GetDestination()
	{
		if (moveToDestination == Vector3.zero) 
		{
			RaycastHit hit;
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(r, out hit))
			    {

					while (!passables.Contains (hit.transform.gameObject.name))
						{
						if (!Physics.Raycast(hit.point + r.direction * 0.1f, r.direction, out hit))
							break;
						}
				}

			if (hit.transform != null)
				moveToDestination = hit.point;

		}

		return moveToDestination;

		
	}

	private void OnGUI()
	{
		if (startClick != -Vector3.one) {
			GUI.color = new Color (1, 1, 1, 0.5f);
			GUI.DrawTexture (selection, selectionHighlight);
		}

	}


}
