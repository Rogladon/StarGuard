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
	string path (Action s) {
		return "Assets/ScriptableObjects/EnemyAI/" + ai.name + s.type + id.ToString()+".asset";
	}
	void OnEnable() {
		ai = (AI)target;
		_enum = serializedObject.FindProperty("_enum");
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		List<Action> actions = ai.actions;
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
					actions.Add(s);
					break;
				case AI.sta.Shoot:
					Shoot sh = CreateInstance<Shoot>();
					id = 0;
					while (AssetDatabase.IsMainAssetAtPathLoaded(path(sh))) {
						id++;
					}
					AssetDatabase.CreateAsset(sh, path(sh));
					AssetDatabase.Refresh();
					actions.Add(sh);
					break;
				case AI.sta.RootRandom:
					RootRandom rR = CreateInstance<RootRandom>();
					id = 0;
					while (AssetDatabase.IsMainAssetAtPathLoaded(path(rR))) {
						id++;
					}
					AssetDatabase.CreateAsset(rR, path(rR));
					AssetDatabase.Refresh();
					actions.Add(rR);
					break;
			}
		}
		EditorGUILayout.EndHorizontal();
		List<Action> removeStat = new List<Action>();
		foreach (var i in actions) {
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
				case "RootRandom":
					RootRandom rr = (RootRandom)i;
					Editor.CreateEditor(rr).OnInspectorGUI();
					break;
			}
			EditorGUILayout.Space(10);
			EditorGUILayout.LabelField("--------------------------------------------------------------");
			
		}

		foreach(var i in removeStat) {
			actions.Remove(i);
		}
		//EditorUtility.SetDirty(ai);
		serializedObject.ApplyModifiedProperties();
	}
}