using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RPSGameManager : MonoBehaviour
{

    public Button shootButton;
    public Button playButton;

    private void Start()
    {
        if (shootButton != null)
        {
            shootButton.interactable = true;
        }

        if (playButton != null)
        {
            playButton.gameObject.SetActive(false);
        }
    }

    public void PlayGame()
    {
        if (shootButton != null)
        {
            shootButton.interactable = false;
        }

        if (playButton != null)
        {
            playButton.gameObject.SetActive(true);
        }

        StartCoroutine(LoadSceneAfterDelay(4f));
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Loading RPS Gameplay scene...");
        SceneManager.LoadScene("RPS Gameplay");
    }
}
