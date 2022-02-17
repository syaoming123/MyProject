using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class NewQuizScript : MonoBehaviour
{
    public GameObject mainQuizPanel;

    public InputField quizNameInputField;
    public Button saveButton;
    public TextAsset jsonFile;

    private string saveFile;
    private string jsonToSave;


    [System.Serializable]
    public class QuizName
    {
        public string name;
    }

    [System.Serializable]
    public class QuizNameList
    {
        public List<QuizName> quiznamelist;
    }

    private QuizNameList quizzes;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        saveFile = "QuizNameData.json"; //Application.persistentDataPath + " / QuizNameData.json";

        saveButton.onClick.AddListener(delegate { SaveQuizName(); });

        quizzes = new QuizNameList();
        quizzes.quiznamelist = new List<QuizName>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveQuizName()
    {
        //save exisiting data first
        string json = ReadFromFile(saveFile);
        JsonUtility.FromJsonOverwrite(json, quizzes);

        //read from inputfield and save into list
        QuizName newQuiz = new QuizName();
        newQuiz.name = quizNameInputField.text;
        quizzes.quiznamelist.Add(newQuiz);

        //update json file
        string output = JsonUtility.ToJson(quizzes);
        File.WriteAllText(saveFile, output);



        this.gameObject.SetActive(false);
        mainQuizPanel.SetActive(true);
        mainQuizPanel.GetComponent<MainPageScript>().UpdateQuizzes();
    }

    public static string ReadFromFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
            Debug.LogWarning("File Not Found!");

        return "";
    }
}
