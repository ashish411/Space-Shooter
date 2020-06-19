using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private bool _isEnemyLaser = false;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == true)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y > 7f)
        {
            Destroy(gameObject);
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }

    public void AddEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _isEnemyLaser==true)
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                player.damage();
            }
        }
    }
}
