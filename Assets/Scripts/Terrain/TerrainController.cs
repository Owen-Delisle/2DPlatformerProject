using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] public Tilemap topMap;
    [SerializeField] public Tilemap bottomMap;

    [SerializeField] public Tile topTile;
    [SerializeField] public Tile bottomTile;

    [SerializeField] public Tile goldTile;
    [SerializeField] public Tile diamondTile;
    [SerializeField] public Tile hoverTile;

    [SerializeField] public Tile lavaTile;

    public int[,] terrainMap;

    private void Start()
    {
    }

    private void Update()
    {
    }
}
