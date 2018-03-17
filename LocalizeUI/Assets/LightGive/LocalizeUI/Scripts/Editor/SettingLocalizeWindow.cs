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
		private LocalizeSettingData m_settingData;
		private int m_addSelectLanguageNo = 0;
		private int m_selectLanguageNo = 0;

		public LocalizeSettingData SettingData
		{
			get 
			{
				if (m_settingData == null)
				{
					m_settingData = Resources.Load<LocalizeSettingData>(LocalizeDefine.SettingPath);
				}
				return m_settingData;
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
			EditorGUI.BeginChangeCheck();
			{
				m_selectLanguageNo = EditorGUILayout.Popup("NowLanguage", m_selectLanguageNo, SettingData.CorrespondenceLanguageNameList.ToArray());
			}
			var isChange = EditorGUI.EndChangeCheck();
			if(isChange)
			{
				LocalizeSystem.ChangeLanguage(LocalizeDefine.GetLanguage(SettingData.CorrespondenceLanguageNameList[m_selectLanguageNo]));
			}

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
			}
		}
	}
}