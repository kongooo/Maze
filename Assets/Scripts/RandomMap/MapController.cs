using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public int width, height;
    public bool IfUseRandomSeed;
    public string seed;

    [Range(0, 100)]
    public int randomFillPercent;

    private int[,] map;

	// Use this for initialization
	void Start () {
        GenerateMap();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            GenerateMap();
	}

    void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();
        for (int i = 0; i < 5; i++)
            SmoothMap();
    }

    void RandomFillMap()
    {
        if (IfUseRandomSeed)
            seed = Time.time.ToString();
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                    map[i, j] = 1;
                else
                    map[i, j] = (pseudoRandom.Next(0, 100) > randomFillPercent) ? 1 : 0;
            }
    }



    void SmoothMap()
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                int nearWallCount = GetNearBlock(i, j);
                if (nearWallCount > 4)
                    map[i, j] = 1;
                else if (nearWallCount < 4)
                    map[i, j] = 0;
            }
               
    }

    int GetNearBlock(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int i = gridX - 1; i <= gridX + 1; i++)
            for (int j = gridY - 1; j <= gridY + 1; j++)
                if (i >= 0 && i < width && j >= 0 && j < height)
                {
                    if (i != gridX || j != gridY)
                        wallCount += map[i, j];
                }
                else
                    wallCount++;
        return wallCount;
    }

    private void OnDrawGizmos()
    {
        if (map != null)
        {
            for(int i=0;i<width;i++)
                for(int j=0;j<height;j++)
                {
                    Gizmos.color = (map[i, j] == 0) ? Color.white : Color.black;
                    Vector3 pos = new Vector3(-width / 2 + i + 0.5f, -height / 2 + j + 0.5f, 0);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
        }
    }
}
