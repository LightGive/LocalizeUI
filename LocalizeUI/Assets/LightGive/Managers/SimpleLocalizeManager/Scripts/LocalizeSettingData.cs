using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ローカライズの設定用データ
/// </summary>
[CreateAssetMenu(menuName = SettingDataPath, fileName = SettingDataName)]
public class LocalizeSettingData : ScriptableObject
{
	public const string SettingDataName = "LocalizeSettingData";
	public const string SettingDataPath = "LightGive/Create Localize SettingData";

	public class LocalizeContent
	{
		private Font m_font;
	}
}