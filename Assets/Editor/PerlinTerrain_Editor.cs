using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PerlinTerrain))]
public class PerlinTerrain_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		{
			PerlinTerrain perlinTerrain = target as PerlinTerrain;
			if (GUILayout.Button("Random Offset"))
			{
				if (perlinTerrain != null)
				{
					perlinTerrain.RandomOffset();
				}
			}
			if (GUILayout.Button("Set Terrain"))
			{
				if (perlinTerrain != null)
				{
					perlinTerrain.SetTerrain();
				}
			}
		}
	}
}
