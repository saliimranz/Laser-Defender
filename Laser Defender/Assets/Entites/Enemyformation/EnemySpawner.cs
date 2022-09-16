using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width = 1.0f;
    public float height = 1.0f;
    private bool movingRight = true;
    public float speed = 5f;
    public float spawnDelay = 0.5f;
    public int childCount;
    float xMin;
    float xMax;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMin = leftEdge.x;
        xMax = rightEdge.x;


        SpawnUntilFull();
    }

    void SpawnEnemy()
    {
        /*  foreach (Transform child in transform)
          {
              GameObject enemy = Instantiate(enemyPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
              enemy.transform.parent = transform;
          } */
        for (i = 0; i < transform.childCount; i++)
        {
            //GameObject enemy = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            GameObject enemy = Instantiate(enemyPrefab, transform.GetChild(i).transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = transform.GetChild(i);
        }

    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition) {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition()) {
            Invoke("SpawnUntilFull", spawnDelay);
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if (leftEdgeOfFormation < xMin)
        {
            movingRight = !movingRight;
        }else if(rightEdgeOfFormation > xMax)
        {
            movingRight = false;
        }

        if (AllMembersDead())
        {
            //Debug.Log("Enemy formation is empty");
            SpawnUntilFull();
        }


    }
    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;

    }

    bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
}
