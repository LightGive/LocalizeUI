using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LightGive
{
	public class LocalizeSettingData : ScriptableObject
	{
		/// <summary>
		/// 言語に対応するかどうか
		/// </summary>
		private bool[] m_isCorrespondence = new bool[LocalizeDefine.LanguageNum];
		private int m_nowLnaguageNo = 10;
		private List<string> m_correspondenceLanguageNameList;
		private List<string> m_notCorrespondenceLanguageNameList;



		#region Property
		public SystemLanguage NowLanguage
		{
			get { return (SystemLanguage)m_nowLnaguageNo; }
		}
		public bool[] IsCorrespondence
		{
			get { return m_isCorrespondence; }
			set 
			{
				m_isCorrespondence = value;
				ChangeNameList();
			}
		}
		public List<string> CorrespondenceLanguageNameList
		{
			get { return m_correspondenceLanguageNameList; }
		}
		public List<string> NotCorrespondenceLanguageNameList
		{
			get { return m_notCorrespondenceLanguageNameList; }
		}
		#endregion

		private void ChangeNameList()
		{
			List<string> tmpList1 = new List<string>();
			List<string> tmpList2 = new List<string>();
			for (int i = 0; i < LocalizeDefine.LanguageNum; i++)
			{
				if (IsCorrespondence[i])
					tmpList1.Add(((SystemLanguage)i).ToString());
				else
					tmpList2.Add(((SystemLanguage)i).ToString());
			}

			m_correspondenceLanguageNameList = tmpList1;
			m_notCorrespondenceLanguageNameList = tmpList2;
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
