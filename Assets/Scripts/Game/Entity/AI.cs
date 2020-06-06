using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	Entity entity;
    void Start()
    {
		entity = GetComponent<Entity>();
    }

    
    void Update()
    {
		entity.position += entity.directon * entity.speed * Time.deltaTime;
    }
}
