using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	
	public bool selected = false;

	private Vector3 moveToDest = Vector3.zero;
	public float floorOffset = 1;
	public float speed = 5;
	public float stopDistanceOffset = 0.5f;
	public GameObject selectionGlow = null;
	private GameObject glow = null;

	private bool selectedByClick = false;

	public float rotationSpeed = 2;
	private float currentRotationSpeed = 2;
	private bool lockedAngle = false;
	private bool chooseDirection = false;
	
	
	
	private void Update()
	{
		if (GetComponent<Renderer>().isVisible && Input.GetMouseButton(0)) 
		{
			if (!selectedByClick)
			{
				Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
				camPos.y = CameraOperator.InvertMouseY(camPos.y);
				selected = CameraOperator.selection.Contains (camPos);
			}


			if (selected && glow == null)
			{
				glow = (GameObject)GameObject.Instantiate (selectionGlow);
				glow.transform.parent = transform;
				glow.transform.localPosition = new Vector3(0, 1, 0);
				GetComponent<Renderer>().material.color = Color.red;
			}
			else if (!selected && glow != null)
			{
				GameObject.Destroy(glow);
				glow = null;
			}
				
		}

		if (selected && Input.GetMouseButtonUp(1)) 
		{
			Vector3 destination = CameraOperator.GetDestination ();

			if (destination != Vector3.zero)
			{
				moveToDest = destination;
				moveToDest.y += floorOffset;
			}
			lockedAngle = false;
			chooseDirection = false;
		}

		UpdateMove();

		
	}

	private void UpdateMove()
	{
		if (moveToDest != Vector3.zero && transform.position != moveToDest) {
			Vector3 direction = (moveToDest - transform.position).normalized;
			direction.y = 0;

			if (!lockedAngle)
			{
				float angle = Vector3.Angle (direction, transform.forward);

				if (!chooseDirection)
				{
					Vector3 cross = Vector3.Cross (transform.forward, direction);
					float dot = Vector3.Dot (cross, transform.up);

					if (dot < 0)
						currentRotationSpeed = -rotationSpeed;
					else
						currentRotationSpeed = rotationSpeed;
					chooseDirection = true;
				}
				if (angle > rotationSpeed)
					transform.Rotate (new Vector3(0, currentRotationSpeed, 0));
				else
				{
					transform.LookAt(new Vector3(moveToDest.x, transform.position.y, moveToDest.z));
					lockedAngle = true;
				}
			}
			transform.GetComponent<Rigidbody>().velocity = transform.forward * speed;

			if (Vector3.Distance (transform.position, moveToDest) < stopDistanceOffset)
				moveToDest = Vector3.zero;

		     } 
		    else 
		    {

			chooseDirection = false;
			lockedAngle = false;
			transform.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		     }
	}

	private void OnMouseDown()
	{
		selectedByClick = true;
		selected = true;
	}

	private void OnMouseUp()
	{
		if (selectedByClick)
			selected = true;

		selectedByClick = false;
	}
	
	
}
