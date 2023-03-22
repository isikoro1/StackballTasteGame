using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    //やりたいこと
    //ライブラリ追加（2種類）
    //変数の作成（UIオブジェクト2種類、テキスト、プレイヤー）
    [SerializeField]
    private GameObject clearUI, gameOverUI;

    [SerializeField]
    private Text clearLevelText;

    private Player player;

    //PlayerStateに合わせて表示するUIを設定する


    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerState == Player.PlayerState.Finish)
        {
            if (!clearUI.activeInHierarchy)
            {
                clearLevelText.text = "Level" + FindObjectOfType<LevelSpawns>().level;
            }

            clearUI.SetActive(true);
            gameOverUI.SetActive(false);
        }

        if (player.playerState == Player.PlayerState.Died)
        {
            
            clearUI.SetActive(false);
            gameOverUI.SetActive(true);

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
