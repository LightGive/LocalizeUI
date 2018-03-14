using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LightGive
{
	public class LocalizeSettingData : ScriptableObject
	{
		private bool[] m_isCorrespondence = new bool[LocalizeDefine.LanguageNum];

		public bool[] IsCorrespondence
		{
			get { return m_isCorrespondence; }
		}

		[MenuItem("Tools/LightGive/Localize/Create SettingData")]
		static void CreateExampleAssetInstance()
		{
			string path = EditorUtility.SaveFilePanel("Create Setting Data", "Assets/", "LocalizeSetting.asset", "asset");
			if (path == "")
				return;

			path = FileUtil.GetProjectRelativePath(path);
			LocalizeSettingData data = ScriptableObject.CreateInstance<LocalizeSettingData>();
			AssetDatabase.CreateAsset(data, path);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
}
