using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slotItemExample : MonoBehaviour
{
    public float buffTime = 10f;    //버프가 지속되는 시간
    float curTime;


    public Image icon;
    void Start(){

    }
    void Awake(){
        icon = GetComponent<Image>();
        icon.fillAmount = 1;
        curTime = buffTime;
    }
    void Update()
    {
        if(Input.inputString == (transform.parent.GetComponent<Slot>().num + 1).ToString()){
            // use item
            Debug.Log("You used item");
            playerAttack.coolTime = playerAttack.coolTime/2.0f;
            StartCoroutine(BuffSystem());
        }
    }
    WaitForSeconds waitTime = new WaitForSeconds(0.1f);
    IEnumerator BuffSystem() {
        while (curTime > 0) {
            curTime -= 0.1f;
            icon.fillAmount = curTime/buffTime;
            yield return waitTime;
        }
        Debug.Log("버프 끝");
        playerAttack.coolTime = playerAttack.coolTime*2.0f;
        Destroy(this.gameObject);    
        
    }

    

}
