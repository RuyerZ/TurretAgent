using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface ItemInterface
{
    bool Activate(); // 切换到这个道具时的函数入口，返回结果为是否切换成功
    bool Deactivate(); // 从这个道具切换走时的函数入口，返回结果为是否切换成功（如果是炮台放在不合法位置可能切换失败）
    void Fire();
    Sprite getIcon();
    int getItemCount();
}

public abstract class ItemBase : MonoBehaviour, ItemInterface
{
    public int itemCount = 0;
    public Transform firePoint;
    public Sprite icon;

    public bool Activate()
    {
        gameObject.SetActive(true);
        return true;
    }
    public bool Deactivate()
    {
        gameObject.SetActive(false);
        return true;
    }
    public Sprite getIcon()
    {
        return icon;
    }
    void Update()
    {
        //COMMENTED TO TEST， MIGHT NOT BE NEEDED

        // //sprite manipulation for visuals
        // float angle = transform.parent.rotation.eulerAngles.z;
        // Vector3 scale = transform.localScale;
        // Vector3 pos = transform.position;
        // //flip sprite
        // if (0f <= angle && angle <= 180f && transform.localScale.x > 0f) {
        //      scale.x *= -1;
        // }
        // if (180f < angle && angle <= 360f && transform.localScale.x < 0f) {
        //      scale.x *= -1;
        // }
        // //move layer
        // if (0f <= angle && angle < 90f || 270f < angle && angle <= 360f) {
        //     pos.z = 0.001f;
        // } else {
        //     pos.z = -0.001f;
        // }
        // transform.localScale = scale;
        // transform.position = pos;
    }
    public int getItemCount() { return itemCount;}
    public virtual void Fire() {}
}
