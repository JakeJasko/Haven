using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	float zoom;
	public float min = 6;
	public float max = 8;

	// Use this for initialization
	void Start () {
		zoom = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		float scrollDelta = Input.GetAxis ("Mouse ScrollWheel");
		if (zoom - scrollDelta > min && zoom - scrollDelta < max) {
			zoom -= scrollDelta;
			Camera.main.orthographicSize = zoom;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow))
			Camera.main.transform.Translate (1, 0, 0);
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			Camera.main.transform.Translate (-1, 0, 0);
		if (Input.GetKeyDown (KeyCode.UpArrow))
			Camera.main.transform.Translate (0, 1, 0);
		if (Input.GetKeyDown (KeyCode.DownArrow))
			Camera.main.transform.Translate (0, -1, 0);

	}
}