using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void play()
    {
        SceneManager.LoadScene("main");
    }

    public void leave()
    {
        Application.Quit();
    }
}
