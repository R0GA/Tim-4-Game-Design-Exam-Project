using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAni : MonoBehaviour
{
    public float delay;
    void Start()
    {
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
