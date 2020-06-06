using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	public float kAngle = 1;
	public float t;
	private Vector3 edgeCamera;
	private Vector3 endPosition;
	private Vector3 delta;
	private Rigidbody2D rigid;
	private Entity entity;
	private Transform cam;
	

	private void Start() {
		cam = Camera.main.transform;
		entity = GetComponent<Entity>();
		rigid = GetComponent<Rigidbody2D>();
		edgeCamera = Camera.main.ViewportToWorldPoint(new Vector3(0, 0f));
		borderField = PlayManager.borderField-edgeCamera.x;
		k = borderField / (borderField + edgeCamera.x);
	}

	private void Update() {
		if (InputHelper.isTapped) {
			delta = NewDelta();
		}
		Move();
		if (InputHelper.isMoved) {
			Vector2 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Physics2D.Raycast(curMousePos, Vector2.zero)) {
				RaycastHit2D hit = Physics2D.Raycast(curMousePos, Vector2.zero);
				Vector3 pos = hit.point;
				pos.z = 0;
				pos += delta;
				endPosition = pos;
			}
		}
	}
	private float k;
	private float borderField;
	void Move() {
		Vector3 temp = NewDelta();
		Vector3 pos = endPosition;
		if (pos.x < -borderField) {
			delta.x = temp.x;
			endPosition.x = -borderField;
		}
		if (pos.x > borderField) {
			delta.x = temp.x;
			endPosition.x = borderField;
		}

		if (pos.y < edgeCamera.y) {
			delta.y = temp.y;
			endPosition.y = edgeCamera.y;
		}
		if (pos.y > -edgeCamera.y) {
			delta.y = temp.y;
			endPosition.y = -edgeCamera.y;
		}
		transform.position = endPosition;
		cam.position = new Vector3(endPosition.x / k, cam.position.y, cam.position.z);
	}

	Vector3 NewDelta() {
		Vector3 _delta = new Vector3();
		Vector2 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (Physics2D.Raycast(curMousePos, Vector2.zero)) {
			RaycastHit2D hit = Physics2D.Raycast(curMousePos, Vector2.zero);
			_delta = (Vector2)transform.position - hit.point;
			_delta.z = 0;
		}
		return _delta;
	}
}
