using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> skins;
    public LevelInfo levelInfo;
    public Transform Parent;
    public List<GameObject> playerObjs;
    public GameObject floor;
  

    

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click, casting ray.");
            CastRay();
        }

    }
    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            Debug.Log(hit.collider.gameObject.GetComponent<PlyerMovement>().players[0].name);
        }
    }
  /*  private void OnMouseDown(Collider other)
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - floor.transform.position;
            Debug.Log(clickPosition);
            Debug.Log(other.gameObject.GetComponent<PlyerMovement>().players);
        }
    }*/

    public void Player_Init()
    {
        List<Player> players = new List<Player>();
        var skinPl = skins[Random.Range(0, skins.Count)];
        for (int i = 0; i < 4; i++)
        {

            var newPlayer = new Player();
            newPlayer.posX = levelInfo.playerPozX[i];
            newPlayer.posY = levelInfo.playerPozY[i];
            newPlayer.skin = skinPl;
            newPlayer.vect_pos = new Vector3(0, 0, 0);
            
            players.Add(newPlayer);
        }
        Spawn_Players(players);
    }

    void Spawn_Players(List<Player> players)
    {

        foreach (var player in players)
        {
            //Debug.Log($"{ player.posX}; { player.posY}");
            GameObject playerObj = Instantiate(player.skin, FindPos(player), player.skin.transform.rotation);
            playerObjs.Add(playerObj);
            playerObj.transform.SetParent(Parent);
            
        }
    }

    Vector3 FindPos(Player player)
    {
        string name = $"{ player.posX};{ player.posY}";
        GameObject obj = GameObject.Find(name);
        //Debug.Log(obj.name);
        player.vect_pos = obj.transform.position;
        Debug.Log(obj.transform.position);
        return player.vect_pos;
    }
}
