using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField]
    private MiniGame miniGame;
    public MiniGame MiniGame => miniGame;
    [SerializeField]
    private float scoreMultiplier;
    public float ScoreMultiplier => scoreMultiplier;
}
