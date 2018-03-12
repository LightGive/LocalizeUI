using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// テキストをローカライズさせる
/// </summary>
[RequireComponent(typeof(Text))]
public class LocalizeText : MonoBehaviour, ILocalizeUI
{
	[SerializeField]
	private string[] m_textList = new string[LocalizeDefine.LanguageNum];

	private Text m_mainText;
	public Text MainText
	{
		get
		{
			if (!m_mainText)
				m_mainText = this.gameObject.GetComponent<Text>();
			return m_mainText;
		}
	}

	void Start()
	{

	}

	void Update()
	{

	}


	public void ChangeLanguage()
	{
	}
}