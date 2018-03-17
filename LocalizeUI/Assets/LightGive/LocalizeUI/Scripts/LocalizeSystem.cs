using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightGive;

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


		public static void ChangeLanguage(SystemLanguage _lang)
		{
			for (int i = 0; i < localizeList.Count;i++)
			{
				localizeList[i].ChangeLanguage(_lang);
			}
		}

		public static void AddLocalizeUI(ILocalizeUI _localizeUI)
		{
			if (localizeList.Contains(_localizeUI))
				return;

			localizeList.Add(_localizeUI);
		}
	}
}
