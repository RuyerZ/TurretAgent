using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private int currentSlide = 0;
    private float minBuyGold = 5f;
    private bool isActive = false;
    private bool trigger = false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.sTheGlobalBehavior.Pause();
        transform.Find("Slide" + currentSlide.ToString()).gameObject.SetActive(true);
        transform.Find("Background").gameObject.SetActive(true);
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && Input.GetMouseButtonDown(0)) {
            NextSlide();
        }
        if (GameManager.sTheGlobalBehavior.Gold >= minBuyGold && !trigger) {
            GameManager.sTheGlobalBehavior.Pause();
            transform.Find("Slide" + currentSlide.ToString()).gameObject.SetActive(true);
            transform.Find("Background").gameObject.SetActive(true);
            trigger = true;
            isActive = true;
        }
    }

    void NextSlide() {
        transform.Find("Slide" + currentSlide.ToString()).gameObject.SetActive(false);

        if (currentSlide >= 8) {
            GameManager.sTheGlobalBehavior.Resume();
            isActive = false;
            transform.Find("Background").gameObject.SetActive(false);
            currentSlide++;
            return;
        }

        currentSlide++;
        transform.Find("Slide" + currentSlide.ToString()).gameObject.SetActive(true);
    }
}
