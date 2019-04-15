using System.Linq;
using UnityEngine;
using System.Collections;

public class CurrentContent : MonoBehaviour
{
    public UIToggle uIToggle1;
    public UIToggle uIToggle2;
    public UIToggle uIToggle3;

    private void OnEnable()
    {
        uIToggle1 = uIToggle1.GetComponent<UIToggle>();
        uIToggle2 = uIToggle2.GetComponent<UIToggle>();
        uIToggle3 = uIToggle3.GetComponent<UIToggle>();

        GameObject go = GameObject.Find("ManagerGameObject");
        int classNow = go.GetComponent<ManagerController>().currentClass;
       // Debug.Log("현재 크래스:" + go.GetComponent<ManagerController>().currentClass);

        if (classNow == 0)
        {
            uIToggle1.startsActive = true;
            GameObject.Find("Window").GetComponent<ControlManager>().titleClassShowLabel.text = "현재 [00ff00]1[-]동";
            Debug.Log("o 번 실행 된거다.");
        }
        else if (classNow == 1)
        {
            uIToggle1.startsActive = true;
            GameObject.Find("Window").GetComponent<ControlManager>().titleClassShowLabel.text = "현재 [00ff00]1[-]동";
        }
        else if (classNow == 2)
        {
            uIToggle2.startsActive = true;
            GameObject.Find("Window").GetComponent<ControlManager>().titleClassShowLabel.text = "현재 [00ff00]2[-]동";
        }
        else if (classNow == 3)
        {
            uIToggle3.startsActive = true;
            GameObject.Find("Window").GetComponent<ControlManager>().titleClassShowLabel.text = "현재 [00ff00]3[-]동";
        }
        else
        {
            Debug.Log("뭐지.......?");
        }
    }
}
