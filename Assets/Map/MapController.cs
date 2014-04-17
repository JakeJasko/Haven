using UnityEngine;
using System.Collections;
using HavenMath;

// [ExecuteInEditMode]
public class MapController : MonoBehaviour {

	// World Coordinates
	static int worldpos_x = 0;
	static int worldpos_y = 0;

	// Mini-map Size
	public static readonly int minimap_size_x = 3;
	public static readonly int minimap_size_y = 3;

	// Playermap offset
	static readonly int playermap_offset_x = PlayerMap.size_x;
	static readonly int playermap_offset_y = PlayerMap.size_y / 2;
	
	// This is an array of the playermap objects in the scene
	static GameObject[] playermap_objects = new GameObject[minimap_size_x * minimap_size_y];
	// Keeps track of the actually script objects within those objects
	static PlayerMap[] minimap = new PlayerMap[minimap_size_x * minimap_size_y];

	void Start () {
		InstantiatePlayerMaps ();
		UpdatePosition ();

	}

	void Update() {

	}

	void UpdatePosition(){
		for (int i = 0; i < minimap_size_x; i++) {
			for(int j = minimap_size_y - 1; j >= 0; j--){
			// Updates position of PlayerMap object using offset
				Vector2 new_position = HavenMath.Iso.toISO(j, i, playermap_offset_x, playermap_offset_y);
				playermap_objects[i * minimap_size_x + j].transform.position = new_position;
			}
		}

	}

	void InstantiatePlayerMaps(){
		// Instantiate minimap array with existing PlayerMap scene objects
		for (int i = 0; i < minimap.Length; i++) {
			// Use this when objects already exist in scene
			// GameObject playermap_controller = GameObject.Find("PlayerMap" + i);
			
			// Creates & Instantiates into scene the PlayerMapPrefab GameObject
			GameObject playermap_controller = Instantiate(Resources.Load("PlayerMapPrefab")) as GameObject;
			PlayerMap playermap = (PlayerMap) playermap_controller.GetComponent("PlayerMap");

			// Keeps track of the PlayerMap GameObjects in scene
			playermap_objects[i] = playermap_controller;
			// Keeps track of the PlayerMap script within the GameObjects
			minimap[i] = playermap;

			// playermap.BuildTexture();
		}
	}
}
