using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{

    //やること
    //変数の作成（床の各パーツを格納）
    [SerializeField]
    private StackPartController[] stackPartControllers = null;



    //関数作成（各パーツの吹き飛ばし関数を呼ぶ、削除用のコルーチン）

    public void ShatterAllParts()
    {
        if(transform.parent != null)
        {
            transform.parent = null;
        }

        foreach (StackPartController o in stackPartControllers)
        {
            o.Shatter();
        }


        //関数を呼ぶ
        StartCoroutine("RemoveParts");
    }

    IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }



}
