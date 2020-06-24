using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	public float kAngle = 1;
	public float t;
	public Vector3 edgeCamera;
	public Vector3 endPosition;
	public Vector3 delta;
	private Rigidbody2D rigid;
	private Entity entity;
	private Transform cam;
	public Transform c;
	public bool lockMove = true;
	

	private void Start() {
		AudioManager.events.gameTrack.Invoke();
		cam = Camera.main.transform;
		entity = GetComponent<Entity>();
		rigid = GetComponent<Rigidbody2D>();
		edgeCamera = Camera.main.ViewportToWorldPoint(new Vector3(0, 0f));
		borderField = PlayManager.borderField-edgeCamera.x;
		k = borderField / (borderField + edgeCamera.x);
		//curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private void Update() {
		if (lockMove) return;
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
				pos += (delta-cam.position);
				endPosition = pos;
			}
		}
	}
	private float k;
	public float borderField;
	void Move() {
		Vector3 temp = NewDelta();
		//delta.x =temp.x - cam.position.x;
		Vector3 pos = endPosition;
		if (pos.x < edgeCamera.x) {
			delta.x = temp.x;
			endPosition.x = edgeCamera.x;
		}
		if (pos.x > -edgeCamera.x) {
			delta.x = temp.x;
			endPosition.x = -edgeCamera.x;
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
