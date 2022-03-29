using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    [Header("Target Variables")]
    public Vector3 targetDir;
    public Vector3 targetPos;
    public Vector3 targetLocal;
    public GameObject target;

    [Header("")]
    public int sightDistance = 5;
    //Raycasting storage variables
    private RaycastHit2D hitup;
    private RaycastHit2D hitdown;
    private RaycastHit2D hitright;
    private RaycastHit2D hitleft;
    
    [Header("Tilemaps")]
    [SerializeField] [Tooltip("This is the tilemap this object can walk on")]
    private Tilemap GroundTilemap;
    [SerializeField] [Tooltip("This is the tilemap the object treats as an obstacle")]
    private Tilemap ObstacleTilemap;

    private EnemyStats myStats; //The stat component of the enemy

    private void Start()
    {
        myStats = GetComponent<EnemyStats>(); //Get your stat component
        target = GameObject.Find("Player");
    }

    void Move()
    {
        targetPos = GameObject.Find("Player").transform.position; //Get the target player's position.
        targetLocal = transform.InverseTransformPoint(targetPos); //Convert that position to a local (relative) position.

        hitup = Physics2D.Raycast(transform.position, Vector2.up, 1, LayerMask.GetMask("Player"));
        hitdown = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Player"));
        hitright = Physics2D.Raycast(transform.position, Vector2.right, 1, LayerMask.GetMask("Player"));
        hitleft = Physics2D.Raycast(transform.position, Vector2.left, 1, LayerMask.GetMask("Player")); //Raycast in the four directions of movement

        targetDir = new Vector3(0, 0, 0); //reset the target direction to move in.
        if (Vector3.Distance(target.transform.position, transform.position) <= sightDistance) //If the distance between the player and the enemy is less than the enemy's sight distance...
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= 1.3) //If the target is within attacking distance...
            {
                //Debug.Log("Enemy Attacking");
                target.SendMessage("Attacked", myStats.damage); //Tell them they've been attacked and for how much damage.
                return; //Don't do anything else.
            }
            
            if (Mathf.RoundToInt(targetLocal.y) > 0 && hitup.collider == null) //If the player is above the enemy's position and the space above the enemy is clear...
            {
                targetDir = Vector3.up; //Set your destination to up.
            }
            else if (Mathf.RoundToInt(targetLocal.y) < 0 && hitdown.collider == null) //Otherwise, if the player is below the enemy's position and the space below the enemy is clear...
            {
                targetDir = Vector3.down; //Set your destination to down.
            }


            if (Mathf.RoundToInt(targetLocal.y) == 0 && Mathf.RoundToInt(targetLocal.x) > 0 && hitleft.collider == null) //If the player is to the right and not above or below and the space is clear...
            {
                targetDir = Vector3.right; //Set your destination to right.
            }
            else if (Mathf.RoundToInt(targetLocal.y) == 0 && Mathf.RoundToInt(targetLocal.x) < 0 &&
                     hitright.collider == null) //Otherwise, if the player is to the left and not above or below and the space is clear...
            {
                targetDir = Vector3.left; //Set your destination to left.
            }
        }
        else //If the distance between the player and enemy is larger than the enemy's sight distance...
        {
            Wander(); //Call the wander function to randomize movement direction.
        }

        if (CanMove(targetDir)) //If the CanMove function returns true using the target direction variable...
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + targetDir, 1); //Move to the target position
        }
        else //If it can't (I.E. there's an obstacle between the enemy and the player)
        {
            Wander(); //Call the wander function to randomize movement
            transform.position = Vector3.MoveTowards(transform.position, transform.position + targetDir, 1); //Move to the target position
        }
        
    }
    
    private bool CanMove(Vector2 direction)
    {
        
        Vector3Int gridPosition = GroundTilemap.WorldToCell(transform.position + (Vector3)direction); //Convert the position targeted (current position plus target direction) to a tilemap cell
        if (Physics2D.Raycast(transform.position, direction, 1, LayerMask.GetMask("Wall")).collider != null) //If a raycast in the target direction hits a collider in the "wall" layer...
        {
            return false; //Say you can't move there.
        }
        else
        {
            return true; //Say you can move there.
        }
       
    }

    public void Wander()
    {
        int tryWander = 10;
        while (tryWander >= 0) //Forever...
        {
            int wander = Random.Range(1, 5); //Get a random number from 1 to 4 (the minimum is inclusive, but the maximum is exclusive, don't ask why)

            if (wander == 1 && hitup.collider == null) //If it's 1, and the space above you is clear...
            {
                targetDir = Vector3.up; //Go up.
                break; //Stop the while loop
            }
            else if (wander == 2 && hitdown.collider == null)
            {
                targetDir = Vector3.down; //Repeat for down (on a 2)
                break; //Stop the loop
            }
            else if (wander == 3 && hitright.collider == null)
            {
                targetDir = Vector3.right; //And for right (on a 3)
                break; //Stop loop
            }
            else if (wander == 4 && hitleft.collider == null)
            {
                targetDir = Vector3.left; //And left (4)
                break; //Stop
            }
            else
            {
                tryWander--;
                //The enemy tried to move into a wall. It will to make a valid move up to 10 times. This is a failsafe to prevent hardlocks.
            }
        }
    }
}
    