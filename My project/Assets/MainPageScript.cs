using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainPageScript : MonoBehaviour
{
    //own obj
    public Button createButton;
    public GameObject quizPrefab;
    private List<GameObject> quizPrefabs;
    public GameObject title;

    //panels
    public GameObject NewQuizPanel;
    public GameObject QuizDetailsPanel;
    public GameObject AddQnPanel;
    public GameObject PreviewPanel;
    public GameObject QuizResultsPanel;

    //new quiz page text
    public Text qpTitle;

    private string saveFile;

    // Start is called before the first frame update
    void Start()
    {
        saveFile = "QuizNameData.json"; //Application.persistentDataPath + "/QuizNameData.json";

        createButton.onClick.AddListener(delegate { CreateQuizPage(); });

        NewQuizPanel.SetActive(false);
        QuizDetailsPanel.SetActive(false);
        AddQnPanel.SetActive(false);
        PreviewPanel.SetActive(false);
        QuizResultsPanel.SetActive(false);

        quizPrefabs = new List<GameObject>();
        UpdateQuizzes();
    }

    // Update is called once per frame
    void Update()
    {

         
    }
    

    void CreateQuizPage()
    {

        this.gameObject.SetActive(false);
        NewQuizPanel.SetActive(true);
    }

    public void UpdateQuizzes()
    {
        NewQuizScript.QuizNameList quizzes = JsonUtility.FromJson<NewQuizScript.QuizNameList>(File.ReadAllText(saveFile));
        
        if (quizzes == null) return;


        foreach (GameObject quiz in quizPrefabs)
        {
            Object.Destroy(quiz);
        }

        Vector3 pos = new Vector3(0.0f, -56.0f);
        foreach(NewQuizScript.QuizName name in quizzes.quiznamelist)
        {           
            GameObject quiz = Instantiate(quizPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            quiz.transform.SetParent(title.transform);
            quiz.transform.localPosition = pos;//title.transform.localPosition;
            quizPrefabs.Add(quiz);
            Transform trans = quiz.transform.Find("QuizName");

            trans.GetComponent<Text>().text = name.name;

            pos.y -= 56.0f;

            File.Open("Quizzes/"+ name.name + ".json", FileMode.OpenOrCreate);
        }
    }
}
