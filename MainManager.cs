using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance; // Declares an instance of the main manager, which is static.
    public Color TeamColor; 

    // Sets instance equal to this current class and prevents the game manager object from being destroyed on load.
    private void Awake()
    {
        // Destroys extra game managers
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }

    [System.Serializable] // This line is required to use JsonUtility 
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        // Creates new instance of save data and filled its team color member with the TeamColor variable
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        // Transforms the instance above to JSON with JsonUtility
        string json = JsonUtility.ToJson(data);

        /* Writes a string to a file by giving you a folder first with persitentDataPath,
          where the save data persists each time the application is closed, updated, or reinstalled. */
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor() 
    {
        // Checks if the save file is present, if not, nothing needs to be saved.
        string path = Application.persistentDataPath + "/savefile.json";

        /* If the save files exists, read its content with File.ReadAllText(path), then give that text to
           JsonUtility to transform it back to the SaveData instance. Finally, set the color to the saved color. */
        if (File.Exists(path)) 
        { 
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor  = data.TeamColor;
        }
    }
}
