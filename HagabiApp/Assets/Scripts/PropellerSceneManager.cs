using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSceneManager : MonoBehaviour {
    /// <summary>
    /// 상단 라벨 표기를 위한 하우스 번호.
    /// </summary>
    public UILabel classNum;
    /// <summary>
    /// 상단 라벨 표기를 위한 선택 번호.
    /// </summary>
    public UILabel choiceNum;
    private CircleBlank circleBlank;
    private MqttManager mqttManager;
    public GameObject loadingSprite;
    void Start()
    {
        mqttManager = GameObject.Find("ManagerGameObject").GetComponent<MqttManager>();
        circleBlank = GameObject.Find("CircleSelecterObject").GetComponent<CircleBlank>();
        StartCoroutine(InputInfo());
    }

    void Update()
    {
        loadingSprite.SetActive(mqttManager.isLoading);
    }

    private IEnumerator InputInfo()
    {
        if (GameObject.Find("ManagerGameObject") != null)
        {
            ManagerController mc = GameObject.Find("ManagerGameObject").GetComponent<ManagerController>();
            classNum.text = "[00ff00]" +mc.currentClass +"[-]" + "동";
            choiceNum.text = "[00ff00]" + mc.selectButtonNumber + "[-]" + "번 버튼";
            circleBlank.selectNumber = mc.selectButtonNumber;

        }
        yield return new WaitForSeconds(.1f);
        StartCoroutine(InputInfo());
        yield break;
    }	
}
