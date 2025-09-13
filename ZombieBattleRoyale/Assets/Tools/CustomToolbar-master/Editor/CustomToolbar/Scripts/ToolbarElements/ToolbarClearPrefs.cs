using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityToolbarExtender;

[Serializable]
internal class ToolbarClearPrefs : BaseToolbarElement {
	private static GUIContent clearPlayerPrefsBtn;

	public override string NameInList => "[Button] Clear prefs";
	public override int SortingGroup => 4;

	public override void Init() {
		clearPlayerPrefsBtn = EditorGUIUtility.IconContent("SaveFromPlay");
		clearPlayerPrefsBtn.tooltip = "Clear player prefs";
	}

	protected override void OnDrawInList(Rect position) {

	}

	protected override void OnDrawInToolbar() {
		if (GUILayout.Button(clearPlayerPrefsBtn, ToolbarStyles.commandButtonStyle)) 
		{
			System.IO.DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
			foreach (FileInfo file in di.GetFiles())
				file.Delete();
			foreach (DirectoryInfo dir in di.GetDirectories())
				dir.Delete(true);
			PlayerPrefs.DeleteAll();
			Debug.Log("Clear Player Prefs and persistent data path: " + Application.persistentDataPath);
		}
	}
}
