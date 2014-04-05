
public class TileMap {
	Tile[] _tiles;
	int _width;
	int _height;

	public TileMap(int width, int height) {
		_width = width;
		_height = height;

		_tiles = new Tile[_width * _height];
	}

	public Tile getTile(int x, int y) {
		if(x < 0 || x >= _width || y < 0 || y >= _height){
			return null;
		}

		return _tiles[ y * _width + x];
	}
}
