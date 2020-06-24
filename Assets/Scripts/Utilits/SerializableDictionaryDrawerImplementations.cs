using UnityEngine;
using UnityEngine.UI;
 
using UnityEditor;

#if UNITY_EDITOR

// ---------------
//  WeaponType => Missle
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(MissleTypeGameObjectDictionary))]
public class MissleTypeGameObjectDictionaryDrawer : SerializableDictionaryDrawer<Weapon.TypeMissle, Missle> {
	protected override SerializableKeyValueTemplate<Weapon.TypeMissle, Missle> GetTemplate() {
		return GetGenericTemplate<MissleTypeGameObjectDictionaryTemplate>();
	}
}
internal class MissleTypeGameObjectDictionaryTemplate : SerializableKeyValueTemplate<Weapon.TypeMissle, Missle> { }


// ---------------
//  Entite => float
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(EntityTimeDictionary))]
public class EntityTimeDictionaryDrawer : SerializableDictionaryDrawer<Entity, float> {
	protected override SerializableKeyValueTemplate<Entity, float> GetTemplate() {
		return GetGenericTemplate<EntityTimeDictionaryTemplate>();
	}
}
internal class EntityTimeDictionaryTemplate : SerializableKeyValueTemplate<Entity, float> { }
// ---------------
//  Bonus => int
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(BonusIntDictionary))]
public class BonusIntDictionaryDrawer : SerializableDictionaryDrawer<Bonus, int> {
	protected override SerializableKeyValueTemplate<Bonus, int> GetTemplate() {
		return GetGenericTemplate<BonusIntDictionaryTemplate>();
	}
}
internal class BonusIntDictionaryTemplate: SerializableKeyValueTemplate<Bonus, int> { }

// ---------------
//  GameObject => float
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(GameObjectFloatDictionary))]
public class GameObjectFloatDictionaryDrawer : SerializableDictionaryDrawer<string, float> {
	protected override SerializableKeyValueTemplate<string, float> GetTemplate() {
		return GetGenericTemplate<GameObjectFloatDictionaryTemplate>();
	}
}
internal class GameObjectFloatDictionaryTemplate : SerializableKeyValueTemplate<string, float> { }


// ---------------
//  TypeTrack => AudioTrack
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(TypeAudioTrackDictionary))]
public class TypeAudioTrackDictionaryDrawer : SerializableDictionaryDrawer<AudioManager.TypeTrack, AudioClip> {
	protected override SerializableKeyValueTemplate<AudioManager.TypeTrack, AudioClip> GetTemplate() {
		return GetGenericTemplate<TypeAudioTrackDictionaryTemplate>();
	}
}
internal class TypeAudioTrackDictionaryTemplate : SerializableKeyValueTemplate<AudioManager.TypeTrack, AudioClip> { }



#endif
