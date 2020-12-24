using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mining : MonoBehaviour
{
    // Reference to Parent Player Script
    [SerializeField] private PlayerController playerController;

    Vector3Int prevPosition = new Vector3Int();
    Color prevColor = new Color();
    Vector3Int mouseCellPosition = new Vector3Int();

    Tilemap topMap;

    private Color hoverColor = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        topMap = playerController.terrainController.topMap;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseCellPosition();
        SetHoverTile();
        MineBlock();
    }

    private void UpdateMouseCellPosition()
    {
        mouseCellPosition = topMap.WorldToCell(playerController.mousePosition);
    }

    private void SetHoverTile()
    {
        if (mouseCellPosition != prevPosition)
        {
            topMap.SetColor(prevPosition, prevColor);
            topMap.SetTileFlags(mouseCellPosition, TileFlags.None);
            prevColor = topMap.GetColor(mouseCellPosition);
            topMap.SetColor(mouseCellPosition, hoverColor);

            prevPosition = mouseCellPosition;
        }
    }

    private void MineBlock()
    {
        if (playerController.isMinePressed == true)
        {
            playerController.isMinePressed = false;
            topMap.SetTile(mouseCellPosition, null);
        }
    }
}
