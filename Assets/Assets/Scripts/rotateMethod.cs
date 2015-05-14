using UnityEngine;
using System.Collections;

public class rotateMethod : MonoBehaviour {

	public Transform target;
	
	// agdg rotate method
	void Update () {

		transform.forward = Vector3.Lerp (transform.forward, target.position - transform.position, Time.deltaTime * 10.0f);
	
	}
}
