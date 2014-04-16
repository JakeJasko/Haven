using UnityEngine;
using System.Collections;

// [ExecuteInEditMode]
public class MapController : MonoBehaviour {

	// World Coordinates
	static int worldpos_x = 0;
	static int worldpos_y = 0;

	// Mini-map Size
	static int minimap_size_x = 3;
	static int minimap_size_y = 3;
	
	// This is an array of the playermap objects in the scene
	static GameObject[] playermap_objects = new GameObject[minimap_size_x * minimap_size_y];
	// Keeps track of the actually script objects within those objects
	static PlayerMap[] minimap = new PlayerMap[minimap_size_x * minimap_size_y];

	void Start () {
		// Instantiate minimap array with existing PlayerMap scene objects
		for (int i = 0; i < minimap.Length; i++) {
			GameObject playermap_controller = GameObject.Find("PlayerMap" + i);

			playermap_objects[i] = playermap_controller;
			minimap[i] = (PlayerMap) playermap_controller.GetComponent("PlayerMap");
		}

	}

	void Update() {

	}

}
