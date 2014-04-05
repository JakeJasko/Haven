
public class Tile {
	enum TileType {
		GRASS,
		SAND,
		OCEAN,
		MOUNTAIN
	}

	TileType type;

	public Tile() {
		type = TileType.OCEAN;
	}
}
