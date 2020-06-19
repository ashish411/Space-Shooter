using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _rightEngineDestroy;
    [SerializeField] 
    private GameObject _leftEngineDestroy;

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserSoundClip;

    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;


    private bool _isTripleShotsEnable = false;
    private bool _isSpeedPowerEnable = false;
    private bool _isShieldPowerEnable = false;

    [SerializeField]
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if(_audioSource == null)
        {
            Debug.LogError("Audio source is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }

        if(_uiManager == null)
        {
            Debug.LogError("_Ui manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {

        _canFire = Time.time + _fireRate;
        if (_isTripleShotsEnable == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _audioSource.Play();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.9f, 0), 0);


        if (transform.position.x > 10 || transform.position.x < -10)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
        }
    }

    public void damage()
    {
        if (_isShieldPowerEnable == true)
        {
            _shieldVisualizer.SetActive(false);
            _isShieldPowerEnable = false;
            return;
        }
        _lives--;
        _uiManager.UpdateLives(_lives);
        switch (_lives)
        {
            case 2: _rightEngineDestroy.SetActive(true);
                break;
            case 1: _leftEngineDestroy.SetActive(true);
                break;
            case 0:
                _uiManager.EnableGameOver();
                Destroy(this.gameObject);
                if (_spawnManager != null)
                {
                    _spawnManager.onPlayerDeath();
                }
                else
                {
                    Debug.Log("Spawn manager is null");
                }
                break;
            default: Debug.Log("default case reached  lives");
                break;
        }
    }


    public void enablePowerUp(int powerUpType)
    {

        switch (powerUpType)
        {
            case 0:
                {
                    _isTripleShotsEnable = true;
                    StartCoroutine(TripleShotPowerDownRoutine());
                    break;
                }
            case 1:
                {
                    _isSpeedPowerEnable = true;
                    _speed = 7f;
                    StartCoroutine(SpeedPowerDownRoutine());
                    break;
                }
            case 2:
                {
                    _isShieldPowerEnable = true;
                    _shieldVisualizer.SetActive(true);
                    break;
                }
            default:
                {
                    Debug.Log("default case");
                    break;
                }
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }


    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(10f);
        _isTripleShotsEnable = false;

    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(10f);
        _isSpeedPowerEnable = false;
        _speed = 3.5f;
    }
}
