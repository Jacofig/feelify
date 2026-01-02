using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public AudioClip sceneMusic; // przeci¹gnij tu nowy utwór w Inspectorze

    void Start()
    {
        if (AudioManager.instance != null && sceneMusic != null)
        {
            AudioManager.instance.ChangeMusic(sceneMusic);
        }
    }
}
