﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedZone : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision) {
		if(!collision.transform.CompareTag("NoDestroy"))
		Destroy(collision.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (!collision.transform.CompareTag("NoDestroy"))
			Destroy(collision.gameObject);
	}
}
