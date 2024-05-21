using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMDestructable : MonoBehaviour
{
    public float destructionTime = 1f;
    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItem;

    private void Start()
    {
        Destroy(gameObject, destructionTime);   
    }

    private void OnDestroy()
    {
        if (spawnableItem.Length> 0 && Random.value < itemSpawnChance)

        {
            int randomIndex = Random.Range(0, spawnableItem.Length);
            Instantiate(spawnableItem[randomIndex], transform.position, Quaternion.identity);

        }
    }

}
