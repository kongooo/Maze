using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelMap : MonoBehaviour {

    public VoxelGrid voxelGrid;
    public int voxelResolution,chunkResolution;
    public float size;

    private VoxelGrid[] chunks;
    private float chunkSize, voxelSize, halfSize;

	// Use this for initialization
	void Start () {

        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(size, size);

        halfSize = size * 0.5f;
        chunkSize = size / chunkResolution;
        voxelSize = chunkSize / voxelResolution;

        chunks = new VoxelGrid[chunkResolution * chunkResolution];
        for (int i = 0, y = 0; y < chunkResolution; y++)
            for (int x = 0; x < chunkResolution; x++, i++)
                CreatChunk(i, x, y);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
            {
                if (hit.collider.gameObject == gameObject)
                    EditVoxels(transform.InverseTransformPoint(hit.point));
            }
        }
	}

    private void EditVoxels(Vector3 point)
    {

    }

    void CreatChunk(int i,int x,int y)
    {
        VoxelGrid chunk = Instantiate(voxelGrid) as VoxelGrid;
        chunk.Initialize(voxelResolution, chunkSize);
        chunk.transform.parent = transform;
        chunk.transform.localPosition = new Vector3(x * chunkSize - halfSize, y * chunkSize - halfSize);
        chunks[i] = chunk;
    }
}
