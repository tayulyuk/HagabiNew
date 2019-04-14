using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPropellerControler : MonoBehaviour {

    private ManagerController mc;
    private MqttManager mqttManager;
    private PropellerShowManager propellerShowManager;

    /// <summary>
    /// 선택 버튼 번호.
    /// </summary>
    private string order;

    void Start()
    {
        mc = GameObject.Find("ManagerGameObject").GetComponent<ManagerController>();
        mqttManager = GameObject.Find("ManagerGameObject").GetComponent<MqttManager>();
        propellerShowManager = GameObject.Find("ScrewGameObject").GetComponent<PropellerShowManager>();
    }

    void OnClick()
    {
        order = OrderMessage();
        Debug.Log("order :" + order);
        Debug.Log("class :" + mqttManager.GetClassTopic(mc.currentClass));

        if (!String.IsNullOrEmpty(order))
        {
            mc.checkButtonNum = order;

            mqttManager.SendPublishButtonData(mqttManager.GetClassTopic(mc.currentClass), order);


            Debug.Log("mqttRecieveMessage1 :" + mqttManager.mqttRecieveMessage);

            StartCoroutine(ArrowAction());
        }
    }

    private IEnumerator ArrowAction()
    {
        yield return new WaitForSeconds(.3f);
       
        //보낸 신호와 받은 신호가 같다면 다음 동작을 한다.
        if (mqttManager.mqttRecieveMessage == order)
        {
            //환풍기.
            if (transform.name == "PropellerOnButton")
            {
                propellerShowManager.OnPropeller();
            }
            else if (transform.name == "propellerOffButton")
            {
               propellerShowManager.OffPropeller();
            }
        }
        else
        {
            Debug.Log("뭔놈에 버튼이여?");
        }
    }

    /// <summary>
    ///  1~14까지.
    /// </summary>
    /// <returns></returns>
    private string OrderMessage()
    {
        string order = "";
        if (transform.name == "PropellerOffButton")
        {
            order = "pinOff " + mc.selectButtonNumber;
        }
        else if (transform.name == "PropellerOnButton")
        {
            order = "pinOn " + mc.selectButtonNumber;
        }
        else
        {
            Debug.Log("알수 없는 버튼입니다.");
            order = "";
        }
        return order;
    }
}
