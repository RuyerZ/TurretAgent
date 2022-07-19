using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{

    //  开始按钮
    public void OnPlayBtn()
    {
        SceneManager.LoadScene("LevelScene");
    }

    //  退出按钮
    public void OnQuitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//编辑器模式下游戏退出
#else
        Application.Quit();//游戏发布后游戏退出
#endif
    }

}
