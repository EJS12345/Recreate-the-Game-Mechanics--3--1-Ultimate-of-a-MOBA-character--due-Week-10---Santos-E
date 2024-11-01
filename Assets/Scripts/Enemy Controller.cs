using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isStunned = false;
    private float stunTimer = 0f;

    void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                isStunned = false;
            }
        }
        else
        {
            // Enemy default behavior here (like moving toward player)
        }
    }

    public void Stun(float duration)
    {
        isStunned = true;
        stunTimer = duration;
    }
}

