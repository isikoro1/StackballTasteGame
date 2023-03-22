using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    //やりたいこと
    //シングルトン化
    public static SoundManager instance;

    //変数の作成（AudioSouce）
    private AudioSource audioSource;

    //関数の作成（SE再生用）


    private void Awake()
    {
        Singleton();

        audioSource = GetComponent<AudioSource>();
    }

    void Singleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySE(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);　
    }
}
