﻿using System.Text;
using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;

public class MqttManager : MonoBehaviour
{
    private MqttClient client;
    public string tempVal;
    public string humiVal;
    public string mqttRecieveMessage;

    private string class1publish = "Hagabi/test1";  //1동 하우스
    private string class2publish = "Hagabi/test2";  //2동 하우스
    private string class3publish = "Hagabi/test3";  //3동 하우스
    private string subscribeReciveTempHumiMessage = "ModelTempHumi/result";
    private string resultMessage = "Hagabi/result";
    private string mqttAddress = "119.205.235.214";

    //public GameObject loadingObject;
    public bool isLoading; // 현제 통신 로딩 중인가?.
    public bool isOne; // 메세지 한번 받을때 마다 라벨에 값을 입력합니다.   라벨값은 메인쓰레드에서만 입력하라네요. 
    public string storeOrderMassage;
    public bool isSignOk; // 보낸 명령 메시지와 받은 메시지가 같으면  true .
    public float time;

    void Start()
    {
        // create client instance 
        client = new MqttClient(IPAddress.Parse(mqttAddress), 1883, false, null);

        // register to message received 
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

       string clientId = Guid.NewGuid().ToString();
        //string clientId = "siheung_namu_moter";
        client.Connect(clientId);

        // subscribe to the topic "/home/temperature" with QoS 2 
        client.Subscribe(new string[] { subscribeReciveTempHumiMessage }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        client.Subscribe(new string[] { resultMessage }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }
   
    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {  
    
        Debug.Log(e.Topic + " : " + System.Text.Encoding.UTF8.GetString(e.Message));

        if (e.Topic == subscribeReciveTempHumiMessage)
            TempHumiMessageParsing(System.Text.Encoding.UTF8.GetString(e.Message));
        if (e.Topic == resultMessage)
            mqttRecieveMessage = Encoding.UTF8.GetString(e.Message).Trim();
    }
   

    /// <summary>
    /// 버튼 클릭 현재 정보를 전달한다.
    /// </summary>
    /// <param name="topic">버튼 주소</param>
    public void SendPublishButtonData(string topic,string sendData)
    {
        client.Publish( topic, System.Text.Encoding.Default.GetBytes(sendData));     
    }

    /// <summary>
    /// 서버로 부터 받은 정보를 각 변수에 저장한다.
    /// </summary>
    /// <param name="getMessage">서버로 부터 받은 정보.</param>
    private void TempHumiMessageParsing(string getMessage)
    {
        tempVal = GetParserString(getMessage, "|Temp=", "0|") + " ℃";
        humiVal = GetParserString(getMessage, "Humi=", "0|") +" %";
    }

    /// <summary>
    /// 서버로 부터 받은 정보를 나눈다.
    /// </summary>
    /// <param name="message">서버 data</param>
    /// <param name="startSearch">시작문구</param>
    /// <param name="endSearch">끝 문구</param>
    /// <returns></returns>
    public string GetParserString(string message ,string startSearch,string endSearch)
    {
        string getValue = "";
        string search = "";

        search = startSearch;        
    
        int p = message.IndexOf(search);
        if (p >= 0)
        {
            // move forward to the value
            int start = p + search.Length;
            // now find the end by searching for the next closing tag starting at the start position, 
            // limiting the forward search to the max value length
            int end = 0;
            end = message.IndexOf(endSearch, start);           

            if (end >= 0)
            {
                // pull out the substring
                string v = message.Substring(start, end - start);
                // finally parse into a float
                // float value = float.Parse(v);
                // Debug.Log("1classTemp Value = " + value);
              
               getValue = v;                
            }
            else
            {
                Debug.Log("Bad html - closing tag not found");
                getValue = "text error";
            }
        }
        return getValue;
    }

    public string GetClassTopic(int currentClassNum)
    {
        string topic = "";
        if (1 == currentClassNum)
            topic = class1publish;
        if (2 == currentClassNum)
            topic = class2publish;
        if (3 == currentClassNum)
            topic = class3publish;
        return topic;
    }

    /// <summary>
    ///message type :  pinon 3
    /// </summary>
    /// <param name="buttonName"></param>
    /// <returns></returns>
    internal IEnumerator ReSendToServer()
    {
        yield return new WaitForSeconds(.1f);

        if (mqttRecieveMessage.Trim() != storeOrderMassage.Trim()) //|| !String.IsNullOrEmpty(mqttRecieveMessage)
        {
            isSignOk = false; // true 면  화살표 표시 버튼이 눌러진다 . false면 화살표 안눌러진다.
            isLoading = true;
            Debug.Log("Message ReSend To Server 1");
            SendPublishButtonData(GetClassTopic(GetComponent<ManagerController>().currentClass), storeOrderMassage);

            if (time < 3)
                time += .1f;
            else
            {
                isLoading = false;
                time = 0;
                yield return new WaitForSeconds(.1f);
                yield break;
            }
            Debug.Log("Message ReSend To Server 2");
            StartCoroutine(ReSendToServer());
        }
        else // 두가지 조건이 같다면   기능 정지.
        {
            isSignOk = true; //화살표는 켜고.
            isLoading = false; //로딩은 끄고
            time = 0;
            yield break;
        }
        
    }
}
