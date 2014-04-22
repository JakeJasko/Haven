using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	float zoom;
	public readonly float min = 2;
	public readonly float max = 12;

	// Use this for initialization
	void Start () {
		zoom = gameObject.camera.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		float scrollDelta = Input.GetAxis ("Mouse ScrollWheel");
		if (zoom - scrollDelta > min && zoom - scrollDelta < max) {
			zoom -= scrollDelta;
			// Camera.main.orthographicSize = zoom; 
			// changed to reference the script's gameObject vs finding camera in scene
			gameObject.camera.orthographicSize = zoom;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow))
			gameObject.transform.Translate (1, 0, 0);
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			gameObject.transform.Translate (-1, 0, 0);
		if (Input.GetKeyDown (KeyCode.UpArrow))
			gameObject.transform.Translate (0, 1, 0);
		if (Input.GetKeyDown (KeyCode.DownArrow))
			gameObject.transform.Translate (0, -1, 0);
	}
}