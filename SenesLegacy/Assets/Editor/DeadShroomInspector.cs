using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeadShroom))]
public class DeadShroomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Spawn"))
        {
            DeadShroom deadShroom = (DeadShroom)target;
            deadShroom.Spawn(deadShroom.transform.position, deadShroom.transform.rotation, deadShroom.transform.position);
        }
    }
}
