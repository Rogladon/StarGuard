using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
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
		if (has) {
			GetComponent<Image>().sprite = s.iconHas;
		} else {
			GetComponent<Image>().sprite = s.iconNoHas;
		}
		GetComponent<Button>().interactable = has;
		GetComponent<Button>().onClick.AddListener(() => {
			GameManager.events.choiceSkin.Invoke(skin.pack,skin.index);
			Choice();
		});
	}

	public void BySelf() {
		GameManager.events.bySkin.Invoke(skin.pack, skin.index);
		GetComponent<Image>().sprite = skin.iconHas;
		GetComponent<Button>().interactable = has;
	}

	private void Choice() {
		mall.InstSheep(skin);
	}
}
