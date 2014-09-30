using UnityEngine;
using System.Collections;

public class MouseCamera : MonoBehaviour {
	public float speed = 2.0f;

	private Camera Maincamera;

	// Use this for initialization
	void Start () {
		Maincamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			Vector3 vec = Input.mousePosition;
			vec.z = 10f;

			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Maincamera.ScreenToWorldPoint(vec) - transform.position), speed);
		}

	}
}
