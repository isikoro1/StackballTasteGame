using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rortator : MonoBehaviour
{

    //今回やりたいこと
    //変数の作成（回転スピード）
    [SerializeField]
    private float speed = 100;

    //Updateで回転するコード記述


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
