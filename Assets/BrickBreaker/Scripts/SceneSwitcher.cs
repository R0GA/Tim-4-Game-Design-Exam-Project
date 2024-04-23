using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    PlayerManager playerManager = PlayerManager.Instance;

    public float switchDelay = 60f; // Time in seconds before switching scenes

    private void Start()
    {
        // Start the coroutine to switch scenes after the specified delay
        StartCoroutine(SwitchSceneAfterDelay());
    }

    private IEnumerator SwitchSceneAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(switchDelay);

        // Switch back to the main scene
        playerManager.ExitMiniGame(1);
    }
}