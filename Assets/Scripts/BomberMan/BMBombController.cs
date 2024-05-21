using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BMBombController : MonoBehaviour

{
    [Header ("Bomb")]

    public GameObject bombPrefab;

    public KeyCode inputKey = KeyCode.Space;

    public float bombFuseTime = 3f;

    public int bombAmount = 1;

    private int bombsRemaining;


    [Header("BMExplosion")]

    public BMExplosion BMExplosionPrefab;

    public float BMExplosionDuration = 1f;

    public int BMExplosionRadius = 1;

    public LayerMask BMExplosionLayerMask;

    [Header("BMDestructable")]

    public Tilemap BMDestructableTiles;
    public BMDestructable BMDestructablePrefab;

  

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }
    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) {
            StartCoroutine(PlaceBomb());
        
        }
    }

    private  IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x =Mathf.Round(position.x);
        position.y =Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;


        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        BMExplosion BMExplosion = Instantiate(BMExplosionPrefab, position, Quaternion.identity);
        BMExplosion.SetActiveRenderer(BMExplosion.start);
        BMExplosion.DestroyAfter(BMExplosionDuration);

        Explode(position, Vector2.up, BMExplosionRadius);
        Explode(position, Vector2.down, BMExplosionRadius);
        Explode(position, Vector2.left , BMExplosionRadius);
        Explode(position, Vector2.right, BMExplosionRadius);




        Destroy(bomb);
        bombsRemaining++;
    }


    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, BMExplosionLayerMask))

        {
            ClearBMDestructable(position);
            return;  
        }

        BMExplosion BMExplosion= Instantiate(BMExplosionPrefab, position, Quaternion.identity);
        BMExplosion.SetActiveRenderer(length > 1? BMExplosion.middle: BMExplosion.end);
        BMExplosion.SetDirection(direction);
        BMExplosion.DestroyAfter (BMExplosionDuration);

        Explode(position, direction, length-1);


    }

    private void ClearBMDestructable(Vector2 position)
    { 
      Vector3Int cell= BMDestructableTiles.WorldToCell(position);
        TileBase tile = BMDestructableTiles.GetTile(cell);

        if (tile != null) 
        { 
            Instantiate(BMDestructablePrefab, position, Quaternion.identity);
            BMDestructableTiles.SetTile(cell, null);
        
        }

    }



    private void OnTriggerExit2D(Collider2D other)

    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;   

    }

}
