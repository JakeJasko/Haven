
public class Tile {
	enum TileType {
		GRASS,
		SAND,
		DIRT
	
	}

	TileType type;

	public Tile() {
		type = TileType.GRASS;
	}
}
