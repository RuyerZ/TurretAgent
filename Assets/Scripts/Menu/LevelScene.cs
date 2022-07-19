using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScene : MonoBehaviour
{
    [SerializeField]
    private int _MakeLevel = 1;

    [SerializeField]
    private Transform _LevelNode = null;

    private Button[] _LevelList;

    [SerializeField]
    private string _ScenePrefixName = "mission_";

    private void Awake()
    {
        _LevelList = _LevelNode.GetComponentsInChildren<Button>();
        for (int i = 0; i < _LevelList.Length; i++)
        {
            int index = i + 1;
            _LevelList[i].onClick.AddListener(() => OnLevelBtn(index));

            if (i < _MakeLevel)
            {
                _LevelList[i].GetComponentInChildren<Text>().text = (i + 1).ToString();
            }
            else
            {
                _LevelList[i].interactable = false;
            }
        }
    }

    //  ¹Ø¿¨°´Å¥
    public void OnLevelBtn(int index)
    {
        //Debug.Log(_ScenePrefixName + index);
        SceneManager.LoadScene(_ScenePrefixName + index);
    }


    private void OnDestroy()
    {
        for (int i = 0; i < _LevelList.Length; i++)
        {
            _LevelList[i].onClick.RemoveListener(() => OnLevelBtn(i + 1));
        }
    }

}
