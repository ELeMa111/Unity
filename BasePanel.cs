using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class BasePanel : MonoBehaviour
{
    [Header("面板的CanvasGroup组件")]
    public CanvasGroup cg;
    [Header("面板淡入淡出的速度")]
    public float fadeInSpeed = 1f;
    [Header("是否需要淡入淡出")]
    public bool needFadeIn = true;
    public bool needFadeOut = false;

    public UnityAction OnPanelDestroy;

    protected void Awake()
    {
        //获取CanvasGroup组件
        cg = this.GetComponent<CanvasGroup>();
        if (cg == null) { cg = this.AddComponent<CanvasGroup>(); }
        cg.alpha = 0f;
    }
    private void Update()
    {
        FadeInOrOut();
    }
    public virtual void Show()
    {
        //Debug.Log("enter show");
        Transform Canvas = GameObject.Find("Canvas").transform;
        GameObject thisPanel = GameObject.Instantiate(this.gameObject,Canvas,false);
        
    }
    public virtual void Hide()
    {
        needFadeOut = true;
    }
    private void FadeInOrOut()
    {
        //判断是否需要淡入
        if (needFadeIn)
        {
            cg.alpha = Mathf.Min(1f, cg.alpha + Time.deltaTime * fadeInSpeed);
            if (cg.alpha >= 1)
            {
                needFadeIn = false;
            }
        }
        //判断是否需要淡出
        if (needFadeOut)
        {
            Destroy(this.gameObject,1/fadeInSpeed);
            //提前订阅Destroy
            //防止页面来回跳转导致的引用丢失，使界面以半透明的形式存在且无法被删除
            cg.alpha = Mathf.Max(0, cg.alpha - Time.deltaTime * fadeInSpeed);
            if (cg.alpha <= 0)
            {
                needFadeOut = false;
                //处理完全消失的其他逻辑
                //Destroy(this.gameObject);
                OnPanelDestroy?.Invoke();
            }
        }
    }
}
