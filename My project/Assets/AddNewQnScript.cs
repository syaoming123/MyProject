using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AddNewQnScript : MonoBehaviour
{
    public GameObject QuizDetailsPanel;

    public Button saveButton;

    public InputField[] texts;

    public Toggle[] toggles;

    public Dropdown dropdown;

    [System.Serializable]
    public class Question
    {
        public string qnStr;
        public string option1;
        public string option2;
        public string option3;
        //public int chosenAnswer;
        public int answer;
    }

    [System.Serializable]
    public class QuestionList
    {
        public List<Question> questionList;
    }

    private QuestionList qnList;

    void Awake()
    {
        qnList = new QuestionList();
        qnList.questionList = new List<Question>();
    }

    // Start is called before the first frame update
    void Start()
    {
        saveButton.onClick.AddListener(() => Save());

        //toggles[1].onValueChanged.AddListener
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Save()
    {
        string quizName = QuizDetailsPanel.GetComponent<QuizDetailsScript>().currQuizName;
        string json = NewQuizScript.ReadFromFile("Quizzes/" + quizName + ".json");
        JsonUtility.FromJsonOverwrite(json, qnList);

        Question newQn = new Question();
        newQn.qnStr = texts[0].text;
        newQn.option1 = texts[1].text;
        newQn.option2 = texts[2].text;
        newQn.option3 = texts[3].text;
        //newQn.chosenAnswer = CorrectAnswer();
        newQn.answer = CorrectAnswer();//dropdown.value;
        qnList.questionList.Add(newQn);

        string output = JsonUtility.ToJson(qnList);
        File.WriteAllText("Quizzes/" + quizName + ".json", output);

        foreach (InputField text in texts)
        {
            text.text = "";
        }

        this.gameObject.SetActive(false);
        QuizDetailsPanel.SetActive(true);
        QuizDetailsPanel.GetComponent<QuizDetailsScript>().UpdateQuestions();
    }

    private int CorrectAnswer()
    {
        if(toggles[0].isOn) { return 1; }
        if(toggles[1].isOn) { return 2; }
        if(toggles[2].isOn) { return 3; }

        return 1;
    }



}
