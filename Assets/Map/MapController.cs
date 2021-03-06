using UnityEngine;
using System.Collections;
using HavenMath;

// [ExecuteInEditMode]
public class MapController : MonoBehaviour {

	// World Coordinates
	static int worldpos_x = 0;
	static int worldpos_y = 0;

	// Mini-map Size
	public static readonly int minimap_size_x = 5;
	public static readonly int minimap_size_y = 5;

	// Playermap offset
	static readonly float playermap_offset_x = PlayerMap.mesh_width;
	static readonly float playermap_offset_y = PlayerMap.mesh_height;

	// PlayerMap GameObjects are derived from this prefab
	public PlayerMap playermap_prefab;
	
	// This is an array of the playermap objects in the scene
	static PlayerMap[] minimap = new PlayerMap[minimap_size_x * minimap_size_y];

	void Start () {
		InstantiatePlayerMaps ();
		// UpdatePosition ();
	}

	void Update() {

	}

	public void UpdatePosition(int array_pos){
		int j = array_pos / minimap_size_x;
		int i = array_pos - (j * minimap_size_y);

		Vector2 new_position = HavenMath.Iso.toISO(j, i, playermap_offset_x, playermap_offset_y);
		minimap[array_pos].transform.position = new_position;
		UpdateTexture (array_pos);
	}

	public void UpdatePosition(){
		for (int i = 0; i < minimap_size_x; i++) {
			for(int j = minimap_size_y - 1; j >= 0; j--){
			// Updates position of PlayerMap object using offset
				int array_pos = (i * minimap_size_x + j);
				Vector2 new_position = HavenMath.Iso.toISO(j, i, playermap_offset_x, playermap_offset_y);
				minimap[array_pos].transform.position = new_position;
				// playermap_objects[array_pos].GetComponent<PlayerMap>().BuildTexture();
				UpdateTexture (array_pos);
			}
		}

	}

	void UpdateTexture(int playermap_num){
		//Debug.Log (playermap_objects [array_pos].GetComponent<PlayerMap>());
		//PlayerMap pmap_object = (PlayerMap)(playermap_objects [array_pos].GetComponent<PlayerMap> ());
		//pmap_object.BuildTexture();
		minimap[playermap_num].BuildTexture ();
	}

	void InstantiatePlayerMaps(){
		// Instantiate minimap array with existing PlayerMap scene objects
		for (int i = 0; i < minimap.Length; i++) {
			// Use this when objects already exist in scene
			// GameObject playermap_controller = GameObject.Find("PlayerMap" + i);
			
			// Creates & Instantiates into scene the PlayerMapPrefab GameObject
			PlayerMap playermap_controller = (PlayerMap) Instantiate(playermap_prefab);
			playermap_controller.name = "PlayerMap" + i;

			//PlayerMap playermap = (PlayerMap) playermap_controller.GetComponent(typeof(PlayerMap));

			// Keeps track of the PlayerMap GameObjects in scene
			//				playermap_objects[i] = playermap_controller;
			// Keeps track of the PlayerMap script within the GameObjects
			minimap[i] = playermap_controller;

			// playermap.BuildTexture();
		}
	}
}
