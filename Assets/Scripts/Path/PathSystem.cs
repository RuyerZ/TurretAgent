using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : MonoBehaviour
{
    [System.Serializable]
    public struct PathList
    {
        public string name;
        public Transform[] waypoints;
    }

    private class Path
    {
        private Vector3[] mWayPoints;
        private float[] mPointDists;
        private int mLength;
        public Path(Vector3[] points)
        {
            // Get the waypoints
            mLength = points.Length;
            Debug.Assert(mLength > 0);
            mWayPoints = new Vector3[mLength];
            points.CopyTo(mWayPoints, 0);

            // Get the distance to waypoints
            float fTotalDist = 0.0f;
            mPointDists = new float[mLength];
            for (int i = 1; i < mLength; i++)
            {
                mPointDists[i - 1] = fTotalDist;
                fTotalDist += Vector3.Distance(mWayPoints[i], mWayPoints[i - 1]);
            }
            mPointDists[mLength - 1] = fTotalDist;
        }
        public float PathDistance { get { return mPointDists[mPointDists.Length - 1]; } }
        public int Length { get { return mLength; } }
        public Vector3 GetPositionFromDistance(float dist)
        {
            if (dist < 0.0f)
                return mWayPoints[0];
            else if (dist >= PathDistance)
                return mWayPoints[mLength - 1];

            int index = FindSegmentIndex(dist, 0, mLength);
            return mWayPoints[index] + (dist - mPointDists[index]) / (mPointDists[index + 1] - mPointDists[index]) * (mWayPoints[index + 1] - mWayPoints[index]);
        }
        public bool IsPathEnd(float dist) { return (dist >= PathDistance); }
        private int FindSegmentIndex(float dist, int start, int end)
        {
            Debug.Assert(end >= start);
            if (end - start <= 1)
                return start;
            int mid = (start + end) / 2;
            if (mPointDists[mid] == dist)
                return mid;
            else if (mPointDists[mid] < dist)
                return FindSegmentIndex(dist, mid, end);
            return FindSegmentIndex(dist, start, mid);
        }
    }

    public PathList[] mPathList;
    private Dictionary<string, Path> mPathDict;

    // Start is called before the first frame update
    void Awake()
    {
        InitPathDict();
        /*for (float i = 0.0f; i < 8.0f; i += 0.1f)
        {
            Debug.Log(GetPositionFromPath("path1", i));
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitPathDict()
    {
        mPathDict = new Dictionary<string, Path>();

        for (int i = 0; i < mPathList.Length; i++)
        {
            if (!mPathDict.ContainsKey(mPathList[i].name))
            {
                Transform[] wps = mPathList[i].waypoints;
                int length = wps.Length;
                Vector3[] points = new Vector3[length];
                for (int j = 0; j < length; j++)
                {
                    points[j] = wps[j].position;
                }
                Path path = new Path(points);
                mPathDict.Add(mPathList[i].name, path);
            }
        }
    }

    public bool PathExists(string pathname) { return mPathDict.ContainsKey(pathname); }

    public Vector3 GetPositionFromPath(string pathname, float dist)
    {
        Debug.Assert(PathExists(pathname));
        return mPathDict[pathname].GetPositionFromDistance(dist);
    }

    public bool IsPathEnd(string pathname, float dist)
    {
        return PathExists(pathname) && mPathDict[pathname].IsPathEnd(dist);
    }
}
