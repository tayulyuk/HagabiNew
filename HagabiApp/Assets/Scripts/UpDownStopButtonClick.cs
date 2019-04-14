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
        Debug.Log("transform name 1 : " + transform.name);
        order = OrderMessage();
        Debug.Log("order :" + order);
        Debug.Log("class :" + mqttManager.GetClassTopic(mc.currentClass));

        mqttManager.SendPublishButtonData(mqttManager.GetClassTopic(mc.currentClass),order);

        
        Debug.Log("mqttRecieveMessage1 :" + mqttManager.mqttRecieveMessage);

       StartCoroutine(ArrowAction());
    }

    private IEnumerator ArrowAction()
    {
        yield return new WaitForSeconds(.5f);
        
        if(mqttManager != null)
            Debug.Log("not null  select num :" + mc.selectButtonNumber );
        Debug.Log("mqttRecieveMessage 2:" + mqttManager.mqttRecieveMessage);
       
        //보낸 신호와 받은 신호가 같다면 다음 동작을 한다.
        if (mqttManager.mqttRecieveMessage.Trim() == order.Trim() || !String.IsNullOrEmpty(mqttManager.mqttRecieveMessage) )
        {
            //HagabiDecision 씬의 버튼들.
            if (transform.name == "UpButton")
            {
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().AllOffArrowfBlank();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().SelectStopCorouine();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().StartArrow(1); //1.up
            }
            else if (transform.name == "StopButton")
            {
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().AllOffArrowfBlank();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().SelectStopCorouine();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().StartArrow(2); //2.stop
            }
            else if (transform.name == "DownButton")
            {
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().AllOffArrowfBlank();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().SelectStopCorouine();
                GameObject.Find("UpStopDownShowObject").GetComponent<ArrowButtonController>().StartArrow(3); //3.down
            }
        }
        else
        {
            Debug.Log("두 명령 불일치.");
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
