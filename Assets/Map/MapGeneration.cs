using UnityEngine;
using System.Collections;
using System.IO;



public class MapGeneration : MonoBehaviour {

	public Texture2D tempFile;
	public Texture2D wetFile;

	string mapText;
	float temp;
	float wet;

	// Use this for initialization
	void Start () {
		createRandomMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createRandomMap() {

		//where to save map file
		string mapFile = "Assets/Map/MapFiles/Map.txt";

		//where to acquire temp/wet noise maps
	
		//xy map dimensions
		int playerMapX = 90;
		int playerMapY = 90;


		//how many types of tiles
		int tileMapSize = 4;

		string tile_type = "0";
		//keeps track of where on the IMG we are for the arrays
		int map_position = 0;



		for(int y=0; y < playerMapY; y++){
			for(int x=0; x< playerMapX; x++){

				//take current pixel in respective images and write them to appropriate array as either 255 or 0
				temp = tempFile.GetPixel (x,y).r;

				wet = wetFile.GetPixel (x,y).r;


				//tundra
				if(temp == 0 && wet == 0){
					tile_type = "O";
				//desert
				}else if (temp == 1 && wet == 0){
					tile_type = "~";
				//Forest
				}else if (temp == 0 && wet == 1){
					tile_type = "T";
				//Forest
				}else if (temp == 1 && wet == 1){
					tile_type = "T";
				}

				//keep it 2 digit
				mapText = mapText+tile_type;
				//if(tile_type<10){
					//mapText = mapText+"0"+tile_type;

				//}else{
					//mapText = mapText+tile_type;
				//}

				map_position++;

			}
			Debug.Log ("At new line, The Pixel Temp was "+temp+" and the wet was "+wet);
			mapText = mapText+"\n";

		}

		if(System.IO.File.Exists (mapFile)){
			Debug.Log (mapFile+" already exists. Overwriting");
			using(TextWriter textWriter = File.CreateText (mapFile)){
				textWriter.Write (mapText);
			}
			Debug.Log (mapFile+" created.");
			
		}else{
			using(TextWriter textWriter = File.CreateText (mapFile)){
				textWriter.Write (mapText);
			}
			Debug.Log (mapFile+" created.");
		}


}
}
