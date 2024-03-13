using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StateManager))]
public class StateManager_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		{
			StateManager stateManager = target as StateManager;
			foreach (var state in stateManager.states)
			{
				if (GUILayout.Button(state + ": enter"))
				{
					if (stateManager != null)
					{
						stateManager.ChangeState(state);
					}
				}
			}
		}
	}
}
