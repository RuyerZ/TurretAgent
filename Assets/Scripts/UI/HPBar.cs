using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private float mRate;
    public Image mBarImage;
    public Gradient mGradient;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(mBarImage != null);

        mRate = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        mBarImage.fillAmount = mRate;
        if (mGradient != null)
            mBarImage.color = mGradient.Evaluate(mRate);
    }

    public void Set(float x)
    {
        mRate = Mathf.Clamp(x, 0.0f, 1.0f);
    }
}
