using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlayerController : MonoBehaviour
{
    //VARIABLES
    private float moveTimer = 0;
    public float moveSpeed = 5;

    private PlayerMovement controls;

    [SerializeField]
    private Tilemap GroundTilemap;
    [SerializeField]
    private Tilemap ObstacleTilemap;

    private GameObject[] enemies;
    private PlayerStats myStats;
    
    
    private void Awake()
    {
        controls = new PlayerMovement();
        myStats = GetComponent<PlayerStats>(); //Get your movement and stats...
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        if (moveTimer <= 0)
        {
            moveTimer = moveSpeed;
            if (CanMove(direction))
            {
               
                transform.position += (Vector3) direction;

            }

            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyMovement>().SendMessage("Move");
            }
        }
    }

    private bool CanMove(Vector2 direction)
    {
        
        Vector3Int gridPosition = GroundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (Physics2D.Raycast(transform.position, direction, 1, LayerMask.GetMask("Wall")).collider != null)
        {
            Debug.Log("Blah");
            return false;
        }
        else 
        {

            if(Physics2D.Raycast(transform.position, (Vector2)direction, 1, LayerMask.GetMask("Enemy")).collider == null)
            {
                return true;
            }
            else if(Physics2D.Raycast(transform.position, (Vector2)direction, 1, LayerMask.GetMask("Enemy")).transform.CompareTag("Enemy"))
            {
                Attack(direction);
                return false;
            }
            else
            {
                //Debug.Log(Physics2D.Raycast(transform.position, (Vector2)direction, 1).transform.tag);
                return true;
                
            }
           
        }
    }

    void Attack(Vector2 direction)
    {
        //Debug.Log("WHACK");
        Physics2D.Raycast(transform.position, direction, 1, LayerMask.GetMask("Enemy"))
            .collider.gameObject.GetComponent<EnemyStats>().SendMessage("Attacked", myStats.attack); //This is all one line. Make a raycast, then invoke the Attacked function of the target and pass in the damage.
    }


    private void Update()
    {
        moveTimer -= Time.deltaTime;
        //Debug.Log(moveTimer);
    }
}
