using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCollector : MonoBehaviour
{
    private int coins = 0;

    [SerializeField]
    private AudioSource coinPickup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            coinPickup.Play();
            Destroy(collision.gameObject);
            coins++;
            if(coins == 3)
            {
                Application.Quit();
            }
        }
    }
}
