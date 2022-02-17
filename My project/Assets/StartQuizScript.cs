using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartQuizScript : MonoBehaviour
{
    GameObject quizName;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => StartQuiz());

        quizName = this.transform.parent.Find("QuizName").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuiz()
    {
        GameObject mainPanel = GameObject.Find("MainPanel");
        GameObject quizDetailsPanel = FindInActiveObjectByName("QuizDetailsPanel");

        quizDetailsPanel.SetActive(true);
        mainPanel.SetActive(false);

        Text quizTitle = quizDetailsPanel.transform.Find("QuizTitleText").GetComponent<Text>();
        quizTitle.text = quizName.GetComponent<Text>().text;
        quizDetailsPanel.GetComponent<QuizDetailsScript>().currQuizName = quizTitle.text;
        quizDetailsPanel.GetComponent<QuizDetailsScript>().UpdateQuestions();
    }

    GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}

