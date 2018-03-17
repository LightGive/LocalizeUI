using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
			get
			{
				return m_isCorrespondence;
			}
			set
			{
				m_isCorrespondence = value;
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

		public void ChangeNameList()
		{
			List<string> tmpList1 = new List<string>();
			List<string> tmpList2 = new List<string>();
			for (int i = 0; i < LocalizeDefine.LanguageNum; i++)
			{
				if (m_isCorrespondence[i])
					tmpList1.Add(((SystemLanguage)i).ToString());
				else
					tmpList2.Add(((SystemLanguage)i).ToString());
			}

			m_correspondenceLanguageNameList = new List<string>(tmpList1);
			m_notCorrespondenceLanguageNameList = new List<string>(tmpList2);
		}


		private void OnEnable()
		{
			ChangeNameList();
		}

#if UNITY_EDITOR

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
#endif

	}
}
