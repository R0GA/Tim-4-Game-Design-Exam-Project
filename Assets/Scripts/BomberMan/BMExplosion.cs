using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BMExplosion : MonoBehaviour
{
    public BMAnimatedSpriteRenderer start;
    public BMAnimatedSpriteRenderer middle;
    public BMAnimatedSpriteRenderer end;


    public void SetActiveRenderer(BMAnimatedSpriteRenderer renderer)
    {
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;

    }

    public void SetDirection(Vector2 direction)
    {
         float angle=Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);

    }


}


