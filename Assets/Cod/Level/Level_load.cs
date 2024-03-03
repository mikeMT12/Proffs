using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_load : MonoBehaviour
{
    public List<LevelInfo> levelInfos;
    public List<GameObject> cubes;
    public List<GameObject> floors;
    [SerializeField] private LevelInfo levelInfo;
    [SerializeField] private GameObject floor;

    void Start()
    {
        
       
    }
    public void Level_Load()
    {
        levelInfo = levelInfos[Random.Range(0, levelInfos.Count)];
        levelInfo.cube = cubes[Random.Range(0, cubes.Count)];
        floor = floors[levelInfo.level_size];
        Generate_Level();
        //levelInfo.players = 
        print(levelInfo.floor_pos);

    }

    void Generate_Level()
    {
        Instantiate(floor, levelInfo.floor_pos,floor.transform.rotation);
    }
    void Update()
    {
        
    }
}
