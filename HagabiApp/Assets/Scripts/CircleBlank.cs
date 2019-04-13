using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clircle 를 깜박인다.
/// </summary>
public class CircleBlank : MonoBehaviour
{
    public UISprite sel1;
    public UISprite sel2;
    public UISprite sel3;
    public UISprite sel4;
    public UISprite sel5;
    public UISprite sel6;
    public UISprite sel7;
    public UISprite sel8;
    public UISprite sel9;
    public UISprite sel10;
    public UISprite sel11;
    public UISprite sel12;
    public UISprite sel13;
    public UISprite sel14;
    /// <summary>
    /// 싸이클 표시를 위한 번호.
    /// </summary>
    public int selectNumber = 0;

    void Awake()
    {
        AllOffBlank();
    }
    
    void OnEnable()
    {
        StartCoroutine(SelBlank(selectNumber));
    }

    IEnumerator SelBlank(int selNum)
    {
        OnBlank(selNum);
        yield return new WaitForSeconds(.4f);
        AllOffBlank();
        yield return new WaitForSeconds(.2f);
        StartCoroutine(SelBlank(selectNumber));
    }

    private void OnBlank(int selNum)
    {
        if (selNum == 1)
            sel1.gameObject.SetActive(true);
        if (selNum == 2)
            sel2.gameObject.SetActive(true);
        if (selNum == 3)
            sel3.gameObject.SetActive(true);
        if (selNum == 4)
            sel4.gameObject.SetActive(true);
        if (selNum == 5)
            sel5.gameObject.SetActive(true);
        if (selNum == 6)
            sel6.gameObject.SetActive(true);
        if (selNum == 7)
            sel7.gameObject.SetActive(true);
        if (selNum == 8)
            sel8.gameObject.SetActive(true);
        if (selNum == 9)
            sel9.gameObject.SetActive(true);
        if (selNum == 10)
            sel10.gameObject.SetActive(true);
        if (selNum == 11)
            sel11.gameObject.SetActive(true);
        if (selNum == 12)
            sel12.gameObject.SetActive(true);
        if (selNum == 13)
            sel13.gameObject.SetActive(true);
        if (selNum == 14)
            sel14.gameObject.SetActive(true);
    }

    private void AllOffBlank()
    {
        sel1.gameObject.SetActive(false);
        sel2.gameObject.SetActive(false);
        sel3.gameObject.SetActive(false);
        sel4.gameObject.SetActive(false);
        sel5.gameObject.SetActive(false);
        sel6.gameObject.SetActive(false);
        sel7.gameObject.SetActive(false);
        sel8.gameObject.SetActive(false);
        sel9.gameObject.SetActive(false);
        sel10.gameObject.SetActive(false);
        sel11.gameObject.SetActive(false);
        sel12.gameObject.SetActive(false);
        sel13.gameObject.SetActive(false);
        sel14.gameObject.SetActive(false);
    }
}
