using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPlatform : MonoBehaviour
{
    public void ChangeLayer(){
        gameObject.layer = 15;
        StartCoroutine("ReturnLayer");
    }
    IEnumerator ReturnLayer(){
        yield return new WaitForSeconds(2f);
        gameObject.layer = 8;
    }
}
