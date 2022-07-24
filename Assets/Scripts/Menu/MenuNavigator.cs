using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuNavigator : MonoBehaviour
{
    public AudioSource confirmAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTryAgainBtn()
    {
        confirmAudio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnExitToMenuBtn()
    {
        confirmAudio.Play();
        SceneManager.LoadScene("MenuScene");
    }
    public void CloseUI(GameObject o) {
        GameManager.sTheGlobalBehavior.Resume("pause");
        o.SetActive(false);
    }
}
