using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LocalizeSettingData : ScriptableObject
{
	[SerializeField]
	string str;

	[SerializeField, Range(0, 10)]
	int number;

	[MenuItem("Tools/LightGive/Localize/CreateSettingData")]
	static void CreateExampleAssetInstance()
	{
		var exampleAsset = CreateInstance<LocalizeSettingData>();

		AssetDatabase.CreateAsset(exampleAsset, "Assets/Editor/ExampleAsset.asset");
		AssetDatabase.Refresh();
	}
}
