using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//배틀을 나타내는 상태
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, ESCAPE,  FAIL} 

public class BattleSystem : MonoBehaviour {
	// 주인공과 적 가져오기
	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	Unit playerUnit;
	Unit enemyUnit;
	// 대화창
	public Text dialogueText;
	//UI 표시용
	public BattleHud playerHUD;
	public BattleHud enemyHUD;
	// 도망치는 확률 변수
	public int eValue;

	public BattleState state;


	void Start() {
		state = BattleState.START;    //첫 시작은 스타트
		StartCoroutine(SetupBattle());
	}

	IEnumerator SetupBattle() {
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = "앗 야생의" + enemyUnit.unitName + "(이)가 나타났다...!";
		//각 UI초기화
		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		eValue = 95;  // 도망갈 수 있는 확률
		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}


	void PlayerTurn() {
		dialogueText.text = "무얼 해야할까 : ";
	}

	IEnumerator PlayerAttack() {
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "공격은 성공적이다!";

		yield return new WaitForSeconds(2f);

		if (isDead) {
			//적이 죽는다면 배틀 상태 변화
			state = BattleState.WON;
			EndBattle();
		} else {
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator PlayerHeal() {
		playerUnit.Heal(5);

		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "힐을 사용했다!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}
	IEnumerator PlayerEscape() {
		dialogueText.text = "도망치기!";

		yield return new WaitForSeconds(2f);

		if(Random.Range(1, 100) < eValue) {   // 현재 90퍼센트의 확률로 도망 못감
			state = BattleState.ESCAPE;
			EndBattle();
		} else {
			dialogueText.text = "도망 못갔쥬?";
			yield return new WaitForSeconds(2f);
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	public void OnAttackButton() {
		// 플레이어턴이 아니라면 반환 X
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}

	public void OnHealButton() {
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}

	public void OnEscapeButton() {
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerEscape());
	}



	// 적과 관련된 함수
	IEnumerator EnemyTurn() {
		dialogueText.text = enemyUnit.unitName + "의 공격 !";

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if (isDead) {
			state = BattleState.LOST;
			EndBattle();
		} else {
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}


	//배틀 끝
	void EndBattle() {
		if (state == BattleState.WON) {
			dialogueText.text = "배틀에서 이겼다!";
		} else if (state == BattleState.LOST) {
			dialogueText.text = "배틀에서 패배했다.";
		} else if (state == BattleState.ESCAPE) {
			dialogueText.text = "성공적으로 도망쳤다.";
		}
		Invoke("EndScene", 2);
	}

	void EndScene() {
		SceneManager.LoadScene("ShadowIsland");
		// 해야할 것
		// 이전 씬에서의 위치를 기억해 그 위치로 보내주기
	}

	

}
