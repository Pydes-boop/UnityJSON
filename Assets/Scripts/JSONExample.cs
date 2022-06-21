using System;
using System.IO;
using UnityEngine;

namespace JSON
{
    [CreateAssetMenu(fileName = "Example", menuName = "JSON")]
    public class JSONExample : ScriptableObject
    {
        public JSONdata _data;
        
        [Tooltip("Click to load Sprite and EntityID")]
        public bool loadEntityData = false;
    
        [Tooltip("Click to save EntityData to JSON")]
        public bool saveEntityData = false;
        
        private void saveEntityDataToJson()
        {
            string json = JsonUtility.ToJson(_data);
            string path = Application.dataPath + "/Resources/Files/" + _data.name + ".json";
            System.IO.File.WriteAllText(path, json);
            Debug.Log("Saved Entity Data of " + _data.name + " to " + path);
        }

        private void loadEntityDataFromJson()
        {
            string path = Application.dataPath + "/Resources/Files/" + _data.name + ".json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _data = JsonUtility.FromJson<JSONdata>(json);
                Debug.Log("Loaded Entity Data from " + path);
            }
            else
            {
                Debug.LogError("Loading Entity Data for " + _data.name + " not possible. Check if " + path + "exists.");
            }
        }
        
        public void OnValidate()
        {
            if (loadEntityData)
            {
                loadEntityData = false;
                loadEntityDataFromJson();
            }

            if (saveEntityData)
            {
                saveEntityData = false;
                saveEntityDataToJson();
            }
        }
        
    }

    [System.Serializable]
    public class JSONdata
    {
        public String name;
        public String url;
        public String description;
        public JSONComponent data;
    }
    
    [System.Serializable]
    public class JSONComponent
    {
        public int[] numbers1;
        public int[] numbers2;
    }
}
