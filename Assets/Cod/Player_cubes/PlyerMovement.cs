using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyerMovement : MonoBehaviour
{
    public List<GameObject> players;
    
    void Start()
    {
        Debug.Log("Start"); 
    }

    private void OnTriggerEnter(Collider other)
    {
        players.Add(other.gameObject);
        Debug.Log(other.gameObject.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        players.Add(collision.gameObject);
        Debug.Log(collision.gameObject.name);
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
