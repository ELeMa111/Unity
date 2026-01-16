using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_RewardType
{
    Atk,Def,HP,Speed
}
public class RewardAttribute : MonoBehaviour
{
    public E_RewardType type = E_RewardType.Atk;
    public int atkValue = 1;
    public int defValue = 1;
    public int HPValue = 10;
    public int speedValue = 10;

    public GameObject effectObject;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case E_RewardType.Atk:
                    other.gameObject.GetComponent<PlayerTank>().atk += atkValue;
                    break;
                case E_RewardType.Def:
                    other.gameObject.GetComponent<PlayerTank>().def+= defValue;
                    break;
                case E_RewardType.HP:
                    int newHP = Mathf.Min(other.gameObject.GetComponent<PlayerTank>().currentHP + HPValue, other.gameObject.GetComponent<PlayerTank>().maxHP);
                    other.gameObject.GetComponent<PlayerTank>().currentHP = newHP;
                    GamePanel.Instance.RefreshHP(other.gameObject.GetComponent<PlayerTank>().maxHP,newHP);
                    break;
                case E_RewardType.Speed:
                    other.gameObject.GetComponent<PlayerTank>().moveSpeed += speedValue;
                    break;
                default: break;
            }
            GameObject.Instantiate(effectObject, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
