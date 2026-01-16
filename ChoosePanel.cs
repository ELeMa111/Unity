using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePanel : BasePanel
{
    public ScrollRect svVertical;
    public Transform contentV;
    public ScrollRect svService;
    public Transform contentG;
    //public List<Button> buttonV;
    public Button buttonT;
    public Text text;
    //public List<Button> buttonG;
    public GameObject buttonObj;

    protected new void Awake()
    {
        base.Awake();
        buttonObj.SetActive(false);
        buttonObj.GetComponent<Button>().onClick.AddListener(() =>
        {
            //
            contentV.localPosition = contentV.localPosition + new Vector3(0, 20, 0);
        });
        RefreshButtonT();
        RefreshVItems();
        RefreshGItems(1);
        RefreshLabel(1);
    }
    private void RefreshVItems()
    {
        int num = GameDataManager.Instance.serverData.serverList.Count;
        Debug.Log(num);
        num = num / 10 + 1;
        for(int i = 1; i <= num; i++)
        {
            int currentI = i;
            GameObject item = GameObject.Instantiate(GameDataManager.Instance.GetVitem(), contentV, false);
            item.transform.Find("Text").GetComponent<Text>().text = (1+(10*(i-1))).ToString() + " - "+ (i*10).ToString() + " 区";
            //buttonV.Add(item.GetComponent<Button>());
            Button thisButton = item.GetComponent<Button>();
            thisButton.onClick.AddListener(() => 
            {
                RefreshGItems(currentI);
                RefreshLabel(currentI);
            });
        }
        if((contentV.transform as RectTransform).rect.height>(svVertical.transform.Find("Viewport").transform as RectTransform).rect.height)
        {
            buttonObj.SetActive(true);
        }
    }
    private void RefreshGItems(int j)   //j 为 1 就显示 1-10
    {
        //Debug.Log(j);
        for(int i = 0; i < contentG.childCount; i++)
        {
            Destroy(contentG.GetChild(i).gameObject);
        }
        for(int i = 1+(j-1)*10; i <= (j-1)*10 + 10; i++)
        {
            if (i <= GameDataManager.Instance.serverData.serverList.Count)
            {
                GameObject thisItem = GameObject.Instantiate(GameDataManager.Instance.GetGitem(),contentG,false);
                ServerItem thisData = GameDataManager.Instance.serverData.serverList[i-1];
                thisItem.transform.Find("Name").GetComponent<Text>().text = thisData.name;
                thisItem.transform.Find("NO").GetComponent<Text>().text = i.ToString() + "区";
                thisItem.transform.Find("ImageNew").gameObject.SetActive(thisData.isNew);
                Sprite sprite = null;
                switch (thisData.serverType)
                {
                    case 1:
                        sprite = Resources.Load<Sprite>("type1");
                        break;
                    case 2:
                        sprite = Resources.Load<Sprite>("type2");
                        break;
                    case 3:
                        sprite = Resources.Load<Sprite>("type3");
                        break;
                    case 4:
                        sprite = Resources.Load<Sprite>("type4");
                        break;
                }
                thisItem.transform.Find("ImageType").GetComponent<Image>().sprite = sprite;
                int currentI = i;
                thisItem.transform.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameDataManager.Instance.SetServer(currentI);
                    UIManager.Instance.Show<ServerPanel>();
                    UIManager.Instance.Hide<ChoosePanel>();
                });
            }
        }
    }
    private void RefreshLabel(int j)    //j 为 行号
    {
        // (j-1)*10 + 1 
        // (j-1)*10 + 10
        //serverData.serverList.Count
        string text = (1 + 10 * (j - 1)).ToString() + " - " + Mathf.Min((10 + 10 * (j - 1)), GameDataManager.Instance.serverData.serverList.Count).ToString() + " 区";
        this.text.text = text;
    }
    private void RefreshButtonT()
    {
        ServerItem thisServer = GameDataManager.Instance.thisServer;
        if (thisServer == null) return;
        buttonT.transform.Find("Text").GetComponent<Text>().text = thisServer.name;
        buttonT.onClick.AddListener(() => 
        {
            UIManager.Instance.Show<ServerPanel>();
            UIManager.Instance.Hide<ChoosePanel>();
        });
    }
}
