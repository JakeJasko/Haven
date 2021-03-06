﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class PlayerMap : MonoBehaviour {
	// Tile set controller logic, finds tileset controller object in scene
	public static GameObject tileset_controller;
	public static Tileset tileset;

	// Tiles per player map
	public static readonly int size_x = 30;
	public static readonly int size_y = 30;

	// Mesh Size
	public static readonly float mesh_width = 30;
	public static readonly float mesh_height = 15;

	// Clear Colors
	public static Color[] clear_tex;

	/*
	public PlayerMap(int worldpos_x, int worldpos_y){
		// TO-DO: load from file at this point in world
		Start ();
	}
	*/

	void Start () {
		if(tileset_controller == null)
			tileset_controller = GameObject.Find("Tileset Controller");
		if(tileset == null)
			tileset = (Tileset) tileset_controller.GetComponent("Tileset");

		BuildMesh ();
		// BuildTexture ();
	}

	void DrawTile(int tile_id, int offset_x, int offset_y, Texture2D texture){
		// Pulls color array of tile based on type from tileset
		Color[] tile = tileset.GetTile (tile_id);

		if (tile == null)
			return;

		// Loops through every pixel to skip drawing of alpha, otherwise overrides
		for(int px=0; px < Tileset.tile_width; px++){
			for(int py=0; py < Tileset.tile_height; py++){
				Color pixelColor = tile[py * Tileset.tile_width + px];

				// Alpha check
				if(pixelColor.a == 1)
					texture.SetPixel(offset_x + px, offset_y + py, pixelColor);
			}
		}
	}

	void DrawMap(Texture2D texture){
		for(int i = 0; i < size_x; i++) {
			for(int j = size_y - 1; j >= 0; j--) {
				// Color[] p = tiles[ map.GetTileAt(x,y) ];
				
				// Determine isometric coordinates
				Vector2 isoCoords = HavenMath.Iso.toISO(j, i);
				
				// Draw Tile
				DrawTile(Random.Range(0,3), (int)isoCoords.x, (int)isoCoords.y, texture);
			}
		}
	}

	public void BuildTexture() {
		// Determine mesh texture resolution
		int texWidth = size_x * Tileset.tile_width; // + Tileset.tile_width / 2;
		int texHeight = size_y * Tileset.tile_height; // + Tileset.tile_height / 2;

		// Instantiate empty texture for mesh
		Texture2D texture = new Texture2D(texWidth, texHeight);

		// Initialize clear texture if unset previously
		if (clear_tex == null) {
			clear_tex = new Color[texWidth * texHeight];
			for (int i = 0; i < clear_tex.Length; i++)
				clear_tex[i] = Color.clear;
		}

		// Initialize texture to transparent
		texture.SetPixels (0, 0, texWidth, texHeight, clear_tex);

		// Handles map drawing logic
		DrawMap(texture);
		
		// Point filter does no pixel blending
		texture.filterMode = FilterMode.Point;
		texture.Apply();

		// Assigns generated texture to mesh object
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		mesh_renderer.material.mainTexture = texture;
	}

	private void BuildMesh() {
		Vector3[] vertices = new Vector3[4];
		int[] triangles = new int[2 * 3];
		Vector3[] normals = new Vector3[4];
		Vector2[] uv = new Vector2[4];
		
		vertices [0] = new Vector3 (0, 0, 0);
		vertices [1] = new Vector3 (mesh_width, 0, 0);		//(size_x, 0, 0);
		vertices [2] = new Vector3 (0, 0, -mesh_height);		//(0, 0, -size_y / 2);
		vertices [3] = new Vector3 (mesh_width, 0, -mesh_height);	//(size_x, 0, -size_y /2);
		
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
		
		// BuildTexture();
	}
}
