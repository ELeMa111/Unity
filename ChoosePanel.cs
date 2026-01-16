using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    public UIButton buttonBack;
    public UIButton buttonLeft;
    public UIButton buttonRight;
    public UIButton buttonStart;
    public Transform CharacterPos;
    public List<GameObject> HPObjs;
    public List<GameObject> SpeedObjs;
    public List<GameObject> VolumeObjs;
    public GameObject airPlane;
    private float time = 0;
    private bool isSel = false;
    public Camera UICamera;

    public override void Initialize()
    {
        buttonStart.onClick.Add(new EventDelegate(() =>
        {
            SceneManager.LoadScene("GameScene");
        }));
        buttonBack.onClick.Add(new EventDelegate(() => 
        {
            BeginPanel.Instance.Show();
            Hide();
        }));
        buttonLeft.onClick.Add(new EventDelegate(() =>
        {
            GameDataManager.Instance.nowSelHeroIndex--;
            if (GameDataManager.Instance.nowSelHeroIndex < 0) 
                GameDataManager.Instance.nowSelHeroIndex = GameDataManager.Instance.roleData.roleList.Count - 1;
            ChangeNowHero();
        }));
        buttonRight.onClick.Add(new EventDelegate(() =>
        {
            GameDataManager.Instance.nowSelHeroIndex++;
            if (GameDataManager.Instance.nowSelHeroIndex > GameDataManager.Instance.roleData.roleList.Count - 1)
                GameDataManager.Instance.nowSelHeroIndex = 0;
            ChangeNowHero();
        }));
        Hide();
    }
    private void ChangeNowHero()
    {
        RoleInfo info = GameDataManager.Instance.GetNowSelHeroInfo();
        //Debug.Log(Resources.Load<GameObject>(info.resName));
        //更新模型/属性
        //先删除上一次的飞机模型
        DestroyObj();
        airPlane = Instantiate(Resources.Load<GameObject>(info.resName));
        airPlane.transform.SetParent(CharacterPos, false);
        airPlane.transform.localPosition = Vector3.zero;
        airPlane.transform.localRotation = Quaternion.identity;
        airPlane.transform.localScale = info.scale * Vector3.one;
        airPlane.layer = LayerMask.NameToLayer("UI");

        for(int i = 0; i < 10; i++)
        {
            HPObjs[i].SetActive(i < info.HP ? true : false);
            SpeedObjs[i].SetActive(i < info.speed ? true : false);
            VolumeObjs[i].SetActive(i < info.volume ? true : false);
        }
    }
    private void DestroyObj()
    {
        if (airPlane != null)
        {
            Destroy(airPlane);
            airPlane = null;
        }
    }
    public override void Show()
    {
        base.Show();
        GameDataManager.Instance.nowSelHeroIndex = 0;
        ChangeNowHero();
    }
    public override void Hide()
    {
        DestroyObj();
        base.Hide();
    }
    private void Update()
    {
        time += Time.deltaTime;
        //2秒为一个周期
        //最大偏移值为0.2
        float deltaY = 0.2f * Mathf.Sin(time / 2 * 2 * Mathf.PI);
        CharacterPos.Translate(Vector3.up * deltaY * Time.deltaTime, Space.World); ;

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("按下鼠标左键");
            Ray ray = UICamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 10000, 1 << LayerMask.NameToLayer("UI"), QueryTriggerInteraction.UseGlobal))
            {
                //Debug.Log("检测到对象"+hit.transform.gameObject.name);
                if(hit.collider.transform == CharacterPos)
                {
                    //Debug.Log("检测到目标对象");
                    isSel = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0)) { isSel = false; }
        if (isSel)
        {
            float degree = Input.GetAxis("Mouse X") * Time.deltaTime * 1000;
            Vector3 upv = CharacterPos.InverseTransformDirection(Vector3.up);
            Quaternion quaternion = Quaternion.AngleAxis(-degree,upv);
            CharacterPos.rotation *= quaternion;
        }
    }
}
