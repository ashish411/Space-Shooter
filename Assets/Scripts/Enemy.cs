using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private GameObject _enemyLaser;

    private Animator _anim;
    private Player _player;

    private float _canFire = -1;
    private float _fireRate = 3.0f;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player != null)
        {
            _anim = GetComponent<Animator>();
        }
        transform.position = new Vector3(0, 6f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser =  Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AddEnemyLaser();
            }
        }

        
    }

    private void calculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7f)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f), transform.position.y * -1, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit: " + other.transform.name);
        if(other.gameObject.tag == "Player")
        {
            if(_player != null) {
                _player.damage();
            }
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.5f);
        }
        else if(other.gameObject.tag ==  "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
                _player.AddScore(10);
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.5f);
        }
    }
}
