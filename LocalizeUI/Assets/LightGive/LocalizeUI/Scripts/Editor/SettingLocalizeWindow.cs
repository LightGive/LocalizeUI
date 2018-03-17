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
		private LocalizeSettingData m_SettingData;
		private int m_addSelectLanguageNo = 0;
		private int m_selectLanguageNo = 0;

		public LocalizeSettingData SettingData
		{
			get 
			{
				if (m_SettingData == null)
				{
					m_SettingData = Resources.Load<LocalizeSettingData>(LocalizeDefine.SettingPath);
				}
				return m_SettingData;
			}
		}


		[MenuItem("Tools/LightGive/Localize/Setting")]
		static void Open()
		{
			EditorWindow.GetWindow<SettingLocalizeWindow>("SettingLocalize");
		}

		void OnGUI()
		{
			if (SettingData == null)
			{
				EditorGUILayout.Space();
				EditorGUILayout.LabelField("Please 'Tools/LightGive/Language/Create SettingData'");
				return;
			}

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("NowLanguage", SettingData.NowLanguage.ToString());
			GUI.color = Color.white;
			EditorGUILayout.BeginVertical(GUI.skin.box);

			for (int i = 0; i < SettingData.CorrespondenceLanguageNameList.Count; i++)
			{

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(SettingData.CorrespondenceLanguageNameList[i]);
				if (GUILayout.Button("Remove"))
				{
					int removeIndex = (int)((SystemLanguage)Enum.Parse(typeof(SystemLanguage), SettingData.CorrespondenceLanguageNameList[i]));
					SettingData.IsCorrespondence[removeIndex] = false;
					SettingData.ChangeNameList();
					Repaint();
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();

			//追加する言語が0じゃ無い時
			if(SettingData.NotCorrespondenceLanguageNameList.Count != 0)
			{
				EditorGUILayout.BeginHorizontal();
				m_addSelectLanguageNo = EditorGUILayout.Popup(m_addSelectLanguageNo, SettingData.NotCorrespondenceLanguageNameList.ToArray());
				if (GUILayout.Button("Add language."))
				{
					int addIndex = (int)((SystemLanguage)Enum.Parse(typeof(SystemLanguage), SettingData.NotCorrespondenceLanguageNameList[m_addSelectLanguageNo]));
					SettingData.IsCorrespondence[addIndex] = true;
					SettingData.ChangeNameList();
					m_addSelectLanguageNo = 0;
					Repaint();
				}
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.Space();

			GUI.color = Color.red;
			if (GUILayout.Button("All Delete"))
			{
				PlayerPrefs.DeleteKey(LocalizeDefine.SaveKeyLanguageList);
				//LoadLanguageList();
			}
		}

		/// <summary>
		/// 初期化
		/// </summary>
		void OnEnable()
		{
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