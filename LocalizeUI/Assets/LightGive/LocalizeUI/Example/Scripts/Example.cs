using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightGive
{
	public class Example : MonoBehaviour
	{
		public void OnLanguageButtonwDown(ExampleLocalizeButton _langButton)
		{
			Debug.Log("通ってるぞ〜");
			LocalizeSystem.ChangeLanguage(_langButton.Language);
		}
	}
}
