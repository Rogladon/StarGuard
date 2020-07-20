using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMainStateEditor : MonoBehaviour
{
	[Header("Components")]
	public Slider sliderSize;
	public Slider sliderTime;
	public Slider sliderSpeed;
	public Slider sliderStartTime;
	public Slider sliderBonus;

	int id => LevelStateEditor.levelID;
	LevelManager.Stage stage;
	public void Start() {
		stage = LevelManager.stages.stages[id];
		sliderSize.value = stage.size;
		sliderSpeed.value = stage.kSpeed;
		sliderTime.value = stage.kTime;
		sliderStartTime.value = stage.startTime;
		sliderBonus.value = stage.kBonus;
	}
	public void Update() {
		stage.size = (int)sliderSize.value;
		stage.kTime = (int)sliderTime.value;
		stage.kSpeed = (int)sliderSpeed.value;
		stage.startTime = sliderStartTime.value;
		stage.kBonus = sliderBonus.value;
		sliderSize.GetComponentInChildren<Text>().text = ((int)sliderSize.value).ToString();
		sliderTime.GetComponentInChildren<Text>().text = ((int)sliderTime.value).ToString();
		sliderSpeed.GetComponentInChildren<Text>().text = ((int)sliderSpeed.value).ToString();
		sliderStartTime.GetComponentInChildren<Text>().text = (sliderStartTime.value).ToString("N2");
		sliderBonus.GetComponentInChildren<Text>().text = (sliderBonus.value).ToString("N2");
	}
}
