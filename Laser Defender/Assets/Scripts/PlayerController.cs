using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerLaser;
    public AudioClip fireSound;

    public float speed = 5.0f;
    private float xMin;
    private float xMax;
    private float padding = 0.5f;
    public float laserSpeed;
    public float firingRate = 0.2f;
    public float Health = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));

        xMin = leftmost.x + padding;
        xMax = rightmost.x - padding;
        
    }

    void Fire()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        GameObject beam = Instantiate(PlayerLaser, transform.position+offset , Quaternion.identity) as GameObject;
        beam.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }


        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right*speed* Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left*speed*Time.deltaTime;
        }
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player Collided with missile");
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            Health -= missile.getDamage();
            missile.Hit();
            if (Health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadLevel("Win");
        Destroy(gameObject);
    }
}
