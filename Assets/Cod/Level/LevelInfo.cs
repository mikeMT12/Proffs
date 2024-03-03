using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "CreateLevel/Level")]
public class LevelInfo : ScriptableObject
{
    public int level_id;
    public int plaeyrCount;
    public int level_size;
    public GameObject cube;
    public List<Player> players;
    public float floor_offset;
    public Vector3 floor_pos;
    //private List<Player> playerPoz;

}
