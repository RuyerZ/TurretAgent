using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponentInChildren<Text>().text = "$" + GameManager.sTheGlobalBehavior.GetGold().ToString("#.#");
    }
}
