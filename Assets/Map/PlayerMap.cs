using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class PlayerMap : MonoBehaviour {
	// Tiles per player map
	int size_x = 30;
	int size_y = 30;

	// Tile Size
	int tileResolution_x = 64;
	int tileResolution_y = 32;

	// Tile set
	static Tileset tileset = new Tileset();

	// Tile map
	TileMap tilemap;

	void Start () {
		BuildMesh ();
	}

	void Update () {
	
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
	
	void DrawTile(int tile_type, int offset_x, int offset_y, Texture2D texture){
		for(int px=0; px < tileResolution_x; px++){
			for(int py=0; py < tileResolution_y; py++){
				Color pixelColor = tileset.GetPixel(px + (tile_type * tileResolution_x), py);
				
				if(pixelColor.a > 0){
					texture.SetPixel(offset_x + px, offset_y + py, pixelColor);
				}
			}
		}
	}
	
	void BuildTexture() {
		int texWidth = size_x * tileResolution_x + tileResolution_x / 2;
		int texHeight = size_y * tileResolution_y + tileResolution_y / 2;
		Texture2D texture = new Texture2D(texWidth, texHeight);
		
		for (int i=0; i < texWidth; i++) {
			for(int j=0; j < texHeight; j++){
				texture.SetPixel(i,j,Color.clear);
			}
		}
		
		for(int i=0; i < size_x; i++) {
			for(int j=size_y; j >= 0; j--) {
				// Color[] p = tiles[ map.GetTileAt(x,y) ];
				
				// Determine isometric coordinates
				int x = (j * tileResolution_x / 2) + (i * tileResolution_x / 2);
				int y = (i * tileResolution_y / 2) - (j * tileResolution_y / 2) + (size_y * tileResolution_y / 2);
				
				// Draw Tile
				DrawTile(Random.Range(0,3), x, y, texture);
				
				// texture.SetPixels(x, y, tileResolution_x, tileResolution_y, terrainTiles.GetPixels());
			}
		}
		
		texture.filterMode = FilterMode.Point;
		// texture.wrapMode = TextureWrapMode.Repeat;
		texture.Apply();
		
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		mesh_renderer.sharedMaterial.mainTexture = texture;
		
		Debug.Log ("Done Texture!");
	}
}
