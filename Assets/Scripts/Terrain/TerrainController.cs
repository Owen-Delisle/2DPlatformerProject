using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] public Tilemap topMap;
    [SerializeField] public Tilemap bottomMap;

    [SerializeField] public Vector3Int mapSize;

    [SerializeField] public Tile topTile;
    [SerializeField] public Tile bottomTile;

    [SerializeField] public Tile goldTile;
    [SerializeField] public Tile diamondTile;
    [SerializeField] public Tile hoverTile;

    [SerializeField] public GameObject lavaPrefab;

    public int[,] terrainMap;

    public List<Vector3Int> lavaSourceBlocks = new List<Vector3Int>();

    private void Start()
    {
    }

    private void Update()
    {
    }
}
