using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> skins;
    public LevelInfo levelInfo;
    private List<Player> players;
    

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Player_Init()
    {
        
        var skinPl = skins[Random.Range(0, skins.Count)];
        for (int i = 0; i < 4; i++)
        {

            var newPlayer = new Player();
            players.Add(newPlayer);

            newPlayer.posX = levelInfo.playerPozX[i];
         /*   players[i].posY = levelInfo.playerPozY[i];
            players[i].skin = skinPl;*/
            
            

        }
    }

    void Spawn_Players()
    {

        foreach (var player in players)
        {
            Debug.Log($"{ player.posX}, { player.posY}");
        }
    }
}
