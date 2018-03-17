using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LightGive
{
	[CustomEditor(typeof(LocalizeImage))]
	[CanEditMultipleObjects]
	public class LocalizeImageEditor : Editor
	{
		private SerializedProperty m_propImageList;

		private void OnEnable()
		{
			m_propImageList = serializedObject.FindProperty("m_spriteList");

		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			LocalizeImage localizeText = target as LocalizeImage;
			EditorGUILayout.Space();
			//EditorGUILayout.BeginHorizontal();


			SerializedProperty arraySizeProp = m_propImageList.FindPropertyRelative("Array.size");
			EditorGUILayout.Space();
			List<SystemLanguage> languageList = LocalizeSystem.GetCorrespondenceLanguageList();
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
						m_propImageList.GetArrayElementAtIndex(i).objectReferenceValue = EditorGUILayout.ObjectField(m_propImageList.GetArrayElementAtIndex(i).objectReferenceValue, typeof(Sprite),false);
						EditorGUI.indentLevel--;
						EditorGUILayout.Space();
					}
				}
			}
			serializedObject.ApplyModifiedProperties();

		}
	}
}