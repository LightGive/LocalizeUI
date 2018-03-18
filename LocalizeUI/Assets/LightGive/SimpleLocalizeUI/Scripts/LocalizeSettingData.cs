using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LightGive
{
	public class LocalizeSettingData : ScriptableObject
	{
		[SerializeField]
		private Font[] m_fontList = new Font[LocalizeDefine.LanguageNum];
		[SerializeField]
		private bool[] m_isCorrespondence = new bool[LocalizeDefine.LanguageNum];
		[SerializeField]
		private int m_nowLnaguageNo = 10;
		[SerializeField]
		private List<string> m_correspondenceLanguageNameList;
		[SerializeField]
		private List<string> m_notCorrespondenceLanguageNameList;

		public LocalizeSettingData()
		{
			m_nowLnaguageNo = 10;
			m_isCorrespondence[(int)SystemLanguage.English] = true;
			m_isCorrespondence[(int)SystemLanguage.Japanese] = true;
			m_isCorrespondence[(int)SystemLanguage.Chinese] = true;
		}

		#region Property
		public SystemLanguage NowLanguage
		{
			get { return (SystemLanguage)m_nowLnaguageNo; }
			set
			{
				AssetDatabase.StartAssetEditing();

				m_nowLnaguageNo = (int)value;

				AssetDatabase.StopAssetEditing();
				EditorUtility.SetDirty(this);
				AssetDatabase.SaveAssets();
			}
		}

		public int NowLnaguageNo
		{
			get { return m_nowLnaguageNo; }
			set { m_nowLnaguageNo = value; }
		}

		public Font[] FontList
		{
			get
			{
				return m_fontList;
			}
			set
			{
				AssetDatabase.StartAssetEditing();

				m_fontList = value;

				AssetDatabase.StopAssetEditing();
				EditorUtility.SetDirty(this);
				AssetDatabase.SaveAssets();
			}
		}

		public bool[] IsCorrespondence
		{
			get
			{
				return m_isCorrespondence;
			}
			set
			{
				AssetDatabase.StartAssetEditing();

				m_isCorrespondence = value;

				AssetDatabase.StopAssetEditing();
				EditorUtility.SetDirty(this);
				AssetDatabase.SaveAssets();
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
			AssetDatabase.StartAssetEditing();

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

			AssetDatabase.StopAssetEditing();
			EditorUtility.SetDirty(this);
			AssetDatabase.SaveAssets();
		}


		private void OnEnable()
		{
			ChangeNameList();

			for (int i = 0; i < LocalizeDefine.LanguageNum; i++)
			{
				if (!m_fontList[i])
					m_fontList[i] = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
			}
		}

		private void OnDisable()
		{
			
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
