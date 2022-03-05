using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 targetDir;
    public Vector3 targetPos;
    public Vector3 targetLocal;
    public GameObject target;

    public int sightDistance = 5;
    
    private RaycastHit2D hitup;
    private RaycastHit2D hitdown;
    private RaycastHit2D hitright;
    private RaycastHit2D hitleft;
    
    [SerializeField]
    private Tilemap GroundTilemap;
    [SerializeField]
    private Tilemap ObstacleTilemap;

    void Move()
    {
        targetPos = GameObject.Find("Player").transform.position;
        targetLocal = transform.InverseTransformPoint(targetPos);

        hitup = Physics2D.Raycast(transform.position, Vector2.up, 1);
        hitdown = Physics2D.Raycast(transform.position, Vector2.down, 1);
        hitright = Physics2D.Raycast(transform.position, Vector2.right, 1);
        hitleft = Physics2D.Raycast(transform.position, Vector2.left, 1);

        targetDir = new Vector3(0, 0, 0);
        if (Vector3.Distance(target.transform.position, transform.position) <= sightDistance)
        {
            if ((Mathf.RoundToInt(targetLocal.x) < 0 || Mathf.RoundToInt(targetLocal.x) > 0) &&
                Mathf.RoundToInt(targetLocal.y) > 0 && hitup.collider == null)
            {
                targetDir = Vector3.up;
            }
            else if ((Mathf.RoundToInt(targetLocal.x) < 0 || Mathf.RoundToInt(targetLocal.x) > 0) &&
                     Mathf.RoundToInt(targetLocal.y) < 0 && hitdown.collider == null)
            {
                targetDir = Vector3.down;
            }


            if (Mathf.RoundToInt(targetLocal.x) == 0 && Mathf.RoundToInt(targetLocal.y) > 0 && hitup.collider == null)
            {
                targetDir = Vector3.up;
            }
            else if (Mathf.RoundToInt(targetLocal.x) == 0 && Mathf.RoundToInt(targetLocal.y) < 0 &&
                     hitdown.collider == null)
            {
                targetDir = Vector3.down;
            }
            else
            {
            }


            if (Mathf.RoundToInt(targetLocal.y) == 0 && Mathf.RoundToInt(targetLocal.x) > 0 && hitleft.collider == null)
            {
                targetDir = Vector3.right;
            }
            else if (Mathf.RoundToInt(targetLocal.y) == 0 && Mathf.RoundToInt(targetLocal.x) < 0 &&
                     hitright.collider == null)
            {
                targetDir = Vector3.left;
            }
            else
            {
            }
        }
        else
        {
            int wander = Random.Range(1, 4);
            
            if (wander == 1 && hitup.collider == null)
            {
                targetDir = Vector3.up;
            }
            else if (wander == 2 && hitdown.collider == null)
            {
                targetDir = Vector3.down;
            }
            else if (wander == 3 && hitleft.collider == null)
            {
                targetDir = Vector3.right;
            }
            else if (wander == 4 && hitright.collider == null)
            {
                targetDir = Vector3.left;
            }
            else
            {
            }
        }

        if (CanMove(targetDir))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + targetDir, 1);
        }
        
    }
    
    private bool CanMove(Vector2 direction)
    {
        
        Vector3Int gridPosition = GroundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!GroundTilemap.HasTile(gridPosition) || ObstacleTilemap.HasTile(gridPosition))
        {
            return false;
        }
        else
        {
            return true;
        }
       
    }
}
    