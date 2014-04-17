using UnityEditor;
using UnityEngine;
using System.Collections;

 [CustomEditor(typeof(MapGeneration))]
public class MapGenInspector : Editor {
	
	public override void OnInspectorGUI() {
		//base.OnInspectorGUI();
		DrawDefaultInspector();
		
		if(GUILayout.Button("Generate")) {
			 MapGeneration mapGen = (MapGeneration)target;
			 mapGen.createRandomMap();
		}
	}
}
