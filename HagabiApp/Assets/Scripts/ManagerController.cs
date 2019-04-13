using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 통합 관리.
/// </summary>
public class ManagerController : MonoBehaviour {
    
    /// <summary>
    /// 현재 하우스 동 번호
    /// </summary> 
    public int currentClass = 0;

    /// <summary>
    /// 선택된 버튼번호
    /// </summary>  
    public int selectButtonNumber = 0;

    /// <summary>
    /// 선택 버튼 번호 중복 체크용도.
    /// </summary>
    public int checkButtonNum = 10000;

    private static ManagerController m_Instance = null; 

    void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;

        else if (m_Instance != this)
            Destroy(gameObject);    

        DontDestroyOnLoad(gameObject); 
    }

    public static ManagerController instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new GameObject("ManagerGameObject").AddComponent<ManagerController>();
                //오브젝트가 생성이 안되있을경우 생성. 
            }
            return m_Instance;
        }
    }

    void OnApplicationQuit()
    {
        m_Instance = null;
        //게임종료시 삭제. 
    } 
}
