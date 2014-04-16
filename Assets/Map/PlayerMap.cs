using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class PlayerMap : MonoBehaviour {
	// Tile set controller logic, finds tileset controller object in scene
	static GameObject tileset_controller;
	static Tileset tileset;

	// Tiles per player map
	static public int size_x = 30;
	static public int size_y = 30;

	// Tile map
	// TileMap tilemap;

	/*
	public PlayerMap(int worldpos_x, int worldpos_y){
		// TO-DO: load from file at this point in world
		Start ();
	}
	*/

	void Start () {
		tileset_controller = GameObject.Find("Tileset Controller");
		tileset = (Tileset) tileset_controller.GetComponent("Tileset");

		BuildMesh ();
	}

	void DrawTile(int tile_id, int offset_x, int offset_y, Texture2D texture){
		// Pulls color array of tile based on type from tileset
		Color[] tile = tileset.GetTile (tile_id);

		if (tile == null)
			return;

		// Loops through every pixel to skip drawing of alpha, otherwise overrides
		for(int px=0; px < tileset.tile_width; px++){
			for(int py=0; py < tileset.tile_height; py++){
				Color pixelColor = tile[py * tileset.tile_width + px];

				// Alpha check
				if(pixelColor.a > 0)
					texture.SetPixel(offset_x + px, offset_y + py, pixelColor);
			}
		}
	}

	Vector2 toISO(Vector2 coords) {
		Vector2 isoCoords = new Vector2 ();

		isoCoords.x = (coords.x * tileset.tile_width/ 2) + (coords.y * tileset.tile_width / 2);
		isoCoords.y = (coords.y * tileset.tile_height / 2) - (coords.x * tileset.tile_height / 2) + (size_y * tileset.tile_height / 2);

		return isoCoords;
	}

	Vector2 toISO(float x, float y) {
		Vector2 isoCoords = new Vector2 ();
		
		isoCoords.x = (x * tileset.tile_width/ 2) + (y * tileset.tile_width / 2);
		isoCoords.y = (y * tileset.tile_height / 2) - (x * tileset.tile_height / 2) + (size_y * tileset.tile_height / 2);
		
		return isoCoords;
	}

	/* TO-DO
	Vector2 fromISO(Vector2 isoCoords) {
		Vector2 coords = new Vector2 ();
	}
	*/
	
	void BuildTexture() {
		// Determine mesh texture resolution
		int texWidth = size_x * tileset.tile_width+ tileset.tile_width / 2;
		int texHeight = size_y * tileset.tile_height + tileset.tile_height / 2;

		// Instantiate empty texture for mesh
		Texture2D texture = new Texture2D(texWidth, texHeight);

		// Initialize texture to transparent
		for (int i=0; i < texWidth; i++) {
			for(int j=0; j < texHeight; j++){
				texture.SetPixel(i,j,Color.clear);
			}
		}

		//
		for(int i=0; i < size_x; i++) {
			for(int j=size_y; j >= 0; j--) {
				// Color[] p = tiles[ map.GetTileAt(x,y) ];

				// Determine isometric coordinates
				Vector2 isoCoords = toISO(j, i);
				
				// Draw Tile
				DrawTile(Random.Range(0,3), (int)isoCoords.x, (int)isoCoords.y, texture);
				
				// texture.SetPixels(x, y, tileResolution_x, tileResolution_y, terrainTiles.GetPixels());
			}
		}

		// Point filter does no pixel blending
		texture.filterMode = FilterMode.Point;
		texture.Apply();

		// Assigns generated texture to mesh object
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		mesh_renderer.sharedMaterial.mainTexture = texture;
		
		Debug.Log ("Done Texture!");
	}

	public void BuildMesh() {
		Vector3[] vertices = new Vector3[4];
		int[] triangles = new int[2 * 3];
		Vector3[] normals = new Vector3[4];
		Vector2[] uv = new Vector2[4];
		
		vertices [0] = new Vector3 (0, 0, 0);
		vertices [1] = new Vector3 (size_x, 0, 0);
		vertices [2] = new Vector3 (0, 0, -size_y / 2);
		vertices [3] = new Vector3 (size_x, 0, -size_y /2);
		
		uv [0] = new Vector2 (0, 0);
		uv [1] = new Vector2 (1, 0);
		uv [2] = new Vector2 (0, 1);
		uv [3] = new Vector2 (1, 1);
		
		triangles [0] = 0;
		triangles [1] = 3;
		triangles [2] = 2;
		
		triangles [3] = 0;
		triangles [4] = 1;
		triangles [5] = 3;
		
		for (int i=0; i < 4; i++) {
			normals[i] = Vector3.up;
		}
		
		// Create a new Mesh and populate with the data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
		
		// Assign our mesh to our filter/renderer/collider
		MeshFilter mesh_filter = GetComponent<MeshFilter>();
		MeshCollider mesh_collider = GetComponent<MeshCollider>();
		
		mesh_filter.mesh = mesh;
		mesh_collider.sharedMesh = mesh;
		
		BuildTexture();
	}
}
