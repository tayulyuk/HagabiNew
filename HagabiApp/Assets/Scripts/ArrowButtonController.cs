using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.up   2.stop   3.down
/// </summary>
public class ArrowButtonController : MonoBehaviour
{
    public UISprite upSprite;
    public UISprite stopSprite;
    public UISprite downSprite;
    Coroutine arrowBlankCoroutine;
    public int choiceNum = 0;

    void Start()
    {        
        AllOffArrowfBlank();
    }

    public void StartArrow(int num)
    {
      arrowBlankCoroutine = StartCoroutine(ArrowBlank(num));
    }

    IEnumerator ArrowBlank(int num)
    {
        OnArrowBlank(num);
        yield return new WaitForSeconds(1);
        AllOffArrowfBlank();
        yield return new WaitForSeconds(.5f);
        arrowBlankCoroutine = StartCoroutine(ArrowBlank(num));
    }

    public void OnArrowBlank(int num)
    {
        if (num == 1)
        {
            upSprite.gameObject.SetActive(true);
            choiceNum = 1;
        }
        if (num == 2)
        {
            stopSprite.gameObject.SetActive(true);
            choiceNum = 2;
        }
        if (num == 3)
        {
            downSprite.gameObject.SetActive(true);
            choiceNum = 3;
        }
    }

    public void AllOffArrowfBlank()
    {
        upSprite.gameObject.SetActive(false);
        stopSprite.gameObject.SetActive(false);
        downSprite.gameObject.SetActive(false);
    }

    public void SelectStopCorouine()
    {
        if(arrowBlankCoroutine != null)
            StopCoroutine(arrowBlankCoroutine);
    }

    public IEnumerator ArrowAction(string buttonName)
    {
        MqttManager mqttManager = GameObject.Find("ManagerGameObject").GetComponent<MqttManager>();
        mqttManager.isSignOk = false;

        yield return new WaitForSeconds(.5f);
        if (!mqttManager.isLoading)
        {
            //HagabiDecision 씬의 버튼들.
            if (buttonName == "UpButton")
            {
                AllOffArrowfBlank();
                SelectStopCorouine();
                StartArrow(1); //1.up
            }
            else if (buttonName == "StopButton")
            {
                AllOffArrowfBlank();
                SelectStopCorouine();
                StartArrow(2); //2.stop
            }
            else if (buttonName == "DownButton")
            {
                AllOffArrowfBlank();
                SelectStopCorouine();
                StartArrow(3); //3.down
            }
        }
        else
        {
            SelectStopCorouine();
            AllOffArrowfBlank();
        }
    }
}
