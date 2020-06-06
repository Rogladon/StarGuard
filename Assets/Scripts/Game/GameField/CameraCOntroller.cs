using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCOntroller : MonoBehaviour
{
	public float speedScroll;
	public float maxDelta;
	private Entity entity;

	private void Start() {
		entity = PlayManager.entityPlayer;
	}

	private void Update() {
		if (InputHelper.isMoved) {
			Vector2 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position += InputHelper.deltaPos.x*Vector3.right * speedScroll * Time.deltaTime;
		}
	}
}
