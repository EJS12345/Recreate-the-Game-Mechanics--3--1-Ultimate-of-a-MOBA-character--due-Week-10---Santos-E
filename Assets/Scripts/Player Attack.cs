using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject dynamitePrefab;
    public Transform throwPoint; // A point in front of the player for the dynamite to spawn from

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowDynamite();
        }
    }

    void ThrowDynamite()
    {
        Instantiate(dynamitePrefab, throwPoint.position, throwPoint.rotation);
    }
}

