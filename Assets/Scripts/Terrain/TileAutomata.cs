using System.Collections;
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

    //[Range(0, 100)] [SerializeField] private int tileNumber; 

    [SerializeField] private Vector3Int tileMapSize;

    private int width;
    private int height;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            RunSimulation(lifeCycles);
        }
        if(Input.GetKeyDown(KeyCode.Dollar))
        {
            ClearMap(true);
        }
    }

    private void RunSimulation(int lifeCycles)
    {
        ClearMap(true);
        width = tileMapSize.x;
        height = tileMapSize.y;

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
        Tile tile;
        int lavaChance = Random.Range(0, 1000);

        RangeInt lavaRange = new RangeInt(0, 1);

        tile = terrainController.bottomTile;
        if (lavaChance >= lavaRange.start && lavaChance <= lavaRange.end)
        {
            tile = terrainController.lavaTile;
        }
        terrainController.bottomMap.SetTile(new Vector3Int(-row + width / 2, -col + height / 2, 0), tile);
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

    private int[,] GenerateTilePositions(int[,]tileMap)
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
