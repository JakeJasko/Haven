using UnityEngine;
using System.Collections;

public class Tileset : MonoBehaviour {

	public Texture2D tileset_image;

	// Tile size in pixels
	public int tile_width = 64;
	public int tile_height = 32;

	// Amount of usable tiles
	private int total_tiles;
	private int rows;
	private int columns;
	
	void Start() {
		rows = tileset_image.height / tile_height;
		columns = tileset_image.width / tile_width;

		// Determines amount of tiles by areas with non-null pixel centers
		for(int y=0; y < rows; y++)
			for(int x=0; x < columns; x++)
				if(tileset_image.GetPixel(x + tile_width / 2,y + tile_height / 2) > 0)
					total_tiles++;
	}

	public Color[] GetTile (int tile_id) {
		// NULL color set when ID given exceeds usable tiles
		if (tile_id > total_tiles)
			return null;

		// Math for finding specific tile
		int y = tile_id / columns;
		int x = tile_id - (y * rows);

		// Returns array of colors, containing one tile type
		return tileset_image.GetPixels (x, y, tile_width, tile_height);
	}

	public int TotalTypes(){
		return total_tiles;
	}
}
