using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Intro()
    {
        SceneManager.LoadScene("Intro");
    }
    public void Game()
    {
        SceneManager.LoadScene("Game");
    }
    public void Lose()
    {
        SceneManager.LoadScene("Lose");
    }
    public void Win()
    {
        SceneManager.LoadScene("Win");
    }
}
