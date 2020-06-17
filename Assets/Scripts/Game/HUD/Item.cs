using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
	[SerializeField]
	public Image icon;
	[SerializeField]
	public Button btn;
	Skin skin;
	Mall mall;
	public bool has {
		get {
			if (skin.has == 0) return false;
			return true;
		}
	}

	public void Init(Skin s, Mall m) {
		skin = s;
		mall = m;
		SetIcon();
		if (has) {
			ActiveIcon();
		} else {
			UnActiveIcon();
		}
		btn.interactable = has;
		btn.onClick.AddListener(() => {
			GameManager.events.choiceSkin.Invoke(skin.pack,skin.index);
			Choice();
		});
	}

	public void BySelf() {
		GameManager.events.bySkin.Invoke(skin.pack, skin.index);
		ActiveIcon();
		btn.interactable = has;
	}

	private void SetIcon() {
		icon.sprite = skin.icon;
		float width = skin.icon.rect.size.x;
		float height = skin.icon.rect.size.y;
		//icon.rectTransform.sizeDelta = GetComponent<RectTransform>().sizeDelta;
		Vector3 scale = icon.transform.localScale;
		Debug.Log(width + "  " + height);
		if (width < height) {

			scale.x = width / height;
		} else {
			scale.y = height / width;
		}
		icon.transform.localScale = scale;
	}

	private void ActiveIcon() {
		icon.color = Color.white;
	}

	private void UnActiveIcon() {
		icon.color = Color.black;
	}

	private void Choice() {
		mall.InstSheep(skin);
	}
}
