using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {


	public float speed = 0.1f;

	private void Update () 
	{
		transform.eulerAngles += new Vector3 (0, speed, 0);
	}
}
