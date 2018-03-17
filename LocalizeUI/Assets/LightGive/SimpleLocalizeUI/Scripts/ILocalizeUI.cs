using UnityEngine;
using System.Security.Cryptography.X509Certificates;

namespace LightGive
{
	public interface ILocalizeUI
	{
		void ChangeLanguage(SystemLanguage _language);
	}
}