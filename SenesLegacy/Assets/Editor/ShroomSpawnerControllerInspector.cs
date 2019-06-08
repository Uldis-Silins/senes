using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShroomSpawnController))]
public class ShroomSpawnerControllerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Spawn Shrooms"))
        {
            var shroomSpawner = (ShroomSpawnController)target;
            shroomSpawner.SpawnShrooms();
        }
    }
}
