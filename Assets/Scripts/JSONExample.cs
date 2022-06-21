using System;
using System.IO;
using UnityEngine;

namespace JSON
{
    [CreateAssetMenu(fileName = "Example", menuName = "JSON")]
    public class JSONExample : ScriptableObject
    {
        public JSONdata _data;
        
        [Tooltip("Click to data from File")]
        public bool loadData = false;
    
        [Tooltip("Click to save data to JSON")]
        public bool saveData = false;
        
        private void saveDataToJson()
        {
            string json = JsonUtility.ToJson(_data);
            string path = Application.dataPath + "/Resources/Files/" + _data.name + ".json";
            System.IO.File.WriteAllText(path, json);
            Debug.Log("Saved Data of " + _data.name + " to " + path);
        }

        private void loadDataFromJson()
        {
            string path = Application.dataPath + "/Resources/Files/" + _data.name + ".json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _data = JsonUtility.FromJson<JSONdata>(json);
                Debug.Log("Loaded Data from " + path);
            }
            else
            {
                Debug.LogError("Loading Data for " + _data.name + " not possible. Check if " + path + "exists.");
            }
        }
        
        public void OnValidate()
        {
            if (loadEntityData)
            {
                loadData = false;
                loadDataFromJson();
            }

            if (saveData)
            {
                saveData = false;
                saveDataToJson();
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
