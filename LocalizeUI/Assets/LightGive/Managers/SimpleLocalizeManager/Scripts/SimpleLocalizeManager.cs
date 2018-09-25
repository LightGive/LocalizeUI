using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightGive;

public class SimpleLocalizeManager : SingletonMonoBehaviour<SimpleLocalizeManager>
{
	[SerializeField]
	private LocalizeSettingData m_settingData;

	protected override void Awake()
	{
		base.isDontDestroy = true;
		base.Awake();
	}

	void Init()
	{
		m_settingData = Resources.Load<LocalizeSettingData>(LocalizeSettingData.SettingDataName);
	}
}