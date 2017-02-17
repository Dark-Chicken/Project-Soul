using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    public bool isPaused;

    Rigidbody2D rbody;

    public float speed;

    public GameObject PauseMenu;

    public GameObject [] OpenWeapons;
    public GameObject [] handSword;
    public GameObject [] LhandSword;

    public int sword;
    public int FSword;
    public int Enemy;

    SpriteRenderer sprite;

    public void Start()
    {
        PlayerPrefs.SetInt("sword", 0);
        PlayerPrefs.SetInt("FSword", 0);
        PlayerPrefs.SetInt("Enemy", 0);
        sword = PlayerPrefs.GetInt("sword");
        FSword = PlayerPrefs.GetInt("FSword");
        rbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sword = PlayerPrefs.GetInt("sword");
        FSword = PlayerPrefs.GetInt("FSword");

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Time.timeScale = 0;
        }

        PauseMenu.SetActive(isPaused);

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        rbody.MovePosition(rbody.position + movement * speed * Time.deltaTime);

        if (sword == 1)
        {
            SwordSwitch(0);
            Debug.Log("Sword Got!");
        }
        else if (FSword == 1)
        {
            SwordSwitch(1);
            Debug.Log("Fire Sword Got!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.name == "Coconut Head")
            {
                PlayerPrefs.SetInt("Enemy", 1);
            }
            if (collision.gameObject.name == "Ollin Enemy")
            {
                PlayerPrefs.SetInt("Enemy", 2);
            }
            Debug.Log(PlayerPrefs.GetInt("Enemy"));
            SceneManager.LoadScene("battle");
        }
        if (collision.gameObject.tag == "Item")
        {
            if (collision.gameObject.name == "Long Sword")
            {
                OpenWeapons[0].SetActive(false);
                handSword[0].SetActive(true);
                PlayerPrefs.SetInt("sword", 1);
            }
            else if (collision.gameObject.name == "Fire Sword"){
                OpenWeapons[1].SetActive(false);
                handSword[1].SetActive(true);
                PlayerPrefs.SetInt("FSword", 1);
            }
        }
    }

    void SwordSwitch(int x)
    {
        if (sprite.flipX == true)
        {
            LhandSword[x].SetActive(true);
            handSword[x].SetActive(false);
        }
        else
        {
            LhandSword[x].SetActive(false);
            handSword[x].SetActive(true);
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(!isPaused);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Leave()
    {
        Application.Quit();
    }
}

