using UnityEngine;
using System.Collections;

/**
 * Namespace for static reference in other files, declare "using HavenMath;" and
 * reference using HavenMath.classname
 */

namespace HavenMath {
	class Iso {
		public static Vector2 toISO(Vector2 coords) {
			Vector2 isoCoords = new Vector2 ();
			
			isoCoords.x = (coords.x * Tileset.tile_width / 2) + (coords.y * Tileset.tile_width / 2);
			isoCoords.y = (coords.y * Tileset.tile_height / 2) - (coords.x * Tileset.tile_height / 2) + (PlayerMap.size_y * Tileset.tile_height / 2);
			
			return isoCoords;
		}
		
		public static Vector2 toISO(float x, float y) {
			Vector2 isoCoords = new Vector2 ();
			
			isoCoords.x = (x * Tileset.tile_width/ 2) + (y * Tileset.tile_width / 2);
			//isoCoords.y = (y * Tileset.tile_height / 2) - (x * Tileset.tile_height / 2) + (PlayerMap.size_y * Tileset.tile_height / 2) - Tileset.tile_height / 2;
			// Reduced:
			isoCoords.y = 0.5f * Tileset.tile_height * (PlayerMap.size_y - x + y - 1);

			return isoCoords;
		}

		public static Vector2 toISO(float x, float y, float width, float height) {
			Vector2 isoCoords = new Vector2 ();
			
			isoCoords.x = (x * width/ 2) + (y * width / 2);
			isoCoords.y = (y * height / 2) - (x * height / 2); // + (MapController.minimap_size_y * height / 2);
			
			return isoCoords;
		}
	}
}
