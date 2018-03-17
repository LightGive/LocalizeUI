using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Diagnostics;

namespace LightGive
{
	[CustomEditor(typeof(LocalizeText))]
	[CanEditMultipleObjects]
	public class LocalizeTextEditor : Editor
	{
		private SerializedProperty m_propTextList;
		private SerializedProperty m_propFontSizeList;
		private SerializedProperty m_propRectSizeList;
		private SerializedProperty m_propIsChangeRectSize;
		private SerializedProperty m_propIsChangeFontSize;

		private void OnEnable()
		{
			m_propTextList = serializedObject.FindProperty("m_textList");
			m_propFontSizeList = serializedObject.FindProperty("m_fontSizeList");
			m_propRectSizeList = serializedObject.FindProperty("m_rectSizeList");
			m_propIsChangeRectSize = serializedObject.FindProperty("m_isChangeRectSize");
			m_propIsChangeFontSize = serializedObject.FindProperty("m_isChangeFontSize");
		}

		private void Awake()
		{
			UnityEngine.Debug.Log("Awake");
			LocalizeSystem.AddLocalizeUI((LocalizeText)target);
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			LocalizeText localizeText = target as LocalizeText;

			EditorGUILayout.Space();
			EditorGUILayout.ObjectField(localizeText.MainText, typeof(Text));


			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("ChangeFontSize", GUILayout.Width(100));
			m_propIsChangeFontSize.boolValue = GUILayout.Toolbar(
				(m_propIsChangeFontSize.boolValue ? 1 : 0), new string[] { "Off", "On" }) == 1;
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("ChangeRectSize", GUILayout.Width(100));
			m_propIsChangeRectSize.boolValue = GUILayout.Toolbar(
				(m_propIsChangeRectSize.boolValue ? 1 : 0), new string[] { "Off", "On" }) == 1;
			EditorGUILayout.EndHorizontal();



			SerializedProperty arraySizeProp = m_propTextList.FindPropertyRelative("Array.size");
			EditorGUILayout.Space();
			List<SystemLanguage> languageList = SettingLocalizeWindow.GetSaveDataToLangList(PlayerPrefs.GetString(LocalizeDefine.SaveKeyLanguageList, ""));
			for (int i = 0; i < arraySizeProp.intValue; i++)
			{
				for (int j = 0; j < languageList.Count; j++)
				{
					string labelText = ((SystemLanguage)i).ToString();
					if ((SystemLanguage)i == languageList[j])
					{
						var origFontStyle = EditorStyles.label.fontStyle;
						EditorStyles.label.fontStyle = FontStyle.Bold;
						EditorGUILayout.LabelField(labelText, GUILayout.Width(100));
						EditorStyles.label.fontStyle = origFontStyle;

						EditorGUI.indentLevel++;
						m_propTextList.GetArrayElementAtIndex(i).stringValue = EditorGUILayout.TextArea(m_propTextList.GetArrayElementAtIndex(i).stringValue);

						if (m_propIsChangeFontSize.boolValue)
						{
							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("FontSize", GUILayout.Width(65));
							m_propFontSizeList.GetArrayElementAtIndex(i).intValue = Mathf.Clamp(EditorGUILayout.IntField(m_propFontSizeList.GetArrayElementAtIndex(i).intValue), 0, 300);
							EditorGUILayout.EndHorizontal();
						}

						if (m_propIsChangeRectSize.boolValue)
						{
							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("RectSize", GUILayout.Width(65));
							m_propRectSizeList.GetArrayElementAtIndex(i).vector2Value = EditorGUILayout.Vector2Field("", m_propRectSizeList.GetArrayElementAtIndex(i).vector2Value);
							EditorGUILayout.EndHorizontal();
						}

						EditorGUILayout.BeginHorizontal();
						if (m_propIsChangeFontSize.boolValue)
						{
							if (GUILayout.Button("GetFontSize"))
							{
								m_propFontSizeList.GetArrayElementAtIndex(i).intValue = localizeText.MainText.fontSize;
							}
						}
						if (m_propIsChangeRectSize.boolValue)
						{
							if (GUILayout.Button("GetRectSize"))
							{
								m_propRectSizeList.GetArrayElementAtIndex(i).vector2Value = localizeText.MainText.rectTransform.sizeDelta;
							}
						}
						EditorGUILayout.EndHorizontal();

						EditorGUI.indentLevel--;
						EditorGUILayout.Space();

					}
				}
			}

			serializedObject.ApplyModifiedProperties();
		}

	}
}