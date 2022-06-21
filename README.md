# Unity JSON

Unity JSON is an example Project on how to serialize / deserialize JSON Files in Unity. You probably only need to download the script or read this though.

## Installation

Head over to my Script [JSONExample.cs](https://github.com/Pydes-boop/UnityJSON/blob/main/Assets/Scripts/JSONExample.cs) to yank the code and enjoy. If you want to stay for some Explanation look below.

## JSON Serialization in Unity

Unity provides JSONUtility which needs a fitting class [see below](#json-classes-in-unity) so it can serialize/load your data. You should also load/save your data from a resources folder otherwise it might not working in a built version.

```csharp
    public JSONdata _data;
    string path = Application.dataPath + "/Resources/Files/" + _data.name + ".json";
    if (File.Exists(path)) {
        string json = File.ReadAllText(path);
        _data = JsonUtility.FromJson<JSONdata>(json);
        Debug.Log("Loaded Data from " + path);
    } else {
        Debug.LogError("Loading Data for " + _data.name + " not possible. Check if " + path + "exists.");
    }
```

You could save your JSON file in a similiar manner:
```csharp
    string json = JsonUtility.ToJson(_data);
    string path = Application.dataPath + "/Resources/Files/" + _data.name + ".json";
    System.IO.File.WriteAllText(path, json);
    Debug.Log("Saved Data of " + _data.name + " to " + path);
```

## JSON Classes in Unity

JSON in Unity can not be loaded directly in the way you might know it from other languages. The easiest way to deal with it is to build a Class that will represent your data structure and to generate and load your data using that class.

Start with a C# Class and not your JSON File. This class will hold your data.\
(Generating your JSON from a Class is the easier way to go, you can do it the other way around if you want to)
```csharp
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
```
For Unity every Variable of a class will represent a Dictionary Entry, while Arrays represent Arrays. Using Composite/Component classes will let you nest them inside each other. This Code Example will later generate a JSON like this:
```json
{
    "name": "ExampleJSON",
    "url": "https://github.com/Pydes-boop/UnityJSON",
    "description": "simple Unity Example on how to serialize and deserialize JSON in Unity",
    "data": {
        "numbers1": [
            1,
            3,
            3,
            7
        ],
        "numbers2": [
            4,
            2
        ]
    }
}
```

## Load / Save in Inspector (quick and dirty)

if you dont want to write a whole Inspector Script for a simple button to load / save your SerializedObject you can also abuse bool variables and Unitys OnValidate():

```csharp
    [Tooltip("Click to data from File")]
    public bool loadData = false;
    
    [Tooltip("Click to save data to JSON")]
    public bool saveData = false;

    public void OnValidate() {
        if (loadData) {
            loadData = false;
            //Method for Loading Data from JSON
            loadDataFromJson();
        }

        if (saveData) {
            saveData = false;
            //Method for Saving Data to JSON
            saveDataToJson();
        }
    }
```

## License
[GNU General Public](https://github.com/Pydes-boop/UnityJSON/blob/main/LICENSE)
