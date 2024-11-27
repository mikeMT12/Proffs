using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Level_data
{
    public int level_id;
    public int plaeyrCount;
    public int level_size;
    public Vector3 floor_pos;
    public List<int> playerPozX;
    public List<int> playerPozY;

}
public class Level_serialize : MonoBehaviour
{
    private string filePath;
    [SerializeField] private List<string> jsonPaths;
    public Level_data level_data;
    [SerializeField] private Level_load level_load;

    private void Awake()
    {
        if (!Directory.Exists(Application.dataPath + "/Resources"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources");
        }
        //if(File.Exists(Application.dataPath + "/Resources/" + )
    }

    void Start()
    {
        
        
        filePath = Application.dataPath + "/Resources/";
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Resources");
        FileInfo[] info = dir.GetFiles("*.xml");
        foreach (FileInfo f in info)
        {
            print(f.Name);
        }
        for (int i = 0; i < jsonPaths.Count; i++)
        {

            string jsonFile = jsonPaths[i];
            //Level_data level_data = new Level_data { level_id = i + 1, plaeyrCount = 4, level_size = 2, floor_pos=new Vector3(2.682f,0, 2.672f) };


            //ParseToJson(jsonFile, level_data);

            //Debug.Log(level_data.level_id);
            //Debug.Log(File.Exists(filePath + jsonFile)); 
            // Saving to SO
            LevelInfo level_SO = ScriptableObject.CreateInstance<LevelInfo>();
            level_SO.name = "Level_" + (i + 1) + "_Info";
            ParseFormJson(jsonFile, level_data, level_SO);
            level_load.levelInfos.Add(level_SO);

            //Debug.Log(level_SO.level_id);
            /*level_SO.level_id = level_data.level_id;
            level_SO.plaeyrCount = level_data.plaeyrCount;
            level_SO.level_size = level_data.level_size;
            Debug.Log(level_SO);*/
            /*string json = JsonUtility.ToJson(level_SO);
            Debug.Log(json);
            ParseJson(jsonFile, level_SO);*/
        }
        level_load.Level_Load();
    }

    public void ParseToJson(string jsonFile, Level_data level_data)
    {

        /*if (!File.Exists(filePath + jsonFile))
        {
            File.Create(filePath + jsonFile);
        }*/
        string json = JsonUtility.ToJson(level_data);
        //Debug.Log(json);

        File.WriteAllText(filePath + jsonFile, json);
        

    }

    public void ParseFormJson(string jsonFile, Level_data level_data, LevelInfo level_SO)
    {
        string json = File.ReadAllText(filePath + jsonFile);
        level_data = JsonUtility.FromJson<Level_data>(json);
        //Debug.Log(level_data.level_id);
        level_SO.level_id = level_data.level_id;
        level_SO.plaeyrCount = level_data.plaeyrCount;
        level_SO.level_size = level_data.level_size;
        level_SO.floor_pos = level_data.floor_pos;
        level_SO.playerPozX = level_data.playerPozX;
        level_SO.playerPozY = level_data.playerPozY;   
        //Debug.Log(level_SO.level_id);
    }

    // Update is called once per frame
    void Update()
    {
        
       
        
    }

  
}
