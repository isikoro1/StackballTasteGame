using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private bool touch;
　  [SerializeField]
    private float maxSpeed;
    private float currentTime;
    private bool invincible;  
    [SerializeField]
    private GameObject fireEffect;
    [SerializeField]
    private GameObject goalEffect;
    [SerializeField]
    private GameObject splashEffect;
    [SerializeField]
    private AudioClip bounceClip, explodsionClip, goalClip, shatterClip;

    
    //変数の作成（UI用変数格納）
    [SerializeField]
    private GameObject invincibleObj;

    [SerializeField]
    private Image image;

    //InvincibleCheckにコード記述



    public enum PlayerState
    {
        Prepre,
        Playing,
        Died,
        Finish
    }

    [HideInInspector]
    public PlayerState playerState = PlayerState.Prepre;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //currentTime = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        TouchCheck();
        SpeedCheck();

        InvincibleCheck();

        FinishGameCheck();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!touch)
        {
            rb.velocity = new Vector3(0, 5, 0);

            //スプラッシュ生成関数を呼び出す

            if(collision.gameObject.tag != "Finish")
            {
                SplashEffect(collision);　
            }

            SoundManager.instance.PlaySE(bounceClip);

        }
        else
        {
            if (invincible)
            {
                if (collision.gameObject.tag == "Normal" || collision.gameObject.tag == "Enemy")
                {
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<StackController>().ShatterAllParts();

                    SoundManager.instance.PlaySE(shatterClip);
                }
            }
            else
            {
                if (collision.gameObject.tag == "Normal")
                {
                    //Destroy(collision.transform.parent.gameObject);

                    collision.transform.parent.GetComponent<StackController>().ShatterAllParts();
                    SoundManager.instance.PlaySE(shatterClip);
                }
                else if (collision.gameObject.tag == "Enemy")
                {
                    Debug.Log("GameOver");

                    playerState = PlayerState.Died;
                    rb.isKinematic = true;
                    gameObject.SetActive(false);

                    SoundManager.instance.PlaySE(explodsionClip);
                }
            }
                             
        }

        if (collision.gameObject.tag == "Finish" && playerState == PlayerState.Playing)
        {
            playerState = PlayerState.Finish;
            GoalEffect();

            SoundManager.instance.PlaySE(goalClip);
        }

         
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!touch)
        {
            rb.velocity = new Vector3(0, 5, 0);
        }
    }

    /// <summary>
    /// マウスが押されているときにプレイヤーを下に動かす
    /// </summary>
    void TouchCheck()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            if (playerState == PlayerState.Prepre)
            {
                playerState = PlayerState.Playing;
            }

            touch = true;
            rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
        }

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
        {
            touch = false;

        }
    }


    /// <summary>
    /// 移動速度確認して調整
    /// </summary>
    void SpeedCheck()
    {
        if (rb.velocity.y > maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxSpeed, rb.velocity.z);
        }
    }

    /// <summary>
    /// 無敵判定と時間の加算減算を行う
    /// </summary>
    void InvincibleCheck()
    {
        if (invincible)
        {
            currentTime -= Time.deltaTime * 0.35f;

            if (!fireEffect.activeInHierarchy)
            {
                fireEffect.SetActive(true);
            }
        }
        else
        {
            if (fireEffect.activeInHierarchy)
            {
                fireEffect.SetActive(false);
            }


            if (touch)
            {
                currentTime += Time.deltaTime * 0.8f;
            }
            else
            {
                currentTime -= Time.deltaTime * 0.1f;
            }
        }

        if (currentTime >= 0.15 || image.color == Color.red)
        {
            invincibleObj.SetActive(true);
        }
        else
        {
            invincibleObj.SetActive(false);
        }

        if(currentTime >= 1)
        {
            currentTime = 1;
            invincible = true;

            image.color = Color.red;
        }
        else if (currentTime <= 0)
        {
            currentTime = 0;
            invincible = false;

            image.color = Color.white;
        }

        if (invincibleObj.activeInHierarchy)
        {
            image.fillAmount = currentTime / 1f;
        }

    }

    void FinishGameCheck()
    {
        if(playerState == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawns>().NextLevel();
            }
        }
    }

    void GoalEffect()
    {
        GameObject goal = Instantiate(goalEffect);
        goal.transform.SetParent(Camera.main.transform);

        goal.transform.localPosition = Vector3.up * 1.5f;
        goal.transform.eulerAngles = Vector3.zero;
    }


    void SplashEffect(Collision target)
    {
        GameObject splash = Instantiate(splashEffect);
        splash.transform.SetParent(target.transform);

        splash.transform.localEulerAngles = new Vector3(90, Random.Range(0, 359), 0);
        float randomScale = Random.Range(0.0038f, 0.0055f);

        splash.transform.localScale = new Vector3(randomScale, randomScale, 1);

        splash.transform.position = new Vector3(transform.position.x, transform.position.y - 0.22f, transform.position.z);

    }
}
