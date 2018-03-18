using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace LightGive
{
	[CustomEditor(typeof(LocalizeImage))]
	[CanEditMultipleObjects]
	public class LocalizeImageEditor : Editor
	{
		private SerializedProperty m_propImageList;
		private SerializedProperty m_propRectSizeList;
		private SerializedProperty m_propChangeRectSize;

		private void OnEnable()
		{
			m_propImageList = serializedObject.FindProperty("m_spriteList");
			m_propRectSizeList = serializedObject.FindProperty("m_rectSizeList");
			m_propChangeRectSize = serializedObject.FindProperty("m_changeRectSize");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("ChangeRectSize", GUILayout.Width(100));
			m_propChangeRectSize.intValue = GUILayout.Toolbar(m_propChangeRectSize.intValue, new string[] { "Off", "On", "Native Size" });
			EditorGUILayout.EndHorizontal();

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

						if (m_propChangeRectSize.intValue == 1)
						{
							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("RectSize", GUILayout.Width(65));
							m_propRectSizeList.GetArrayElementAtIndex(i).vector2Value = EditorGUILayout.Vector2Field("", m_propRectSizeList.GetArrayElementAtIndex(i).vector2Value);
							EditorGUILayout.EndHorizontal();
						}
						else if(m_propChangeRectSize.intValue == 2)
						{
							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("RectSize", GUILayout.Width(65));
							Vector2 vec = Vector2.zero;
							if ((Sprite)m_propImageList.GetArrayElementAtIndex(i).objectReferenceValue != null)
							{
								vec = new Vector2(((Sprite)m_propImageList.GetArrayElementAtIndex(i).objectReferenceValue).rect.width, ((Sprite)m_propImageList.GetArrayElementAtIndex(i).objectReferenceValue).rect.height);
							}
							EditorGUI.BeginDisabledGroup(true);
							EditorGUILayout.Vector2Field("", vec);
							EditorGUI.EndDisabledGroup();
							EditorGUILayout.EndHorizontal();
						}


						EditorGUI.indentLevel--;
						EditorGUILayout.Space();
					}
				}
			}
			serializedObject.ApplyModifiedProperties();

		}
	}
}