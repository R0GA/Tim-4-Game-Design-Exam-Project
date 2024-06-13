using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerStats
{
   [Range(1,1)]
   public int maxHealth;
   [HideInInspector]
   public int currentHealth;
   [HideInInspector]
   public int maxLives = 1;
   [HideInInspector]
   public int currentLives;

   public float playerSpeed;
   public float fireRate;
}
