using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMItemPickup : MonoBehaviour
{
    public enum ItemType

    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,

    }

    public ItemType type;

    private void OnBMItemPickup(GameObject player)

    {

        switch (type)
        {

            case ItemType.ExtraBomb:
                player.GetComponent<BMBombController>().AddBomb();
                break;

            case ItemType.BlastRadius:
                player.GetComponent<BMBombController>().BMExplosionRadius++;
                break;


            case ItemType.SpeedIncrease:
                player.GetComponent<BMMovementController>().speed++;
                break;


        }

        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnBMItemPickup(other.gameObject);

        }
    }

}



