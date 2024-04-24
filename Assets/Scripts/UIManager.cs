using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    PlayerManager playerManager = PlayerManager.Instance;
    GameObject player1TextObject;
    GameObject player2TextObject;
    private Text player1Text;
    private Text player2Text;
    // Start is called before the first frame update
    void Start()
    {

        player1TextObject = GameObject.FindWithTag("P1TXT");
        player1Text = player1TextObject.GetComponent<Text>();
        player2TextObject = GameObject.FindWithTag("P2TXT");
        player2Text = player2TextObject.GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {
        
        player1Text.text = playerManager.player1Score.ToString();
        player2Text.text = playerManager.player2Score.ToString();

    }
}
