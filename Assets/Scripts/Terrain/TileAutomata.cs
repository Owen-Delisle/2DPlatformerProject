﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileAutomata : MonoBehaviour
{
    [SerializeField] private TerrainController terrainController;

    [Range(0,100)] [SerializeField] private int chanceAtLife;
    [Range(0, 8)] [SerializeField] private int birthLimit;
    [Range(0, 8)] [SerializeField] private int deathLimit;
    [Range(1, 10)] [SerializeField] private int lifeCycles;

    private int width;
    private int height;

    [SerializeField] private GameObject lavaSpawnPoint;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //terrainController.lavaSourceBlocks = new List<Vector3Int>();
            terrainController.lavaSourceBlocks.Clear();
            RunSimulation(lifeCycles);
        }
    }

    private void RunSimulation(int lifeCycles)
    {
        ClearMap(true);
        width = terrainController.mapSize.x;
        height = terrainController.mapSize.y;

        if(terrainController.terrainMap == null)
        {
            terrainController.terrainMap = new int[width, height];
            FirstGeneration();
        }

        for(int generation = 0; generation < lifeCycles; generation++)
        {
            terrainController.terrainMap = GenerateTilePositions(terrainController.terrainMap);
        }

        for(int row = 0; row < width; row++)
        {
            for(int col = 0; col < height; col++)
            {
                PlaceBottomTile(row, col);
                if (terrainController.terrainMap[row,col] == 1)
                {
                    PlaceTopTile(row, col);
                }
            }
        }
    }

    private void PlaceTopTile(int row, int col)
    {
        Tile tile;
        int goldChance = Random.Range(0, 300);
        int diamondChance = Random.Range(0, 1000);

        RangeInt goldRange = new RangeInt(0, 1);
        RangeInt diamondRange = new RangeInt(2, 3);

        tile = terrainController.topTile;
        if (goldChance >= goldRange.start && goldChance <= goldRange.end)
        {
            tile = terrainController.goldTile;
        }
        if (diamondChance >= diamondRange.start && diamondChance <= diamondRange.end)
        {
            tile = terrainController.diamondTile;
        }

        terrainController.topMap.SetTile(new Vector3Int(-row + width / 2, -col + height / 2, 0), tile);
    }

    private void PlaceBottomTile(int row, int col)
    {
        Tile tile = terrainController.bottomTile;
        int lavaChance = Random.Range(0, 1000);
        RangeInt lavaRange = new RangeInt(0, 1);

        if (lavaChance >= lavaRange.start && lavaChance <= lavaRange.end)
        {
            InstantiateLavaSpawnPoint(new Vector3Int(-row + width / 2, -col + height / 2, 0));
        }
        else
        {
            terrainController.bottomMap.SetTile(
                new Vector3Int(
                    -row + width / 2,
                    -col + height / 2,
                    0),tile);
        }
    }

    private void InstantiateLavaSpawnPoint(Vector3Int spawnPosition)
    {
        Instantiate(lavaSpawnPoint,
            new Vector3(
                spawnPosition.x,
                spawnPosition.y,
                spawnPosition.z
                ),
            Quaternion.identity);
    }

    private void ClearMap(bool complete)
    {
        terrainController.topMap.ClearAllTiles();
        terrainController.bottomMap.ClearAllTiles();

        if(complete)
        {
            terrainController.terrainMap = null;
        }
    }

    private void FirstGeneration()
    {
        for(int row = 0; row < width; row++)
        {
            for(int column = 0; column < height; column++)
            {
                terrainController.terrainMap[row, column] = Random.Range(1, 101) < chanceAtLife ? 1 : 0;
            }
        }
    }

    private int[,] GenerateTilePositions(int[,] tileMap)
    {
        int[,] outputMap = new int[width, height];
        int neighbour;
        BoundsInt surroundingTiles = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for(int row = 0; row < width; row++)
        {
            for(int col = 0; col < height; col++)
            {
                neighbour = 0;
                foreach(var tile in surroundingTiles.allPositionsWithin)
                {
                    if (tile.x == 0 && tile.y == 0) continue;
                    if(row+tile.x >= 0 && row+tile.x < width && col+tile.y >= 0 && col+tile.y < height)
                    {
                        neighbour += tileMap[row + tile.x, col + tile.y];
                    }
                    else
                    {
                        neighbour++;
                    }
                }
                if(tileMap[row,col] == 1)
                {
                    if (neighbour < deathLimit) outputMap[row, col] = 0;
                    else
                    {
                        outputMap[row, col] = 1;
                    }
                }
                if (tileMap[row, col] == 0)
                {
                    if (neighbour < deathLimit) outputMap[row, col] = 1;
                    else
                    {
                        outputMap[row, col] = 0;
                    }
                }
            }
        }

        return outputMap;
    }
}
