using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// UIをローカライズさせるエディタ拡張
/// </summary>
public class LocalizeUI : MonoBehaviour {

	//Button(jp)
	public Sprite buttonOnJp;
	public Sprite buttonOffJp;
	public Sprite buttonDisableJp;
	//Button(eng)
	public Sprite buttonOnEng;
	public Sprite buttonOffEng;
	public Sprite buttonDisableEng;

	public SpriteState mSpriteStateJp;
	public SpriteState mSpriteStateEng;

	private Button mButton;

	//Image
	public Sprite imageJp;
	public Sprite imageEng;
	public Image mImage;

	//Text
	public string strJp;
	public string strEng;
	public Text mText;

	//ローカライズさせるUIのタイプ
	public UiType uiType = UiType.Button;

	public enum UiType{
		Button,
		Image,
		Text
	};

	void Awake(){

		//指定のUIのコンポーネントを取得
		switch(uiType){
		case UiType.Button:
			mButton = this.gameObject.GetComponent<Button> ();
			break;
		case UiType.Image:
			mImage = this.gameObject.GetComponent<Image> ();
			break;
		case UiType.Text:
			mText = this.gameObject.GetComponent<Text> ();
			break;
		}

		ChangeImage ();
	}

	/// <summary>
	/// 変更
	/// </summary>
	public void ChangeImage()
	{
		//UIの種類によって変える
		switch (uiType)
		{
			case UiType.Button:

				//ボタンの場合
				mButton.image.sprite = IsEnglish() ? buttonOnEng : buttonOnJp;
				mButton.spriteState = IsEnglish() ? mSpriteStateEng : mSpriteStateJp;
				break;

			case UiType.Image:

				//Imageの時
				mImage.sprite = IsEnglish() ? imageEng : imageJp;
				break;

			case UiType.Text:

				//Textの時
				mText.text = IsEnglish() ? strEng : strJp;
				break;
		}
	}

	/// <summary>
	/// 英語かどうか
	/// </summary>
	public bool IsEnglish(){

		return true;

		if (Application.systemLanguage.ToString () == "Japanese")
			return false;
		else
			return true;
	}
}

#if UNITY_EDITOR

/// <summary>
/// インスペクタの拡張
/// Button、Image、Textがローカライズ可能
/// </summary>
/// 
[CustomEditor(typeof(LocalizeUI))]
[CanEditMultipleObjects]
public class LocalizeEditor : Editor {

	public override void OnInspectorGUI() {
		LocalizeUI local = target as LocalizeUI;
		local.uiType = (LocalizeUI.UiType)EditorGUILayout.EnumPopup ("UIType", local.uiType);

		if (local.uiType == LocalizeUI.UiType.Button) {

			EditorGUILayout.LabelField("English", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("mSpriteStateEng"), true);
			EditorGUILayout.Space();
			EditorGUILayout.LabelField ("Japanese",EditorStyles.boldLabel);
			EditorGUILayout.PropertyField( serializedObject.FindProperty("mSpriteStateJp"), true);


		} else if (local.uiType == LocalizeUI.UiType.Image) {
			EditorGUILayout.LabelField ("English",EditorStyles.boldLabel);
			local.imageEng = (Sprite)EditorGUILayout.ObjectField (local.imageEng, typeof(Sprite), true);
			EditorGUILayout.Space ();
			EditorGUILayout.LabelField ("Japanese",EditorStyles.boldLabel);
			local.imageJp = (Sprite)EditorGUILayout.ObjectField (local.imageJp, typeof(Sprite), true);

		} else if (local.uiType == LocalizeUI.UiType.Text) {
			EditorGUILayout.LabelField ("English",EditorStyles.boldLabel);
			local.strEng = EditorGUILayout.TextField (local.strEng);
			EditorGUILayout.Space ();
			EditorGUILayout.LabelField ("Japanese",EditorStyles.boldLabel);
			local.strJp = EditorGUILayout.TextField (local.strJp);
		}
		serializedObject.ApplyModifiedProperties();
		EditorUtility.SetDirty( target );
	}
}
#endif



