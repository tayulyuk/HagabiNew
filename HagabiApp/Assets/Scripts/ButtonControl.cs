using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour {

    void OnClick()
    {
        //어플 종료!
        if (transform.name == "Exit_Button")
        {
            StartCoroutine(AppClose());
        }

        if (transform.name == "ViewCloseButton")
        {
            StartCoroutine(ViewCloseForSound());
        }


        if (transform.name == "Next Button")
        {
            StartCoroutine(NextButtonForSound());
        }
        if (transform.name == "Before Button")
        {
            StartCoroutine(BeforeButtonForSound());
        }

        if (GameObject.Find("ManagerGameObject") != null)
        {
            //1동 버튼들
            GameObject go = GameObject.Find("ManagerGameObject");
            ManagerController mc = go.GetComponent<ManagerController>();

            if (transform.name == "1_1btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 1;
                DecisionSceneMove();
            }
            if (transform.name == "1_2btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 2;
                DecisionSceneMove();
            }
            if (transform.name == "1_3btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 3;
                DecisionSceneMove();
            }
            if (transform.name == "1_4btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 4;
                DecisionSceneMove();
            }
            if (transform.name == "1_5btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 5;
                DecisionSceneMove();
            }
            if (transform.name == "1_6btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 6;
                DecisionSceneMove();
            }
            if (transform.name == "1_7btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 7;
                DecisionSceneMove();
            }
            if (transform.name == "1_8btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 8;
                DecisionSceneMove();
            }
            if (transform.name == "1_9btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 9;
                DecisionSceneMove();
            }
            if (transform.name == "1_10btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 10;
                DecisionSceneMove();
            }
            if (transform.name == "1_11btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 11;
                DecisionSceneMove();
            }
            if (transform.name == "1_12btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 12;
                DecisionSceneMove();
            }
            if (transform.name == "1_13btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 13;
                HagabiFanControlDecisionSceneMove();
            }
            if (transform.name == "1_14btn")
            {
                mc.currentClass = 1;
                mc.selectButtonNumber = 14;
                HagabiFanControlDecisionSceneMove();
            }


            //2동 버튼들
            if (transform.name == "2_1btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 1;
                DecisionSceneMove();
            }
            if (transform.name == "2_2btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 2;
                DecisionSceneMove();
            }
            if (transform.name == "2_3btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 3;
                DecisionSceneMove();
            }
            if (transform.name == "2_4btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 4;
                DecisionSceneMove();
            }
            if (transform.name == "2_5btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 5;
                DecisionSceneMove();
            }
            if (transform.name == "2_6btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 6;
                DecisionSceneMove();
            }
            if (transform.name == "2_7btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 7;
                DecisionSceneMove();
            }
            if (transform.name == "2_8btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 8;
                DecisionSceneMove();
            }
            if (transform.name == "2_9btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 9;
                DecisionSceneMove();
            }
            if (transform.name == "2_10btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 10;
                DecisionSceneMove();
            }
            if (transform.name == "2_11btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 11;
                DecisionSceneMove();
            }
            if (transform.name == "2_12btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 12;
                DecisionSceneMove();
            }
            if (transform.name == "2_13btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 13;
                HagabiFanControlDecisionSceneMove();
            }
            if (transform.name == "2_14btn")
            {
                mc.currentClass = 2;
                mc.selectButtonNumber = 14;
                HagabiFanControlDecisionSceneMove();
            }
        }
    }

    private IEnumerator AppClose()
    {
        yield return new WaitForSeconds(GetComponent<UIPlaySound>().audioClip.length);
        Application.Quit();
    }

    private IEnumerator ViewCloseForSound()
    {
        yield return new WaitForSeconds(GetComponent<UIPlaySound>().audioClip.length);
        SceneManager.LoadScene("HagabiControl");
    }

    private IEnumerator BeforeButtonForSound()
    {
        yield return new WaitForSeconds(GetComponent<UIPlaySound>().audioClip.length);
        SceneManager.LoadScene("Hagabi_first");
    }

    private IEnumerator NextButtonForSound()
    {
        yield return new WaitForSeconds(GetComponent<UIPlaySound>().audioClip.length);
         SceneManager.LoadScene("HagabiControl");
    }

    
    private void HagabiFanControlDecisionSceneMove()
    {
        StartCoroutine(HagabiFanControlMoveNextScene());
    }

    void DecisionSceneMove()
    {
        StartCoroutine(MoveNextScene());
    }
    IEnumerator HagabiFanControlMoveNextScene()
    {
        yield return new WaitForSeconds(GetComponent<UIPlaySound>().audioClip.length);
        SceneManager.LoadScene("HagabiFanControlDecision");
    }
    IEnumerator MoveNextScene()
    {
        yield return new WaitForSeconds(GetComponent<UIPlaySound>().audioClip.length);
        SceneManager.LoadScene("HagabiDecision");
    }
}
