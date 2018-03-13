using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightGive
{
	public static class LocalizeSystem
	{
		private static List<ILocalizeUI> localizeList = new List<ILocalizeUI>();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void Init()
		{
			//ChangeLanguage(DefaultLanguege);
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
