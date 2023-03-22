using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPartController : MonoBehaviour
{
    //やりたいこと
    //変数の作成（剛体、メッシュレンダラー、こらいだー）

    private Rigidbody rb;
    private MeshRenderer meshRenderer;
    private Collider cod;


    //床のオブジェクト吹き飛ばし用関数

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        cod = GetComponent<Collider>();

    }

    public void Shatter()
    {
        rb.isKinematic = false;
        cod.enabled = false;

        Vector3 forcePoint = transform.parent.position;
        float parentX = transform.parent.position.x;
        float x = meshRenderer.bounds.center.x;

        Vector3 subDir = (parentX - x < 0) ? Vector3.right : Vector3.left;

        Vector3 dir = (Vector3.up * 1.5f + subDir).normalized;

        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        rb.AddForceAtPosition(force * dir, forcePoint, ForceMode.Impulse);

        rb.AddTorque(Vector3.left * torque);

        rb.velocity = Vector3.down;
    }

}
