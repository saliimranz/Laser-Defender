using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject EnemyLaser;
    public AudioClip enemyFire;
    public AudioClip deathSound;

    public float Health = 150;
    public float missileSpeed = 10;
    public float shotsPerSecound = 0.5f;
    public int scoreValue = 150;
    private ScoreKeeper scorekeeper;

    void Start()
    {
        scorekeeper =  GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
    private void Update()
    {
        float probability = Time.deltaTime * shotsPerSecound;
        if(Random.value < probability)
        {
            Fire();
        }
    }

    public void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject missile = Instantiate(EnemyLaser, startPosition, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -missileSpeed);
        AudioSource.PlayClipAtPoint(enemyFire, transform.position);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            Health -= missile.getDamage();
            missile.Hit();
            if(Health <= 0) {
                Die();
            }
        }
        Debug.Log("Hit by a Projectile");
    }

    void Die()
    {
        Destroy(gameObject);
        scorekeeper.Score(scoreValue);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
    }
 
}
