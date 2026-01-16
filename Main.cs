using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        //
        RoleInfo info = GameDataManager.Instance.GetNowSelHeroInfo();
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.resName));
        PlayerObject playerObject = obj.AddComponent<PlayerObject>();
        playerObject.speed = info.speed * 20;
        playerObject.rotateSpeed = 20;
        playerObject.maxHP = 10;
        playerObject.currentHP = info.HP;
        GamePanel.Instance.ChangeHP(info.HP);
    }
}
