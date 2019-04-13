using System;
using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.Text; 
using System.IO; 
using System.Net;
using UnityEngine.Networking;

/// <summary>
/// 웹의 정보를 가저오는것이 임무지만
/// 3동의 mqtt의 정보를 가져오기 위해 일부를 삽입했다.
/// </summary>
public class WWWWiki : MonoBehaviour
{   
    public UILabel class1TempValueLabel;
    public UILabel class1HumiValueLabel;
    public UILabel class2TempValueLabel;
    public UILabel class2HumiValueLabel;
    public UILabel class3TempValueLabel;
    public UILabel class3HumiValueLabel;
    public string url = "www.hagabi.com/tempAndHumi.jsp";
    // Use this for initialization 
    private void Start()
    {
        StartCoroutine(Get(url));
    }


    public IEnumerator Get(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            class1TempValueLabel.text = Get1ClassTemp(www).ToString() + " ℃";
            class1HumiValueLabel.text = Get1ClassHumi(www).ToString() + " %"; ;
            class2TempValueLabel.text = Get2ClassTemp(www).ToString() + " ℃"; ;
            class2HumiValueLabel.text = Get2ClassHumi(www).ToString() + " %";          
        }
        //mqtt 정보 얻어오기.
        if (GameObject.Find("ManagerGameObject").GetComponent<MqttManager>() != null)
        {
            class3TempValueLabel.text = GameObject.Find("ManagerGameObject").GetComponent<MqttManager>().tempVal;
            class3HumiValueLabel.text = GameObject.Find("ManagerGameObject").GetComponent<MqttManager>().humiVal;    
        }

        yield return new WaitForSeconds(5);
       StartCoroutine(Get(url));
    }
    /*
    public WWW POST(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
        return www;
    }
    */

    private IEnumerator WaitForRequest(UnityWebRequest www)
    {
        yield return www;
       class1TempValueLabel.text = Get1ClassTemp(www).ToString() + " ℃";
       class1HumiValueLabel.text = Get1ClassHumi(www).ToString() + " %"; 
       class2TempValueLabel.text = Get2ClassTemp(www).ToString() + " ℃"; 
       class2HumiValueLabel.text = Get2ClassHumi(www).ToString() + " %";
        yield return new WaitForSeconds(5);
        //Get(url);
    }
    private int Get2ClassHumi(UnityWebRequest www)
    {
        int getValue = 0;
        // check for errors 
         if(www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
        {
            string input = www.downloadHandler.text;
            string search = "<div class=\"hidden\" id=\"2classHumi\">";
            int p = input.IndexOf(search);
            if (p >= 0)
            {
                // move forward to the value
                int start = p + search.Length;
                // now find the end by searching for the next closing tag starting at the start position, 
                // limiting the forward search to the max value length
                int end = input.IndexOf("</div>", start);
                if (end >= 0)
                {
                    // pull out the substring
                    string v = input.Substring(start, end - start);
                    // finally parse into a float
                    float value = float.Parse(v);
                   // Debug.Log("2classHumi Value = " + value);
                    getValue = (int)value;
                }
                
            }
           
        }
        
        return getValue;
    }
    private int Get1ClassHumi(UnityWebRequest www)
    {
        int getValue = 0;
        // check for errors 
        if(www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
        {
            string input = www.downloadHandler.text;
            string search = "<div class=\"hidden\" id=\"1classHumi\">";
            int p = input.IndexOf(search);
            if (p >= 0)
            {
                // move forward to the value
                int start = p + search.Length;
                // now find the end by searching for the next closing tag starting at the start position, 
                // limiting the forward search to the max value length
                int end = input.IndexOf("</div>", start);
                if (end >= 0)
                {
                    // pull out the substring
                    string v = input.Substring(start, end - start);
                    // finally parse into a float
                    float value = float.Parse(v);
                   // Debug.Log("1classHumi Value = " + value);
                    getValue = (int)value;
                }
                
            }
           
        }
        
        return getValue;
    }

    private int Get2ClassTemp(UnityWebRequest www)
    {
        int getValue = 0;
        // check for errors 
         if(www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
        {
            string input = www.downloadHandler.text;
            string search = "<div class=\"hidden\" id=\"2classTemp\">";
            int p = input.IndexOf(search);
            if (p >= 0)
            {
                // move forward to the value
                int start = p + search.Length;
                // now find the end by searching for the next closing tag starting at the start position, 
                // limiting the forward search to the max value length
                int end = input.IndexOf("</div>", start);
                if (end >= 0)
                {
                    // pull out the substring
                    string v = input.Substring(start, end - start);
                    // finally parse into a float
                    float value = float.Parse(v);
                  //  Debug.Log("2classTemp Value = " + value);
                    getValue = (int)value;
                }                
            }           
        }       
        return getValue;
    }

    private int Get1ClassTemp(UnityWebRequest www)
    {
        int getValue = 0;
        // check for errors 
        if(www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
        {
            string input = www.downloadHandler.text;
            string search = "<div class=\"hidden\" id=\"1classTemp\">";
            int p = input.IndexOf(search);
            if (p >= 0)
            {
                // move forward to the value
                int start = p + search.Length;
                // now find the end by searching for the next closing tag starting at the start position, 
                // limiting the forward search to the max value length
                int end = input.IndexOf("</div>", start);
                if (end >= 0)
                {
                    // pull out the substring
                    string v = input.Substring(start, end - start);
                    // finally parse into a float
                    float value = float.Parse(v);
                   // Debug.Log("1classTemp Value = " + value);
                     getValue = (int)value;
                }
                else
                {
                    Debug.Log("Bad html - closing tag not found");
                    getValue = -1;
                }
            }
           
        }
       
        return getValue;
    }
    private IEnumerator WaitForGetHeaders(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        foreach (KeyValuePair<string, string>  pair in www.responseHeaders)
        {
            Debug.Log(pair.Key + "=" + pair.Value  );
        }
    }
}


