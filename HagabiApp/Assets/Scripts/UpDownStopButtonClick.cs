using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 환풍기도 같이 쓰자.  
/// </summary>
public class UpDownStopButtonClick : MonoBehaviour
{
    private ManagerController mc;
    private MqttManager mqttManager;
   
    /// <summary>
    /// 선택 버튼 번호.
    /// </summary>
    private string order;
    
    void Start()
    {
        mc = GameObject.Find("ManagerGameObject").GetComponent<ManagerController>();
        mqttManager = GameObject.Find("ManagerGameObject").GetComponent<MqttManager>();
    }

    void OnClick()
    {
        order = OrderMessage();
        Debug.Log("order :" + order);
        Debug.Log("class :" + mqttManager.GetClassTopic(mc.currentClass));

        mqttManager.SendPublishButtonData(mqttManager.GetClassTopic(mc.currentClass),order);

        StartCoroutine(ArrowAction());
    }

    private IEnumerator ArrowAction()
    {
        yield return new WaitForSeconds(.3f);
        Debug.Log("order :" + order);
        Debug.Log("mqttRecieveMessage :" + mqttManager.mqttRecieveMessage);
        //보낸 신호와 받은 신호가 같다면 다음 동작을 한다.
        if (mqttManager.mqttRecieveMessage.Trim() == order.Trim() || order.Trim() != String.Empty || mqttManager.mqttRecieveMessage.Trim() != String.Empty || order.Trim() != "" || mqttManager.mqttRecieveMessage.Trim() != "")
        {
            //HagabiDecision 씬의 버튼들.
            if (transform.name == "UpButton")
            {
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().AllOffArrowfBlank();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().SelectStopCorouine();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().StartArrow(1); //1.up
            }
            if (transform.name == "StopButton")
            {
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().AllOffArrowfBlank();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().SelectStopCorouine();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().StartArrow(2); //2.stop
            }
            if (transform.name == "DownButton")
            {
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().AllOffArrowfBlank();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().SelectStopCorouine();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().StartArrow(3); //3.down
            }

            //환풍기.
            if (transform.name == "PropellerOnButton")
               GetComponent<PropellerShowManager>().OnPropeller();
            if (transform.name == "PropellerOffButton")
                GetComponent<PropellerShowManager>().OffPropeller();
        }
        else
        {
            Debug.Log("두 명령 불일치.");
        }
    }

    /// <summary>
    /// 아두이노에서 10단위 이상만 검색할수 있다 그래서 1부터 시작하지 않고 11 부터 시작했다. %
    /// </summary>
    /// <returns></returns>
    string OrderMessage()
    {
       string order = "";
        ///1-1up
        if (transform.name == "UpButton")
        {
            if (mc.selectButtonNumber != 13 && mc.selectButtonNumber != 14)
                order = "pinOn " + mc.selectButtonNumber;
        }
        //stop
        if (transform.name == "StopButton")
        {
            if (mc.selectButtonNumber != 13 && mc.selectButtonNumber != 14)
                order = "pinIdle " + mc.selectButtonNumber;
        }
        //down
        if (transform.name == "DownButton")
        {
             if (mc.selectButtonNumber != 13 && mc.selectButtonNumber != 14)
                    order = "pinOff " + mc.selectButtonNumber;
        }



        if (transform.name == "PropellerOnButton")
        {
            if (mc.selectButtonNumber == 13 && mc.selectButtonNumber == 14)
                order = "pinOn " + mc.selectButtonNumber;
        }
        if (transform.name == "PropellerOffButton")
        {
            if (mc.selectButtonNumber == 13 && mc.selectButtonNumber == 14)
                order = "pinOff " + mc.selectButtonNumber;
        }
        else
        {
            Debug.Log("알수 없는 버튼입니다.");
        }
        return order;
    }
}
