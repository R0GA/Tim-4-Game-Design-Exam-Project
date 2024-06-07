using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTest : MonoBehaviour
{
    [SerializeField]
    private Vector2Int firstRange, secondRange;

    [SerializeField]
    private int valueToRemap = 50;
    void Start()
    {
        for (int i = 0; i <= firstRange.y; i++)
        {
            var output = Utility.Remap(i, firstRange.x, firstRange.y, secondRange.x, secondRange.y);
            Debug.Log($"{i} -- {output}");  
        }
    }
}
