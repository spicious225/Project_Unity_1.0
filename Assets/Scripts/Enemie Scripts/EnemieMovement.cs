using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    public float movespeed;
    public float despawnDistance = 25f;
    private float speedMultiplier = 1f;
    private KillCounter killCounter;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        killCounter = FindObjectOfType<KillCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movespeed * speedMultiplier * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    void ReturnEnemy()
    {
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
        movespeed *= speedMultiplier; // Apply the multiplier to the move speed
    }

    void Die()
    {
        // Handle the enemy's death here (like playing an animation)
        Debug.Log("Enemy has died!");

        KillCounter.Instance.AddKill();

        Destroy(gameObject); // Destroy the enemy
    }

}
