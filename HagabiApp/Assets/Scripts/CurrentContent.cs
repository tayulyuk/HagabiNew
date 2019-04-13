using UnityEngine;
using System.Collections;

public class CurrentContent : MonoBehaviour {

    void OnEnable()
    {
        if (transform.name == "Content 1")
            GameObject.Find("Window").GetComponent<ControlManager>().titleClassShowLabel.text = "현재 [00ff00]1[-]동";
        if (transform.name == "Content 2")
            GameObject.Find("Window").GetComponent<ControlManager>().titleClassShowLabel.text = "현재 [00ff00]2[-]동";
        if (transform.name == "Content 3")
            GameObject.Find("Window").GetComponent<ControlManager>().titleClassShowLabel.text = "현재 [00ff00]3[-]동";
    }

}
