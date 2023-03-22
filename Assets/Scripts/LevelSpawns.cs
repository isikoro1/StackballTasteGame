using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSpawns : MonoBehaviour
{


    [SerializeField]
    private GameObject[] model;

    [SerializeField]
    private GameObject goal;

    private GameObject temp1, temp2;

    public int level = 1, addOn = 7;
    float i = 0;



    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        level = PlayerPrefs.GetInt("Level");


        if (level > 9)
        {
            addOn = 0;
        }


        for (i = 0; i > -level - addOn; i -= 0.5f)
        {

            if (level <= 15)
            {
                temp1 = Instantiate(model[Random.Range(0, 2)]);
            }

            if (level > 15 && level <= 35)
            {
                temp1 = Instantiate(model[Random.Range(0, 3)]);
            }

            if (level > 35 && level <= 100)
            {
                temp1 = Instantiate(model[Random.Range(0, 4)]);
            }

            if (level > 100)
            {
                temp1 = Instantiate(model[Random.Range(1, 4)]);
            }

            temp1.transform.position = new Vector3(0, i - 0.01f, 0);
            temp1.transform.eulerAngles = new Vector3(0, i * Random.Range( 6, 15), 0);


            temp1.transform.parent = FindObjectOfType<Rortator>().transform;
        }

        temp2 = Instantiate(goal);
        temp2.transform.position = new Vector3(0, i - 5f, 0);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }

}