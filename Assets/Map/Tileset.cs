using UnityEngine;
using System.Collections;

public class Tileset : MonoBehaviour {

	public Texture2D tileset_image;

	// Tile size in pixels
	public readonly int tile_width = 64;
	public readonly int tile_height = 32;

	// Amount of usable tiles
	int total_tiles = 3;
	int rows = 1;
	int columns = 3;
	
	void Start() {
		rows = tileset_image.height / tile_height;
		Debug.Log ("ROWS: " + rows);
		columns = tileset_image.width / tile_width;
		Debug.Log ("COLUMNS: " + columns);

		/* Determines amount of tiles by areas with non-null pixel centers (may not have worked, unsure)
		for(int y=0; y < rows; y++)
			for(int x=0; x < columns; x++)
				if(tileset_image.GetPixel(x + tile_width / 2,y + tile_height / 2).a > 0)
					total_tiles++;
		*/
	}

	public Color[] GetTile (int tile_id) {
		// NULL color set when ID given exceeds usable tiles
		if (tile_id > total_tiles)
			return null;

		// Math for finding specific tile
		int y = tile_id / columns;
		int x = tile_id - (y * rows);

		// Returns array of colors, containing one tile type
		return tileset_image.GetPixels (x * tile_width, y * tile_height, tile_width, tile_height);
	}

	public int TotalTypes(){
		return total_tiles;
	}
}
