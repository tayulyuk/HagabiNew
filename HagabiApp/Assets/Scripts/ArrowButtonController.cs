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
}
