using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeMana : MonoBehaviour
{
    public int amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            var HealthBar = other.GetComponent<HealthBar>();
            if (player != null)
            {
                HealthBar.RechargeMana(amount);
                Debug.Log("Player entered trap area");
            }
        }
    }
}
