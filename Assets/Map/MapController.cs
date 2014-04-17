using UnityEngine;
using System.Collections;

// [ExecuteInEditMode]
public class MapController : MonoBehaviour {

	// World Coordinates
	static int worldpos_x = 0;
	static int worldpos_y = 0;

	// Mini-map Size
	static readonly int minimap_size_x = 3;
	static readonly int minimap_size_y = 3;

	// Playermap offset
	static readonly int playermap_offset_x = PlayerMap.size_x;
	static readonly int playermap_offset_y = PlayerMap.size_y / 2;
	
	// This is an array of the playermap objects in the scene
	static GameObject[] playermap_objects = new GameObject[minimap_size_x * minimap_size_y];
	// Keeps track of the actually script objects within those objects
	static PlayerMap[] minimap = new PlayerMap[minimap_size_x * minimap_size_y];

	void Start () {
		InstantiatePlayerMaps ();


	}

	void Update() {
		
	}

	void InstantiatePlayerMaps(){
		// Instantiate minimap array with existing PlayerMap scene objects
		for (int i = 0; i < minimap.Length; i++) {
			// Use this when objects already exist in scene
			// GameObject playermap_controller = GameObject.Find("PlayerMap" + i);
			
			// Creates & Instantiates into scene the PlayerMapPrefab GameObject
			GameObject playermap_controller = Instantiate(Resources.Load("PlayerMapPrefab")) as GameObject;
			
			// Keeps track of the PlayerMap GameObjects in scene
			playermap_objects[i] = playermap_controller;
			// Keeps track of the PlayerMap script within the GameObjects
			minimap[i] = (PlayerMap) playermap_controller.GetComponent("PlayerMap");
		}
	}
}
