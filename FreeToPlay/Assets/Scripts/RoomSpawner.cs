using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 -> need bottom door
    //2 -> need top door
    //3 -> need left door
    //4 -> need right door

    private RoomTemplates templates;
    private GameObject grid;
    private int rand;
    private bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        grid = GameObject.FindGameObjectWithTag("Grid");
        Invoke("Spawn", 0.5f);
    }


    void Spawn()
    {
        if(spawned == false)
        {
            if (openingDirection == 1)
            {
                //Need to spawn a room with a bottom door
                rand = Random.Range(0, templates.bottomRooms.Length);
                GameObject newRoom = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                newRoom.transform.parent = grid.transform;
            }
            else if (openingDirection == 2)
            {
                //Need to spawn a room with a top door
                rand = Random.Range(0, templates.topRooms.Length);
                GameObject newRoom = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                newRoom.transform.parent = grid.transform;
            }
            else if (openingDirection == 3)
            {
                //Need to spawn a room with a left door
                rand = Random.Range(0, templates.leftRooms.Length);
                GameObject newRoom = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                newRoom.transform.parent = grid.transform;
            }
            else if (openingDirection == 4)
            {
                //Need to spawn a room with a right door
                rand = Random.Range(0, templates.rightRooms.Length);
                GameObject newRoom = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                newRoom.transform.parent = grid.transform;
            }
            spawned = true;
            Debug.Log("spawned = true");
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint") && other.GetComponent<RoomSpawner>().spawned == true)
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
        }
    }
}
