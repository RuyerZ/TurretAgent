using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTest : MonoBehaviour
{
    public float speed = 0.01f;
    public string pathName = "path1";
    public PathSystem pathSystem;

    private float distance = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(pathSystem != null);
    }

    // Update is called once per frame
    void Update()
    {
        distance += speed;
        transform.position = pathSystem.GetPositionFromPath(pathName, distance);
    }
}
