using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizeText))]
public class LocalizeTextEditor : Editor
{
	private SerializedProperty m_propTextList;

	private void OnEnable()
	{
		m_propTextList = serializedObject.FindProperty("m_textList");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();


		SerializedProperty arraySizeProp = m_propTextList.FindPropertyRelative("Array.size");
		//EditorGUILayout.PropertyField(arraySizeProp);
		//EditorGUI.indentLevel++;
	
		List<SystemLanguage> languageList = SettingLocalizeWindow.GetSaveDataToLangList(PlayerPrefs.GetString(LocalizeDefine.SaveKeyLanguageList, ""));
		for (int i = 0; i < arraySizeProp.intValue; i++)
		{
			for (int j = 0; j < languageList.Count; j++)
			{
				string labelText = ((SystemLanguage)i).ToString();
				if ((SystemLanguage)i == languageList[j])
				{
					EditorGUILayout.BeginHorizontal();
					GUIStyle style = new GUIStyle();
					style.fontStyle = FontStyle.Bold;


					EditorGUILayout.LabelField(labelText, GUILayout.Width(100));
					EditorGUILayout.PropertyField(
						m_propTextList.GetArrayElementAtIndex(i),style,
						new GUIContent(""));
					);

					EditorGUILayout.EndHorizontal();
				}
			}
		}
		//EditorGUI.indentLevel--;




		serializedObject.ApplyModifiedProperties();
	}

}