using UnityEngine;
using System.Collections;

public class MouseCamera : MonoBehaviour {
	public float speed = 2.0f;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	private Camera Maincamera;
	private int layerMask;
	private RaycastHit hit;
	
	// Use this for initialization
	void Start () {
		Maincamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		layerMask = 1 << 8;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, 10, layerMask) && hit.collider.gameObject.name.Equals("LargePlane"))
		{
			if(this.transform.localEulerAngles.y < maximumY && this.transform.localEulerAngles.y > minimumY)
			{
				Vector3 vec = Input.mousePosition;
				vec.z = 10f;
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Maincamera.ScreenToWorldPoint(vec) - transform.position), speed);
			}
			Debug.Log(this.transform.localEulerAngles.y);
		}
		
	}
}
