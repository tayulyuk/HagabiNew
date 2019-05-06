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

    void Start()
    {
        mc = GameObject.Find("ManagerGameObject").GetComponent<ManagerController>();
        mqttManager = GameObject.Find("ManagerGameObject").GetComponent<MqttManager>();
    }

    void OnClick()
    {
        if (!mqttManager.isLoading)
        {
            mqttManager.storeOrderMassage = OrderMessage(); // 현재 눌린 버튼의 이름을 저장 ( 비교해서 패킷을 재대로 돌아왔는지 확인)

            Debug.Log("class :" + mqttManager.GetClassTopic(mc.currentClass));

            mqttManager.SendPublishButtonData(mqttManager.GetClassTopic(mc.currentClass), mqttManager.storeOrderMassage);

            //코루틴으로 재 전송 부분 
            StartCoroutine(mqttManager.ReSendToServer());

            Debug.Log("mqttRecieveMessage1 :" + mqttManager.mqttRecieveMessage);
            // 보낸 명령과 받은 명령이 같다면 작동해라.
            if (mqttManager.isSignOk || !mqttManager.isLoading)
            {
                mc.currentUpDownButtonName = transform.name;  //  다시 호출하기 위해 일단 저장.
                //Debug.Log(" tra pa :" + transform.parent.parent.transform.Find("UpStopDownShowObject").name);
                StartCoroutine(transform.parent.parent.transform.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().ArrowAction(transform.name));
            }
        }
    }

    

    /// <summary>
    ///  1~14까지.
    /// </summary>
    /// <returns></returns>
    private string OrderMessage()
    {
        string order = "";
        Debug.Log("transform name3 : " + transform.name);
        if (mc.selectButtonNumber != 13 && mc.selectButtonNumber != 14)
        {
            Debug.Log("transform name : "  + transform.name);
            //1-1up
            if (transform.name == "UpButton")
            {
                order = "pinOn " + mc.selectButtonNumber;
            }
                //stop
            else if (transform.name == "StopButton")
            {
                order = "pinIdle " + mc.selectButtonNumber;
            }
                //down
            else if (transform.name == "DownButton")
            {
                order = "pinOff " + mc.selectButtonNumber;
            }
        }
         else
        {
            Debug.Log("알수 없는 버튼입니다.");
        }
        return order;
    }
}
