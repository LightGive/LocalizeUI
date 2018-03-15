using UnityEngine;

namespace LightGive
{
	public static class LocalizeDefine
	{
		public const string FontPath = "Fonts/";
		public const string SettingPath = "LocalizeSetting";
		public const string SaveKeyBoolIsCorrespondence = "SaveKeyBoolIsCorrespondence";
		public const string SaveKeyLanguageList = "SaveKeyLanguageList";

		public static int LanguageNum
		{
			get { return System.Enum.GetNames(typeof(SystemLanguage)).Length; }
		}
	}
}