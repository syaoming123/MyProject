using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PreviewQuizPanel : MonoBehaviour
{
    public string saveFile;

    public Text qn;
    public Text option1;
    public Text option2;
    public Text option3;
    public Button nextButton;
    public Toggle[] toggles;
    public GameObject QuizResultsPanel;

    private AddNewQnScript.QuestionList qns;
    private int currQn;
    private int numOfQns;
    private int numOfCorrects;
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() => OnButtonClick());
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void UpdateQnList()
    {
        qns = JsonUtility.FromJson<AddNewQnScript.QuestionList>(File.ReadAllText(saveFile));
        numOfQns = qns.questionList.Count;
        currQn = 0;
        numOfCorrects = 0;

        UpdateQuestion(currQn);
    }

    private void UpdateQuestion(int num)
    {
        if(num >= numOfQns) { return; }

        AddNewQnScript.Question question = qns.questionList[num];

        qn.text = question.qnStr;
        option1.text = question.option1;
        option2.text = question.option2;
        option3.text = question.option3;

    }

    private void OnButtonClick()
    {
        Transform text = nextButton.transform.Find("Text");

        CheckAnswer();

        if(text.GetComponent<Text>().text != "Submit")
        {
            GoToNextQn();
        }
        else
        {
            string result = "Your Score: " + numOfCorrects + "/" + qns.questionList.Count.ToString();

            QuizResultsPanel.GetComponent<QuizResultsScript>().results.text = result;
            text.GetComponent<Text>().text = "Next";
            ResetToggleGroup();

            this.gameObject.SetActive(false);
            QuizResultsPanel.SetActive(true);

        }
    }

    private void GoToNextQn()
    {
        ++currQn;

        UpdateButtonText();
        UpdateQuestion(currQn);
        ResetToggleGroup();
    }

    public void UpdateButtonText()
    {
        if (currQn + 1 >= numOfQns)
        {
            Transform text = nextButton.transform.Find("Text");

            if (text != null)
            {
                text.GetComponent<Text>().text = "Submit";
            }
        }
    }

    private void ResetToggleGroup()
    {
        toggles[0].isOn = true;
        toggles[1].isOn = false;
        toggles[2].isOn = false;
    }

    private void CheckAnswer()
    {
        int ans = 0;
        if (toggles[0].isOn) { ans = 1; }
        if (toggles[1].isOn) { ans = 2; }
        if (toggles[2].isOn) { ans = 3; }
        //Debug.Log(currQn);
        if(ans == qns.questionList[currQn].answer) { Debug.Log("Correct"); ++numOfCorrects; }
        else { Debug.Log("Wrong"); }
    }
}
