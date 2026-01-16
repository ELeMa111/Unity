using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public UIButton buttonBack;
    public GameObject RankItem;
    public UIScrollView scrollView;
    public UIScrollBar scrollBar;
    private List<RankItem> rankItems = new List<RankItem>();

    public override void Initialize()
    {
        buttonBack.onClick.Add(new EventDelegate(() =>
        {
            Hide();
        }));    
        Hide();
    }
    public override void Show()
    {
        base.Show();
        List<ItemData> list = GameDataManager.Instance.rankData.list;
        for(int i = 0; i < list.Count; i++)
        {
            if (rankItems.Count > i)
            {
                rankItems[i].Initialize(i + 1, list[i].name, list[i].time);
            }
            else
            {
                GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("UI/RankItem"));
                obj.transform.SetParent(scrollView.transform, false);
                obj.transform.localPosition = new Vector3(0, 177 - i * 80, 0);
                RankItem item = obj.GetComponent<RankItem>();
                item.Initialize(i + 1, list[i].name, list[i].time);
                rankItems.Add(item);
            }
        }
    }
}
