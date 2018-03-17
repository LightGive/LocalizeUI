using System;
using System.Collections;
using System.Collections.Generic;
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

		public static SystemLanguage GetLanguage(int _languageNo)
		{
			return (SystemLanguage)_languageNo;
		}
		public static SystemLanguage GetLanguage(string _languageStr)
		{
			return (SystemLanguage)Enum.Parse(typeof(SystemLanguage), _languageStr);
		}
	}
}