using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartGame;
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartGame.gameObject.SetActive(false);
    }


    public void UpdateScore(int points)
    {
        _scoreText.text = "Score: " + points;
    }

    public void UpdateLives(int lives)
    {
        _liveImage.sprite = _liveSprites[lives];
    }

    public void EnableGameOver()
    {
        StartCoroutine(FlickerGameOverTextRoutine());

        if(_gameManager != null)
        {
            _gameManager.GameOver();
        }
        _gameOverText.gameObject.SetActive(true);
        _restartGame.gameObject.SetActive(true);
    }

    IEnumerator FlickerGameOverTextRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(true);
        }
    }
}
