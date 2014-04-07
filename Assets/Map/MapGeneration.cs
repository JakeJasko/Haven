using UnityEngine;
using System.Collections;
using System.IO;


public class MapGeneration : MonoBehaviour {
	string mapText;


	// Use this for initialization
	void Start () {
		createRandomMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createRandomMap() {
		string mapFile = "Assets/Map/Map.txt";



		int playerMapX = 30;
		int playerMapY = 30;

		int tileMapSize = 3;

	

		for(int y=0; y < playerMapY; y++){
			for(int x=0; x< playerMapX; x++){
				mapText = mapText+Random.Range (0,tileMapSize);
			}
			mapText=mapText+"\n";
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
