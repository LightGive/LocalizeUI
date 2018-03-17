using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.Runtime.CompilerServices;
#endif

namespace LightGive
{
	public static class LocalizeSystem
	{
		private static LocalizeSettingData settingData;
		private static List<ILocalizeUI> localizeList = new List<ILocalizeUI>();
		public static List<Font> fontList = new List<Font>();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void InitAwakeAfter()
		{
			//ChangeLanguage(DefaultLanguege);
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitAwakeBefore()
		{
			settingData = Resources.Load<LocalizeSettingData>(LocalizeDefine.SettingPath);
		}


		public static void ChangeLanguage(SystemLanguage _language)
		{
			localizeList = new List<ILocalizeUI>();
			ILocalizeUI tmp;
			foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
			{
				if (obj.activeInHierarchy)
				{
					tmp = (ILocalizeUI)obj.GetComponent(typeof(ILocalizeUI));
					if (tmp == null)
						continue;
					localizeList.Add(tmp);
				}
			}

			Debug.Log(_language.ToString() + "に言語を変更しました");
			Debug.Log("カウント" + localizeList.Count);
			for (int i = 0; i < localizeList.Count; i++)
			{
				if (localizeList[i] == null)
					continue;

				localizeList[i].ChangeLanguage(_language);
			}
		}

		public static void AddLocalizeUI(ILocalizeUI _localizeUI)
		{
			if (localizeList.Contains(_localizeUI))
				return;
			localizeList.Add(_localizeUI);
		}

		public static void RemoveLocalizeUI(ILocalizeUI _localizeUI)
		{
			localizeList.Remove(_localizeUI);
		}

#if UNITY_EDITOR
		[MenuItem("Tools/LightGive/Localize/ResetList")]
		static void ResetList()
		{
			localizeList = new List<ILocalizeUI>();
		}
#endif
	}
}
