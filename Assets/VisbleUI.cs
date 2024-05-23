using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisbleUI : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        var player = GetComponent<PlayerController>();
        if (collision.CompareTag("Player") && player.DashActivated == true)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
