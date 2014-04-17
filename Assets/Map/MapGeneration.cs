using UnityEngine;
using System.Collections;
using System.IO;



public class MapGeneration : MonoBehaviour {
	//where to acquire temp/wet noise maps
	public Texture2D tempFile;
	public Texture2D wetFile;

	string mapText;
	float temp;
	float wet;

	//map scale
	int map_scale = 1;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createRandomMap() {
		//where to save map file
		string mapFile = "Assets/Map/MapFiles/Map.txt";
		
		//xy map dimensions
		int playerMapX = 90;
		int playerMapY = 90;
		//For writing to text file
		string current_line = "";
	
		
		//how many types of tiles
		int tileMapSize = 2;
		
		int tile_type = 0;

		//keeps track of where on the IMG we are for the arrays
		int map_position = 0;


		//Stream writer for writing to the file
		using (StreamWriter writer = new StreamWriter(mapFile)) {
			Debug.Log ("Writing New Map");
		for(int y=0; y < playerMapY; y++){
			for(int x=0; x< playerMapX; x++){

				//take current pixel in respective images and write them to appropriate array as either 255 or 0
				temp = tempFile.GetPixel (x,y).r;

				wet = wetFile.GetPixel (x,y).r;


				//tundra
				if(temp == 0 && wet == 0){
					tile_type = 0;
				//desert
				}else if (temp == 1 && wet == 0){
					tile_type = 1;
				//Forest
				}else if (temp == 0 && wet == 1){
					tile_type =2;
				//Forest
				}else if (temp == 1 && wet == 1){
					tile_type = 2;
				}



					//write as 2 digit number
				if(tile_type<10){

					for(int i=0;i<map_scale;i++){
							//write line to string first
							current_line=current_line+"0"+tile_type;
						             
					}

				}else{

					for(int i=0;i<2;i++){
							current_line=current_line+tile_type;
						
					}
				}

				map_position++;

			}
			//Write line to text file as many times as scale
				for(int i=0;i<map_scale;i++){
					writer.WriteLine (current_line);
				}

				//reset string to blank
				current_line="";

		}
		}

}
}
