using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace LightGive
{
	/// <summary>
	/// ローカライズの設定をする
	/// </summary>
	public class SettingLocalizeWindow : EditorWindow
	{
		/// <summary>
		/// 対応するかどうか
		/// </summary>
		private bool[] m_isCorrespondence = new bool[LocalizeDefine.LanguageNum];


		private List<SystemLanguage> m_correspondenceLanguageList = new List<SystemLanguage>();
		private List<string> m_addLanguageList = new List<string>();
		private int m_addSelectLanguageNo = 0;

		//private List<string> m_CorrespondenceLanguageEnumList
		//{
		//	get
		//	{
		//		List<string> labelList = new List<string>();
		//		for (int i = 0; i < m_correspondenceLanguageList.Count;i++)
		//		{
		//			//if(m_correspondenceLanguageList[i].ToString() == )
		//		}
		//		return labelList;
		//	}
		//}




		[MenuItem("Tools/LightGive/Localize/Setting")]
		static void Open()
		{
			EditorWindow.GetWindow<SettingLocalizeWindow>("SettingLocalize");
		}

		void OnGUI()
		{
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("NowLanguage");
			//EditorGUILayout.EnumPopup()
			GUI.color = Color.white;
			EditorGUILayout.BeginVertical(GUI.skin.box);
			for (int i = 0; i < m_correspondenceLanguageList.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(m_correspondenceLanguageList[i].ToString());
				if(GUILayout.Button("Remove"))
				{
					m_correspondenceLanguageList.Remove(m_correspondenceLanguageList[i]);
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();

			if (m_addLanguageList.Count != 0)
			{
				EditorGUILayout.BeginHorizontal();
				m_addSelectLanguageNo = EditorGUILayout.Popup(m_addSelectLanguageNo, m_addLanguageList.ToArray());
				if (GUILayout.Button("Add language."))
				{
					m_correspondenceLanguageList.Add((SystemLanguage)Enum.Parse(typeof(SystemLanguage), m_addLanguageList[m_addSelectLanguageNo]));
					m_addSelectLanguageNo = 0;
					SaveLanguageList();
					LoadLanguageList();
				}
				EditorGUILayout.EndHorizontal();
			}


			GUI.color = Color.red;
			if (GUILayout.Button("All Delete"))
			{
				PlayerPrefs.DeleteKey(LocalizeDefine.SaveKeyLanguageList);
				LoadLanguageList();
			}
		}

		/// <summary>
		/// 初期化
		/// </summary>
		void OnEnable()
		{
			LoadLanguageList();
		}



		/// <summary>
		/// ロードする
		/// </summary>
		void LoadLanguageList()
		{
			m_correspondenceLanguageList = new List<SystemLanguage>();
			var loadStr = PlayerPrefs.GetString(LocalizeDefine.SaveKeyLanguageList, "");
			m_correspondenceLanguageList = GetSaveDataToLangList(loadStr);

			//EnumをStringの配列に変換
			string[] enumNames = System.Enum.GetNames(typeof(SystemLanguage));

			m_addLanguageList = new List<string>();
			for (int i = 0; i < enumNames.Length; i++)
			{
				SystemLanguage lang = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), enumNames[i]);
				if (!m_correspondenceLanguageList.Contains(lang))
					m_addLanguageList.Add(lang.ToString());
			}
		}

		/// <summary>
		/// セーブする
		/// </summary>
		void SaveLanguageList()
		{
			var saveStr = "";
			for (int i = 0; i < m_correspondenceLanguageList.Count; i++)
			{
				saveStr += m_correspondenceLanguageList[i].ToString();
				if (i != m_correspondenceLanguageList.Count - 1)
					saveStr += ",";
			}
			PlayerPrefs.SetString(LocalizeDefine.SaveKeyLanguageList, saveStr);
		}


		/// <summary>
		/// ロードした文字からリストにして返す
		/// </summary>
		/// <returns>The save data to lang list.</returns>
		/// <param name="_str">String.</param>
		public static List<SystemLanguage> GetSaveDataToLangList(string _str)
		{
			List<SystemLanguage> langEnumList = new List<SystemLanguage>();

			//デバッグ用
			//UnityEngine.Debug.Log("LoadString : " + _str);

			string[] langList;
			if (_str != "")
			{
				langList = _str.Split(',');
			}
			else
			{
				langList = new string[0];
			}

			for (int i = 0; i < langList.Length; i++)
			{
				//デバッグ用
				//UnityEngine.Debug.Log(i.ToString("00") + " : " + langList[i]);
				SystemLanguage lang = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), langList[i]);
				langEnumList.Add(lang);
			}

			return langEnumList;
		}
	}
}