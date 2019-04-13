using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;


public class TcpSendMessageManager : MonoBehaviour
{
    public string urlClass1 = "";
    public string urlClass2 = "";

    TcpClient tcpClient = new TcpClient();
    public NetworkStream networkStream;
    public string host = "";
    public int Port = 8090;
    public string tcpState = "";
    public bool isSend = false;
    StreamWriter streamWriter;
    StreamReader streamReader;

    //error 창을 만들자.
    public GameObject errorObject;

    void Start()
    {
        if (GameObject.Find("ManagerGameObject") != null)
        {
            urlClass1 = GameObject.Find("ManagerGameObject").GetComponent<HttpGetIpManager>().getIp1;
            urlClass2 = GameObject.Find("ManagerGameObject").GetComponent<HttpGetIpManager>().getIp2;
        }
    }
    internal IEnumerator TcpHttpSendMessageMethod(int classNumber , int orderNum)
    {
        string btnNum = "+IPD,pin=" + orderNum;
        host = GetURL(classNumber);
        try
        {
            if (!isSend)
            {
                isSend = true;

                tcpClient = new TcpClient(host, Port);
                networkStream = tcpClient.GetStream();
                streamWriter = new StreamWriter(networkStream);
                streamReader = new StreamReader(networkStream);

                tcpState = "connected";
                Debug.Log("connected state : " + tcpState);

                if (tcpState == "connected")
                {
                    streamWriter.Write(btnNum);
                    streamWriter.Flush();
                }
                string getMessage = "";
                while ((getMessage = streamReader.ReadLine()) != null)
                {
                    Debug.Log(getMessage);
                    tcpState += "     /  get:" + getMessage;
                }

                streamReader.DiscardBufferedData();
            }

            streamWriter.Close();
            streamReader.Close();
            networkStream.Close();
            tcpClient.Close();
            isSend = false;
        }
        catch(Exception e)
        {
            tcpState = "Not Connected";
            tcpState += "  e-message:"; 
            tcpState += e.Message;
            Debug.Log(tcpState);
            if (errorObject != null)
            {
                errorObject.SetActive(true);
                errorObject.GetComponentInChildren<UILabel>().text += e.Message;
            }
        }
        yield break;
    }

    private string GetURL(int classNum)
    {
        string valueUrl = "";
        if (classNum == 1)
            valueUrl =  urlClass1;
        if (classNum == 2)
            valueUrl = urlClass2;
        return valueUrl;
    }
}
