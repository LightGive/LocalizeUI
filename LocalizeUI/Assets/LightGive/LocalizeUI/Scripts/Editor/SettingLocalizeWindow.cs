using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.CodeDom;
using System.Diagnostics;
using UnityEngine.Rendering;
 
public class SettingLocalizeWindow : EditorWindow
{
    private List<SystemLanguage> correspondenceLanguageList = new List<SystemLanguage>();
    private List<string> addLanguageList = new List<string>();
    private int selectLanguageNo = 0;

    [MenuItem("Tools/LightGive/Localize/Setting")]
    static void Open()
    {
        EditorWindow.GetWindow<SettingLocalizeWindow>("Setting");
    }

    void OnGUI()
    {
        GUI.color = Color.white;
        EditorGUILayout.BeginVertical(GUI.skin.box);
        for (int i = 0; i < correspondenceLanguageList.Count;i++)
        {
            EditorGUILayout.LabelField(correspondenceLanguageList[i].ToString());
        }
        EditorGUILayout.EndVertical();

        if (addLanguageList.Count != 0)
        {
            EditorGUILayout.BeginHorizontal();
            selectLanguageNo = EditorGUILayout.Popup(selectLanguageNo, addLanguageList.ToArray());
            if (GUILayout.Button("Add language."))
            {
                correspondenceLanguageList.Add((SystemLanguage)Enum.Parse(typeof(SystemLanguage), addLanguageList[selectLanguageNo]));
                selectLanguageNo = 0;
                SaveLanguageList();
                LoadLanguageList();
            }
            EditorGUILayout.EndHorizontal();
        }

        GUI.color = Color.red;
        if (GUILayout.Button("Delete."))
        {
            PlayerPrefs.DeleteKey(LocalizeDefine.SaveKeyLanguageList);
            LoadLanguageList();
        }
    }

    void OnEnable()
    {
        LoadLanguageList();
    }

    void SaveLanguageList()
    {
        var saveStr = "";
        for (int i = 0; i < correspondenceLanguageList.Count;i++)
        {
            saveStr += correspondenceLanguageList[i].ToString();
            if (i != correspondenceLanguageList.Count - 1)
                saveStr += ",";
        }
        PlayerPrefs.SetString(LocalizeDefine.SaveKeyLanguageList, saveStr);
    }

    void LoadLanguageList()
    {
        var str = PlayerPrefs.GetString(LocalizeDefine.SaveKeyLanguageList, "");
        UnityEngine.Debug.Log("LoadString : " + str);
        string[] langList;
        if (str != "")
        {
            langList = str.Split(',');
        }
        else
        {
            langList = new string[0];
        }


        correspondenceLanguageList = new List<SystemLanguage>();

        for (int i = 0; i < langList.Length; i++)
        {
            UnityEngine.Debug.Log(i.ToString("00") + " : " + langList[i]);
            SystemLanguage lang = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), langList[i]);
            correspondenceLanguageList.Add(lang);
        }

        //EnumをStringの配列に変換
        string[] enumNames = System.Enum.GetNames(typeof(SystemLanguage));

        addLanguageList = new List<string>();
        for (int i = 0; i < enumNames.Length;i++)
        {
            SystemLanguage lang = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), enumNames[i]);
            if (!correspondenceLanguageList.Contains(lang))
                addLanguageList.Add(lang.ToString());
        }
    }
}