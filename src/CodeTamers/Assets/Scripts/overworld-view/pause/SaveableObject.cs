using UnityEngine;

public class SaveableObject : MonoBehaviour
{
    [Tooltip("Musi byæ unikalne w ca³ej grze")]
    public string uniqueID;

    void Reset()
    {
        // automatyczne ID przy dodaniu komponentu
        uniqueID = System.Guid.NewGuid().ToString();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(uniqueID + "_x", transform.position.x);
        PlayerPrefs.SetFloat(uniqueID + "_y", transform.position.y);
        PlayerPrefs.SetFloat(uniqueID + "_z", transform.position.z);
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey(uniqueID + "_x")) return;

        float x = PlayerPrefs.GetFloat(uniqueID + "_x");
        float y = PlayerPrefs.GetFloat(uniqueID + "_y");
        float z = PlayerPrefs.GetFloat(uniqueID + "_z");

        transform.position = new Vector3(x, y, z);
    }
}
