using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && _isGameOver == true)
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        Debug.LogError("asdmp ad");
        _isGameOver = true;
    }
}
