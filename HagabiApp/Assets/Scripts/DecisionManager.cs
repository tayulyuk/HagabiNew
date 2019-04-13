using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionManager : MonoBehaviour {
    /// <summary>
    /// 상단 라벨 표기를 위한 하우스 번호.
    /// </summary>
    public UILabel classNum;
    /// <summary>
    /// 상단 라벨 표기를 위한 선택 번호.
    /// </summary>
    public UILabel choiceNum;
    private CircleBlank circleBlank;
    void Start()
    {
        circleBlank = GameObject.Find("CircleSelecterObject").GetComponent<CircleBlank>();
        StartCoroutine(InputInfo());
    }

    private IEnumerator InputInfo()
    {
        if (GameObject.Find("ManagerGameObject") != null)
        {
            ManagerController mc = GameObject.Find("ManagerGameObject").GetComponent <ManagerController>();
            classNum.text = mc.currentClass.ToString() + "동";
            choiceNum.text = mc.selectButtonNumber.ToString() + "번 버튼";
            circleBlank.selectNumber = mc.selectButtonNumber;

        }
        yield return new WaitForSeconds(.1f);
        StartCoroutine(InputInfo());
        yield break;
    }	
}
