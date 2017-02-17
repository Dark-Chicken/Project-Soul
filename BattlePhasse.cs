using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

public class BattlePhasse : MonoBehaviour {

    bool PlayerTurn = true;

    public GameObject Enemy1;
    public GameObject Enemy2;

    public int minDMG;
    public int maxDMG;
    public int enmDMG;
    public int minEnmSMG;

    public Text PlayerHPtext;
    public Text EnemyHPtext;
    public Text Status;

    public int PlayerHP = 100;
    public int EnemyHP = 100;

    public int sword;
    public int FSword;
    public int Enemies;

    public string EnemyName;

    private void Start()
    {

        sword = PlayerPrefs.GetInt("sword");
        FSword = PlayerPrefs.GetInt("FSword");
        Enemies = PlayerPrefs.GetInt("Enemy");

        if (sword == 1)
        {
            minDMG = 15;
            maxDMG = 30;
        }
        else if (FSword == 1)
        {
            minDMG = 25;
            maxDMG = 40;
        }
        else
        {
            minDMG = 10;
            maxDMG = 25;
        }
        Debug.Log(minDMG + ", " + maxDMG);

        if (Enemies == 1)
        {
            EnemyHP = 100;
            minEnmSMG = 15;
            enmDMG = 30;
            EnemyName = "Coconut Head";
            Enemy1.SetActive(true);
            EnemyHPtext.text = EnemyName + " | HP:" + EnemyHP;
        }
        else if (Enemies == 2)
        {
            EnemyHP = 150;
            minEnmSMG = 20;
            enmDMG = 35;
            EnemyName = "Ollin the Wizard";
            Enemy2.SetActive(true);
            EnemyHPtext.text = EnemyName + " | HP:" + EnemyHP;
        }
    }

    public void Player1()
    {
        if (PlayerHP > 0 && EnemyHP > 0)
        {
            if (PlayerTurn == true)
            {
                EnemyHP -= Random.Range(minDMG, maxDMG);
                if (EnemyHP <= 0)
                {
                    EnemyHP = 0;
                    Status.text = "You have bestowed the beast!!!";
                    StartCoroutine(Load());
                }
                EnemyHPtext.text = EnemyName + " | HP:" + EnemyHP.ToString();
                PlayerTurn = false;
            }
        }
    }

    private void Update()
    {

        if (PlayerHP > 0 && EnemyHP > 0)
        {
            if (PlayerTurn == false)
            {
                Status.text = "Oppnents turn, please wait...";
                StartCoroutine(Enemy());
            }
            else
            {
                StopAllCoroutines();
                Status.text = "Your turn, choose wisely...";
            }
        }
    }

    public void exit()
    {
        float chance = Random.Range(1, 4);
        if(chance == 1)
        {
            SceneManager.LoadScene("main");
        }
        else
        {
            Status.text = "You failed to run...";
            StartCoroutine(Load());
            EnemyTurn();
        }

    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("main");
    }

    IEnumerator Enemy()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 5));
        EnemyTurn();

    }

    void EnemyTurn()
    {
        PlayerHP -= Random.Range(minEnmSMG, enmDMG);
        if (PlayerHP <= 0)
        {
            PlayerHP = 0;
            Status.text = "The beast is to strong, you flee.";
            StartCoroutine(Load());
        }
        PlayerHPtext.text = "Ken | HP:" + PlayerHP.ToString();
        PlayerTurn = true;
    }
}
