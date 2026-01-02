using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroInput : MonoBehaviour
{
    public TypewriterText typewriter;
    public GameObject nextPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            if (typewriter.IsTyping())
            {
                typewriter.SkipTyping();
            }
            else
            {
                if (nextPanel != null)
                {
                    nextPanel.SetActive(true);
                    gameObject.SetActive(false);
                }
                else
                {
                    PlayerPrefs.DeleteAll();
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("OverworldScene");
                }
            }
        }
    }
}
