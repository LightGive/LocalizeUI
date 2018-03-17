using UnityEngine;
using UnityEngine.UI;

namespace LightGive
{
	/// <summary>
	/// テキストをローカライズさせる
	/// </summary>
	[RequireComponent(typeof(Text))]
	public class LocalizeText : MonoBehaviour, ILocalizeUI
	{
		[SerializeField]
		private string[] m_textList = new string[LocalizeDefine.LanguageNum];
		[SerializeField]
		private int[] m_fontSizeList = new int[LocalizeDefine.LanguageNum];
		[SerializeField]
		private Vector2[] m_rectSizeList = new Vector2[LocalizeDefine.LanguageNum];
		[SerializeField]
		private bool m_isChangeRectSize;
		[SerializeField]
		private bool m_isChangeFontSize;

		private Text m_mainText;
		public Text MainText
		{
			get
			{
				if (!m_mainText)
				{
					if (this.gameObject == null)
						return null;
					m_mainText = this.gameObject.GetComponent<Text>();
				}
				return m_mainText;
			}
		}

		void Reset()
		{
			for (int i = 0; i < LocalizeDefine.LanguageNum; i++)
			{
				m_textList[i] = MainText.text;
				m_fontSizeList[i] = MainText.fontSize;
				m_rectSizeList[i] = MainText.rectTransform.sizeDelta;
			}
			LocalizeSystem.AddLocalizeUI(this);
		}

		void OnEnable()
		{
			LocalizeSystem.AddLocalizeUI(this);
		}

		void OnDisable()
		{
			LocalizeSystem.RemoveLocalizeUI(this);
		}

		public void ChangeLanguage(SystemLanguage _language)
		{
			int index = (int)_language;
			MainText.text = m_textList[index];

			if (m_isChangeRectSize)
			{
				MainText.rectTransform.sizeDelta = m_rectSizeList[index];
			}
			if (m_isChangeFontSize)
			{
				MainText.fontSize = m_fontSizeList[index];
			}
		}
	}
}