 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector3 camFollow;
    private Transform player, goal;

    [SerializeField]
    private float camAdiustedValu = 6.5f;

    //プレイヤーを探して取得

    //関数の作成（ゴールをオブジェクトを探す）

    //Updateで位置調整コード記述

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }



    // Start is called before the first frame update
    void Start()
    {
        FindGoal();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > player.transform.position.y && transform.position.y > goal.position.y + camAdiustedValu)
        {
            camFollow = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, camFollow.y, transform.position.z);
    }

    void FindGoal()
    {
        while (true)
        {
            if(goal == null)
            {
                goal = GameObject.Find("Goal(Clone)").GetComponent<Transform>();
            }

            if(goal != null)
            {
                break;
            }

        }
    }
}
