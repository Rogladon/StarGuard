using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CanEditMultipleObjects]
[CustomEditor(typeof(AI))]
public class SODataBehaviourEditor : Editor {
	private AI ai;
	SerializedProperty _enum;
	int id = 0;
	string path (Stat s) {
		return "Assets/ScriptableObjects/EnemyAI/" + ai.name + s.type + id.ToString()+".asset";
	}
	void OnEnable() {
		ai = (AI)target;
		_enum = serializedObject.FindProperty("_enum");
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		List<Stat> stats = ai.stats;
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(_enum);
		if (GUILayout.Button("+")) {
			switch (ai._enum) {
				case AI.sta.SimpleFly:
					SimpleFly s = CreateInstance<SimpleFly>();
					id = 0;
					while (AssetDatabase.IsMainAssetAtPathLoaded(path(s))) {
						id++;
					}
					AssetDatabase.CreateAsset(s, path(s));
					AssetDatabase.Refresh();
					stats.Add(s);
					break;
				case AI.sta.Shoot:
					Shoot sh = CreateInstance<Shoot>();
					AssetDatabase.CreateAsset(sh, path(sh));
					AssetDatabase.Refresh();
					stats.Add(sh);
					break;
			}
		}
		EditorGUILayout.EndHorizontal();
		List<Stat> removeStat = new List<Stat>();
		foreach (var i in stats) {
			EditorGUILayout.Space(10);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(i.type);
			if (GUILayout.Button("-")) {
				removeStat.Add(i);
			}
			EditorGUILayout.EndHorizontal();
			switch (i.type) {
				case "SimpleFly":
					SimpleFly s = (SimpleFly)i;
					Editor.CreateEditor(s).OnInspectorGUI();
					break;
				case "Shoot":
					Shoot sh = (Shoot)i;
					Editor.CreateEditor(sh).OnInspectorGUI();
					break;
			}
			EditorGUILayout.Space(10);
			EditorGUILayout.LabelField("--------------------------------------------------------------");
			
		}
		foreach(var i in removeStat) {
			stats.Remove(i);
			AssetDatabase.DeleteAsset(path(i));
		}
		EditorUtility.SetDirty(ai);
		serializedObject.ApplyModifiedProperties();
	}
}