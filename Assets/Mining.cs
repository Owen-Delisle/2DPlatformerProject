using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mining : MonoBehaviour
{
    // Reference to Parent Player Script
    [SerializeField] private PlayerController playerController;

    // Saves the previous cursor cell positions for the top and bottom maps
    Vector3Int prevTopPosition = new Vector3Int();
    Vector3Int prevBottomPosition = new Vector3Int();

    // Saves the previous Color for the top tile color at the cursors cell position
    Color prevTopColor = new Color();
    Color prevBottomColor = new Color();

    // Saves the cursor cell positions for the top and bottom maps
    Vector3Int mouseTopCellPosition = new Vector3Int();
    Vector3Int mouseBottomCellPosition = new Vector3Int();

    // Saves the players top map cell position
    Vector3Int playerTopCellPosition = new Vector3Int();

    Tilemap topMap;
    Tilemap bottomMap;

    Tile topTile;
    Tile bottomTile;

    Tile lavaTile;

    private Color hoverColor = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        // Tile Maps from Terrain Controller
        topMap = playerController.terrainController.topMap;
        bottomMap = playerController.terrainController.bottomMap;

        // Tiles from Terrain Controller
        topTile = playerController.terrainController.topTile;
        bottomTile = playerController.terrainController.bottomTile;

        lavaTile = playerController.terrainController.lavaTile;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseCellPosition();
        UpdatePlayerCellPosition();
        if (IsMouseNextToPlayer())
        {
            SetHoverTopTile();
            SetHoverBottomTile();
            MineBlock();
            PlaceBlock();
        }
    }

    // Updates the cursors cell position for the top and bottom maps
    private void UpdateMouseCellPosition()
    {
        mouseTopCellPosition = topMap.WorldToCell(playerController.mousePosition);
        mouseBottomCellPosition = bottomMap.WorldToCell(playerController.mousePosition);
    }

    // Updates the players top map cell position
    private void UpdatePlayerCellPosition()
    {
        playerTopCellPosition = topMap.WorldToCell(playerController.transform.position);
    }

    // Converts the players mouse position in space to a top map cell position
    private void SetHoverTopTile()
    {
        if (mouseTopCellPosition != prevTopPosition)
        {
            topMap.SetColor(prevTopPosition, prevTopColor);
            topMap.SetTileFlags(mouseTopCellPosition, TileFlags.None);
            prevTopColor = topMap.GetColor(mouseTopCellPosition);
            topMap.SetColor(mouseTopCellPosition, hoverColor);

            prevTopPosition = mouseTopCellPosition;
        }
    }

    // Converts the players mouse position in space to a bottom map cell position
    private void SetHoverBottomTile()
    {
        if (mouseBottomCellPosition != prevBottomPosition)
        {
            bottomMap.SetColor(prevBottomPosition, prevBottomColor);
            bottomMap.SetTileFlags(mouseBottomCellPosition, TileFlags.None);
            prevBottomColor = bottomMap.GetColor(mouseBottomCellPosition);
            bottomMap.SetColor(mouseBottomCellPosition, Color.magenta);

            prevBottomPosition = mouseBottomCellPosition;
        }
    }

    // Checks if the mouse is close enough to the Player for interaction
    private bool IsMouseNextToPlayer()
    {
        BoundsInt tilesSurroundingPlayer = new BoundsInt(-2, -2, 0, 5, 5, 1);
        foreach (var tile in tilesSurroundingPlayer.allPositionsWithin)
        {
            if (tile.x + mouseBottomCellPosition.x == playerTopCellPosition.x
                && (tile.y + mouseBottomCellPosition.y) == playerTopCellPosition.y)
            {
                return true;
            }
        }
        return false;
    }

    // Mines the block on the cell where the players mouse is
    private void MineBlock()
    {
        if (playerController.isMinePressed == true)
        {
            playerController.isMinePressed = false;
            topMap.SetTile(mouseTopCellPosition, null);
        }
    }

    // Places a block on the cell where the players mouse is
    private void PlaceBlock()
    {
        if (playerController.isPlacePressed == true)
        {
            if (bottomMap.GetTile(mouseBottomCellPosition).name == lavaTile.name)
                bottomMap.SetTile(mouseBottomCellPosition, bottomTile);
            
            if (topMap.GetTile(mouseBottomCellPosition) == null)
                topMap.SetTile(mouseBottomCellPosition, topTile);

            playerController.isPlacePressed = false;
        }
    }
}
