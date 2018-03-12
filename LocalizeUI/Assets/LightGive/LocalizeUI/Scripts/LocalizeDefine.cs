using UnityEngine;

public static class LocalizeDefine
{
	public const string SaveKeyLanguageList = "SaveKeyLanguageList";

	public static int LanguageNum
	{
		get
		{
			return System.Enum.GetNames(typeof(SystemLanguage)).Length;
		}
	}
}