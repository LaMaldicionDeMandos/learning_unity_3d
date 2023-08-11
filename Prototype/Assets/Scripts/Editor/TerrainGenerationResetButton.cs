using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainGeneratorManager))]
public class TerrainGenerationResetButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (GUILayout.Button("Regenerate Terrain")) {
            TerrainGeneratorManager targetManager = serializedObject.targetObject as TerrainGeneratorManager;
            targetManager.OnRegenerate();
        }
    }
}
