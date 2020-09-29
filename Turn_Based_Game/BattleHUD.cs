using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour {
	// UI를 위해 가져오는 것
	public Text nameText;
	public Slider hpSlider;
	public Slider mpSlider;

	public void SetHUD(Unit unit) {
		nameText.text = unit.unitName;

		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;

		mpSlider.maxValue = unit.maxMP;
		mpSlider.value = unit.currentMP;
	}

	public void SetHP(int hp) {
		hpSlider.value = hp;
	}

	public void SetMP(int mp) {
		mpSlider.value = mp;
	}

}
