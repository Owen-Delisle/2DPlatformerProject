using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Lava : MonoBehaviour
{
    [SerializeField] private TerrainController terrainController;

    Tilemap topMap;
    Tilemap bottomMap;

    Tile topTile;
    Tile lavaTile;

    int height;
    int width;

    // Start is called before the first frame update
    void Start()
    {
        topMap = terrainController.topMap;
        bottomMap = terrainController.bottomMap;

        height = -(terrainController.mapSize.y / 2);
        width = -(terrainController.mapSize.x / 2);

        topTile = terrainController.topTile;
        lavaTile = terrainController.lavaTile;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GenerateLavaTileStream();
        }
    }

    private void GenerateLavaTileStream()
    {
        foreach (var source in terrainController.lavaSourceBlocks)
        {
            Vector3Int locationBelow = new Vector3Int(source.x, source.y - 1, source.z);
            Vector3Int locationLeft = new Vector3Int(source.x - 1, source.y, source.z);
            Vector3Int locationRight = new Vector3Int(source.x + 1, source.y, source.z);

            if (topMap.GetTile(locationBelow) != topTile)
            {
                VerticalLavaStream(source);
            }
        }
    }

    private void VerticalLavaStream(Vector3Int source)
    {
        Vector3Int locationBelow = new Vector3Int(source.x, source.y - 1, source.z);
        Vector3Int locationToLeft = new Vector3Int(source.x - 1, source.y, source.z);
        Vector3Int locationToRight = new Vector3Int(source.x + 1, source.y, source.z);

        if (ShouldRepeat(1, height, locationBelow))
        {
            bottomMap.SetTile(locationBelow, lavaTile);
            VerticalLavaStream(locationBelow);
        }
        else
        {
            if (topMap.GetTile(locationToLeft) != topTile)
            {
                bottomMap.SetTile(locationToLeft, lavaTile);
                LeftLavaStream(locationToLeft);
            }
            if (topMap.GetTile(locationToRight) != topTile)
            {
                bottomMap.SetTile(locationToRight, lavaTile);
                RightLavaStream(locationToRight);
            }
        }
    }

    private void LeftLavaStream(Vector3Int source)
    {
        Vector3Int locationToLeft = new Vector3Int(source.x - 1, source.y, source.z);
        Vector3Int locationBelow = new Vector3Int(source.x, source.y - 1, source.z);

        if (ShouldRepeat(1, height, locationBelow))
        {
            bottomMap.SetTile(locationBelow, lavaTile);
            VerticalLavaStream(locationBelow);
        }
        else
        {
            if (ShouldRepeat(0, 0, locationToLeft))
            {
                bottomMap.SetTile(locationToLeft, lavaTile);
                LeftLavaStream(locationToLeft);
            }
        }
    }

    private void RightLavaStream(Vector3Int source)
    {
        Vector3Int locationToRight = new Vector3Int(source.x + 1, source.y, source.z);
        Vector3Int locationBelow = new Vector3Int(source.x, source.y - 1, source.z);

        if (ShouldRepeat(1, height, locationBelow))
        {
            bottomMap.SetTile(locationBelow, lavaTile);
            VerticalLavaStream(locationBelow);
        }
        else
        {
            if (ShouldRepeat(2, width, locationToRight))
            {
                bottomMap.SetTile(locationToRight, lavaTile);
                RightLavaStream(locationToRight);
            }
        }
    }

    private bool ShouldRepeat(int direction, int max, Vector3Int nextLocation)
    {
        if (topMap.GetTile(nextLocation) != topTile)
        {
            if (direction == 0)
            {
                if (nextLocation.x > max)
                    return true;
            }

            if (direction == 1)
            {
                if (nextLocation.y >= max)
                    return true;
            }

            if (direction == 2)
            {
                if (nextLocation.x <= max)
                    return true;
            }
        }
        return false;
    }
}
