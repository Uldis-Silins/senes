using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Shroom))]
public class ShroomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
