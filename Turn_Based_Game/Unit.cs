using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public string unitName;

	//HP,MP관련
	public int maxHP;
	public int currentHP;
	public int maxMP;
	public int currentMP;

	//공격, 방어 관련
	public int damage;
	public int Mdamage;
	public int defense;

	//레벨 관련
	public int currentLevel;
	public int nextLevel;


	public bool TakeDamage(int dmg, int def) {
		currentHP = currentHP - dmg + def;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public bool TakeMDamage(int dmg, int def) {
		currentHP = currentHP - dmg + def;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}
	public void AfterMAttack() {
		currentMP = currentMP - 5;
	}

	public void Heal(int amount) {
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

}
