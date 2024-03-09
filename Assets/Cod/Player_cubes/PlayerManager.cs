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
    public float speed;
    private float progress;
    public Vector3 offset;
    public Component player_comp;



    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click, cheking triger.");
            CastRay();
        }

    }
    
 
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
            playerObj.GetComponent<Player>().posX = player.posX;
            playerObj.GetComponent<Player>().posY = player.posY;
            playerObj.GetComponent<Player>().skin = player.skin;
            playerObj.GetComponent<Player>().rotation = player.rotation;
            playerObj.GetComponent<Player>().vect_pos = player.vect_pos;
            //Player sc = gameObject.AddComponent<player_comp>();
            //playerObj.AddComponent(System.Type.GetType("Player" + ", Assembly-CSharp"));
            //playerObj.transform.Rotate(new Vector3(0,player.rotation,0));
            playerObjs.Add(playerObj);
            playerObj.transform.SetParent(Parent);
            
        }
    }

    Vector3 FindPos(Player player)
    {
        string name = $"{ player.posX};{ player.posY}";
        GameObject obj = GameObject.Find(name);
        player.rotation = obj.transform.rotation.y;
        player.vect_pos = obj.transform.position;
        Debug.Log(obj.transform.position);
        return player.vect_pos;
    }

    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);

            //Debug.Log(hit.collider.gameObject.GetComponent<PlyerMovement>().players[0].name);
            if(hit.collider.gameObject.GetComponent<PlyerMovement>() != null && hit.collider.gameObject.tag != "CentreZon")
            {
                /*foreach (GameObject player in hit.collider.gameObject.GetComponent<PlyerMovement>().players)
                {
                    StartCoroutine(CalculatePos(player, hit));
                }*/
                
                StartCoroutine(MovePlayer(hit.collider.gameObject.GetComponent<PlyerMovement>().players, hit.collider.gameObject));
                
            }
            
        }
    }

/*    private IEnumerator CalculatePos(GameObject player, RaycastHit hit)
    {
        
        while(player.transform.position != FindTargetPos(player, hit))
        {

        }
        player.GetComponent<Player>();
        yield return null;
    }

    Vector3 FindTargetPos(GameObject player, RaycastHit hit)
    {
        Vector3 target = new Vector3();
        target.x = player.GetComponent<Player>().posX;
        target.z = player.GetComponent<Player>().posY;
        if (hit.collider.gameObject.tag == "RightZone")
        {
            if(GameObject.Find("3;2"))
            {
                target.z = 2;
            }
        return (target);
    }*/

    private IEnumerator MovePlayer(List<GameObject> players, GameObject zone)
    {
        
        foreach (GameObject player in players)
        {


            while (CheckPos(player) != player.transform.position)
            {
                Debug.Log(CheckPos(player));
                var progress = speed * Time.deltaTime;
                player.transform.position = Vector3.Lerp(player.transform.position, CheckPos(player), progress);
                
                //player.transform.position = Vector3.MoveTowards(transform.position, CheckPos(player), progress);

                //player.transform.Translate((CheckPos(player) - player.transform.position) * Time.deltaTime);
                
                yield return null;
            }

            
        }
        
    }

    Vector3 CheckPos (GameObject player)
    {
        Vector3 targerPos = new Vector3();
        Ray ray = new Ray(player.transform.position + offset, player.transform.forward);
        Debug.DrawRay(player.transform.position, player.transform.forward, Color.yellow);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "CentreZon")
            {
                targerPos = hit.collider.transform.position;
                Debug.DrawRay(targerPos, Vector3.up, Color.red);
            }
        }
        else
        {
            targerPos = player.transform.position;
        }
        //Debug.Log(centerPos);
        return (targerPos);
    }



}
