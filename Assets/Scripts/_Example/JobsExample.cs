using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;
using UnityEngine.Jobs;


[BurstCompile]
public struct UpdatePositionJob : IJobParallelForTransform
{
    public int gridX;
    public float time;

    public void Execute(int index, TransformAccess transform)
    {
           float add = (float)(index % (gridX / 5f)) / (gridX / 5f);
           transform.position = new Vector3(transform.position.x, Mathf.Sin(add + time) * 2f, transform.position.z);
    }
}

public class JobsExample : MonoBehaviour
{

    public GameObject cubeTemplate = null;

    public int gridX = 1000;
    public int gridY = 1000;

    private Transform[] transforms = null;
    private TransformAccessArray transformAccess = default;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCube();
        transformAccess = new TransformAccessArray(transforms);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositionJob updateJob = new UpdatePositionJob();
        updateJob.gridX = gridX;
        updateJob.time = Time.realtimeSinceStartup;
        updateJob.Schedule(transformAccess);
        //for (int i = 0; i < transforms.Length; i++)
        //{
        //    float add = (float)(i % (gridX / 5f) / (gridX / 5f));
        //    transforms[i].position = new Vector3(transforms[i].position.x, Mathf.Sin(add + Time.realtimeSinceStartup) * 2f, transforms[i].position.z);
        //}

    }
    private void OnDestroy()
    {
        transformAccess.Dispose();
    }

    public void SpawnCube()
    {
        int x = 0;
        int y = 0;
        transforms = new Transform[gridX * gridY];

        for(int i = 0; i < transforms.Length; i++)
        {
            GameObject go = Instantiate(cubeTemplate);
            go.transform.position = new Vector3(x * 1f, 0, y * 1f);
            transforms[i] = go.transform;
            x = (x + 1) % gridX;
            if (x == 0)
            {
                y++;
            }
        }

    }
}
