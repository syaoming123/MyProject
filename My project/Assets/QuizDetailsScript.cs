using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class QuizDetailsScript : MonoBehaviour
{

    public Button previewButton;
    public Button addQnButton;

    public GameObject AddQnPanel;
    public GameObject PreviewPanel;
    public GameObject title;
    public GameObject qnPrefab;

    public string currQuizName;

    public List<GameObject> qnPrefabs;
    private string saveFile;
    // Start is called before the first frame update
    void Start()
    {
        qnPrefabs = new List<GameObject>();

        addQnButton.onClick.AddListener(delegate
        {
            this.gameObject.SetActive(false);
            AddQnPanel.SetActive(true);
        });

        previewButton.onClick.AddListener(delegate
        {
            this.gameObject.SetActive(false);
            PreviewPanel.SetActive(true);
            PreviewPanel.GetComponent<PreviewQuizPanel>().saveFile = saveFile;
            PreviewPanel.GetComponent<PreviewQuizPanel>().UpdateQnList();
            PreviewPanel.GetComponent<PreviewQuizPanel>().UpdateButtonText();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateQuestions()
    {
        saveFile = "Quizzes/" + currQuizName + ".json";
        AddNewQnScript.QuestionList qns = JsonUtility.FromJson<AddNewQnScript.QuestionList>(File.ReadAllText(saveFile));

        if (qns == null) return;

        if (qnPrefabs.Count != 0)
        {
            foreach (GameObject qn in qnPrefabs)
            {
                Object.Destroy(qn);
            }
        }

        Vector3 pos = new Vector3(0.0f, -56.0f);
        int num = 1;
        foreach(AddNewQnScript.Question qn in qns.questionList)
        {
            GameObject newQn = Instantiate(qnPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newQn.transform.SetParent(title.transform);
            newQn.transform.localPosition = pos;
            qnPrefabs.Add(newQn);

            Text numText = newQn.transform.Find("Qn Number Text").GetComponent<Text>();
            numText.text = num.ToString() + ".";

            Text qnText = newQn.transform.Find("Question").GetComponent<Text>();
            qnText.text = qn.qnStr;

            Text optionText = newQn.transform.Find("Option").GetComponent<Text>();
            optionText.text = qn.answer.ToString();

            pos.y -= 56.0f;
            ++num;
        }

    }
}
