using System;
 
using UnityEngine;
 

// ---------------
//  WeaponType => GameObject
// ---------------
[Serializable]
public class MissleTypeGameObjectDictionary : SerializableDictionary<Weapon.TypeMissle, GameObject> { }


// ---------------
//  Entite => int
// ---------------
[Serializable]
public class EntityTimeDictionary : SerializableDictionary<Entity, float> { }

// ---------------
//  bonus => int
// ---------------
[Serializable]
public class BonusIntDictionary : SerializableDictionary<Bonus, int> { }



// ---------------
//  GameObject => float
// ---------------
[Serializable]
public class GameObjectFloatDictionary : SerializableDictionary<string, float> { }
