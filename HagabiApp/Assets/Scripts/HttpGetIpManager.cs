using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpGetIpManager : MonoBehaviour
{

    private string url = "http://www.hagabi.com/hagabiManager.jsp";
    public string getIp1,getIp2 = "";
	void Start ()
	{
	    StartCoroutine(HttpGetIp(url));
	}

    internal IEnumerator HttpGetIp(string url)
    {
        //string btnNum = "?pin=" + orderNum.ToString();
        UnityWebRequest getRequest = UnityWebRequest.Get(url);

        yield return getRequest.SendWebRequest();

        if (getRequest.isHttpError || getRequest.isNetworkError)
        {
            Debug.Log(getRequest.error);
        }
        else
        {
            //Debug.Log(getRequest.downloadHandler.text);
            getIp1 = GetIp(getRequest,1).Trim();
            getIp2 = GetIp(getRequest, 2).Trim();
            //Debug.Log(getIp);
        }
    }

    private string GetIp(UnityWebRequest www,int selectClass)
    {
        string getValue = "";
        // check for errors 
        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
        {
            string input = www.downloadHandler.text;
            string search = "";
            if (selectClass == 1)
            {
                search = "<h2 id=\"1classIP\">";
            }
            if (selectClass == 2)
            {
                search = "<h2 id=\"2classIP\">";
            }
            int p = input.IndexOf(search);
            if (p >= 0)
            {
                // move forward to the value
                int start = p + search.Length;
                // now find the end by searching for the next closing tag starting at the start position, 
                // limiting the forward search to the max value length
                int end = input.IndexOf("</h2>", start);
                if (end >= 0)
                {
                    // pull out the substring
                    string v = input.Substring(start, end - start);
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

        }

        return getValue;
    }
}
