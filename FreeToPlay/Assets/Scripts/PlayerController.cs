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


    private void Awake()
    {
        controls = new PlayerMovement();
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
        if (CanMove(direction))
        {
            transform.position += (Vector3)direction;
        }
    }

    private bool CanMove(Vector2 direction)
    {
        
        Vector3Int gridPosition = GroundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if ((!GroundTilemap.HasTile(gridPosition) && moveTimer >= 0) || (ObstacleTilemap.HasTile(gridPosition) && moveTimer >= 0))
        {
            return false;
        }
        else if (moveTimer <= 0)
        {
            moveTimer = moveSpeed;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        moveTimer -= Time.deltaTime;
        Debug.Log(moveTimer);
    }
}
