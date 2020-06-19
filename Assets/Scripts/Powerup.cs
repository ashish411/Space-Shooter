using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    // Start is called before the first frame update
    [SerializeField]
    private int powerUpType;
    [SerializeField]
    private AudioClip _audioClip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            Player player = collision.transform.GetComponent<Player>();
             if (player != null)
            {
                player.enablePowerUp(powerUpType);
            }
            Destroy(this.gameObject);
        }
    }
}
