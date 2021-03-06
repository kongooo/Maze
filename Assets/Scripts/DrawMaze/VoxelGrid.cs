﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class VoxelGrid : MonoBehaviour {

    public int resolution;
    public GameObject voxelPrefab;
    private bool[] voxels;
    private float voxelSize;
    
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(int resolution,float size)
    {
        this.resolution = resolution;
        voxelSize = size / resolution;
        voxels = new bool[resolution * resolution];
        for (int i = 0, y = 0; y < resolution; y++)
            for (int x = 0; x < resolution; x++, i++)
                CreateVoxel(i, x, y);
    }

    private void CreateVoxel(int i,int x,int y)
    {
        GameObject o = Instantiate(voxelPrefab) as GameObject;
        o.transform.parent = transform;
        o.transform.localPosition = new Vector3((x + 0.5f) * voxelSize, (y + 0.5f) * voxelSize, 0);
        o.transform.localScale = voxelSize * Vector3.one * 0.9f;
    }
}
