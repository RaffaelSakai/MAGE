using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string firstlevel;

    [SerializeField]
    string HubLevel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGame()
    {
        SceneManager.LoadScene(firstlevel);
    }


    public void Continue()
    {
        SceneManager.LoadScene(HubLevel);
    }


    public void Quit()
    {
        Application.Quit();

    }

}
