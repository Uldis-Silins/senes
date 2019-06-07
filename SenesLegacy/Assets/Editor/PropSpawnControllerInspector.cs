using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PropSpawnController))]
public class PropSpawnControllerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var propSpawner = (PropSpawnController)target;

        DrawDefaultInspector();

        if(GUILayout.Button("Spawn Shit"))
        {
            propSpawner.SpawnStuff();
        }
    }
}
