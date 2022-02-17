using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizResultsScript : MonoBehaviour
{
    public Text results;
    public Button backButton;
    public GameObject QuizDetailsPanel;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(() => ReturnToQuizDetailsPanel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReturnToQuizDetailsPanel()
    {
        this.gameObject.SetActive(false);
        QuizDetailsPanel.SetActive(true);

        results.text = "";
    }
}
