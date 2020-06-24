using System;
 
using UnityEngine;


// ---------------
//  WeaponType => Missle
// ---------------
[Serializable]
public class MissleTypeGameObjectDictionary : SerializableDictionary<Weapon.TypeMissle, Missle> { }


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

// ---------------
//  TypeTRack => AudioTrack
// ---------------
[Serializable]
public class TypeAudioTrackDictionary : SerializableDictionary<AudioManager.TypeTrack, AudioClip> { }
