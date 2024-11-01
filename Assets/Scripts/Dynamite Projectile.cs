using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteAbility : MonoBehaviour
{
    public GameObject dynamitePrefab; // Dynamite projectile prefab
    public Transform handPosition; // The position where the dynamite is thrown from
    public float throwSpeed = 10f; // Speed of the throw
    public float maxDistance = 15f; // Max distance the dynamite can travel
    public GameObject firePrefab; // Fire effect prefab for the impact point
    public float stunDuration = 2f; // Duration of enemy stun
    public float fireDuration = 3f; // Duration of fire after impact

    private Vector3 initialPosition; // To store the initial position of the dynamite
    private bool returning = false; // Check if dynamite is returning
    private GameObject dynamite; // Dynamite object reference

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dynamite == null) // Throw only if the dynamite is not already thrown
            {
                ThrowDynamite();
            }
        }

        if (dynamite != null)
        {
            MoveDynamiteAndPlayer();
        }
    }

    void ThrowDynamite()
    {
        dynamite = Instantiate(dynamitePrefab, handPosition.position, Quaternion.identity);
        initialPosition = dynamite.transform.position;
        returning = false;
    }

    void MoveDynamiteAndPlayer()
    {
        if (!returning)
        {
            // Move dynamite forward
            dynamite.transform.Translate(Vector3.forward * throwSpeed * Time.deltaTime);

            // If it reaches the max distance, start returning
            if (Vector3.Distance(initialPosition, dynamite.transform.position) >= maxDistance)
            {
                returning = true;
            }
        }
        else
        {
            // Move dynamite back toward the player
            Vector3 direction = (handPosition.position - dynamite.transform.position).normalized;
            dynamite.transform.Translate(direction * throwSpeed * Time.deltaTime);

            // Destroy the dynamite when it reaches the player
            if (Vector3.Distance(handPosition.position, dynamite.transform.position) < 1f)
            {
                Destroy(dynamite);
            }
        }
    }

    // Handling collisions with enemies
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StunEnemy(other.gameObject); // Stun the enemy on contact
            SpawnFire(other.transform.position); // Spawn fire at the impact point
            Destroy(dynamite); // Destroy the dynamite on impact
        }
    }

    void StunEnemy(GameObject enemy)
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.Stun(stunDuration);
        }
    }

    void SpawnFire(Vector3 position)
    {
        GameObject fire = Instantiate(firePrefab, position, Quaternion.identity);
        Destroy(fire, fireDuration); // Destroy the fire after the duration
    }
}





