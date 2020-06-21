#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_0_1 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_5
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace TextFx
{
	[CustomEditor(typeof(TextFxUGUI))]
	public class TextFxUGUI_Inspector : UnityEditor.UI.TextEditor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();

			GUILayout.Space(10);

			GUILayout.Label ("TextFx", EditorStyles.boldLabel);

			if (GUILayout.Button("Open Animation Editor", GUILayout.Width(150)))
			{
				TextEffectsManager.Init();
			}
		}
	}
}
#endif