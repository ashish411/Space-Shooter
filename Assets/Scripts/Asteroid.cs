using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private GameObject _explostionPrefab;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Laser")
        {
            Instantiate(_explostionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            _spawnManager.startSpawning();
            Destroy(this.gameObject,0.8f);
        }
    }
}
