using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBehavior : MonoBehaviour
{
    private GameManager mGameManager = null;
    private EnemyManager mEnemyManager = null;
    private PathSystem mPathSystem = null;

    public string mPath = "path1";
    public void SetPath(string path) { mPath = path; }

    private float mDistance = 0.0f;
    public float mSpeed = 1.0f;
    public int mCost = 1;
    // Start is called before the first frame update
    void Start()
    {
        mGameManager = GameManager.sTheGlobalBehavior;
        mPathSystem = mGameManager.mPathSystem;
        mEnemyManager = mGameManager.mEnemyManager;

        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPathEnd();
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Debug.Assert(mPathSystem != null);
        Debug.Assert(mPathSystem.PathExist(mPath));

        transform.position = mPathSystem.GetPositionFromPath(mPath, mDistance);
        mDistance += mSpeed;
    }

    private void CheckPathEnd()
    {
        if (mPathSystem.IsPathEnd(mPath, mDistance))
        {
            // GameManager do something
            // And destroy self?
        }
    }
}
