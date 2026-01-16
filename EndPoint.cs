using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //”Œœ∑Ω· ¯
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            WinPanel.Instance.ShoworHide(true);
        }
    }
}
