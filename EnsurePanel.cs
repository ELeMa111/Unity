using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnsurePanel : BasePanel<EnsurePanel>
{
    [SerializeField]
    private MyButton buttonExit;
    [SerializeField]
    private MyButton buttonContinue;

    private new void Awake()
    {
        base.Awake();
        //≥ı ºªØ
        try
        {
            if (buttonExit == null) { buttonExit = this.transform.Find("MyWindow/ButtonExit").GetComponent<MyButton>(); }
            if (buttonContinue == null) { buttonContinue = this.transform.Find("MyWindow/ButtonContinue").GetComponent<MyButton>(); }
        }
        catch { }
        finally { }
        buttonExit.ButtonAction += ExitAction;
        buttonContinue.ButtonAction += ContinueAction;

        this.ShoworHide(false);
    } 
    private void ExitAction()
    {
        //Application.Quit();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("BeginScene");
    }
    private void ContinueAction()
    {
        Time.timeScale = 1;
        GamePanel.Instance.ShoworHide(true);
        this.ShoworHide(false);
    }
}
