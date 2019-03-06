using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsManager : MonoBehaviour
{

    public LanguageCollection languages;
    public GameObject homeButton;
    public GameObject home;
    public GameObject levels;
    public GameObject subLevels;
    public GameObject questionsAndAnswers;
    public GameObject results;
    public GameObject backGround;
    public GameObject audioManager;
    [HideInInspector] public int languageNumber;
   /* [HideInInspector] */public int questionNumber;

    private void Start()
    {
        GoHome();
    }

    private void Update() 
    {
        if (home.activeInHierarchy) 
        {
            homeButton.SetActive(false);
        }
        else 
        {
            homeButton.SetActive(true);
        }
    }
    
    //Home
    public void GoHome() 
    {
        levels.SetActive(false);
        subLevels.SetActive(false);
        questionsAndAnswers.SetActive(false);
        results.SetActive(false);
        home.SetActive(true);
    }

    //Language Selection
    public void OpenLevels()
    {
        int i = 0;
        
        home.SetActive(false);
        levels.SetActive(true);
        
        foreach (LevelCollection lvl in languages.languageLevels)
        {
            levels.transform.GetChild(i).gameObject.GetComponentInChildren<Text>().text = lvl.name;
            i++;
        }
    }
    
    //LevelSelection
    public void OpenSublevels(int x)
    {
        languageNumber = x;
        levels.SetActive(false);
        subLevels.SetActive(true);
        
        for (int j = 0; j < languages.languageLevels[languageNumber].unlocked.Count; j++)
        {
            if (!languages.languageLevels[languageNumber].unlocked[j])
            {
                subLevels.transform.GetChild(j).gameObject.GetComponent<Image>().color = Color.red;
                subLevels.transform.GetChild(j).gameObject.GetComponent<Button>().enabled = false;
            }
            else
            {
                subLevels.transform.GetChild(j).gameObject.GetComponent<Image>().color = Color.white;
                subLevels.transform.GetChild(j).gameObject.GetComponent<Button>().enabled = true;
            }
        }
    }

    public void LevelSelection() 
    {
        
    }
    
    //single question
    public void OpenQuestionsAndAnswers(int i)
    {
        questionNumber = i;
        subLevels.SetActive(false);
        questionsAndAnswers.SetActive(true);

        AnswerQuestionSeries(); 
    }

    //singleQuestion
    private void AnswerQuestionSeries()
    {
        questionsAndAnswers.transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = languages.languageLevels[languageNumber].questionCollection[0].questions[questionNumber].question;
        questionsAndAnswers.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = languages.languageLevels[languageNumber].questionCollection[0].questions[questionNumber].answers[0];
        questionsAndAnswers.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = languages.languageLevels[languageNumber].questionCollection[0].questions[questionNumber].answers[1];
        questionsAndAnswers.transform.GetChild(3).gameObject.GetComponentInChildren<Text>().text = languages.languageLevels[languageNumber].questionCollection[0].questions[questionNumber].answers[2];
        if (languages.languageLevels[languageNumber].questionCollection[0].questions[questionNumber].answers.Count == 4) 
        {
            questionsAndAnswers.transform.GetChild(4).gameObject.SetActive(true);
            questionsAndAnswers.transform.GetChild(4).gameObject.GetComponentInChildren<Text>().text = languages.languageLevels[languageNumber].questionCollection[0].questions[questionNumber].answers[3];    
        }
        else
        {
            questionsAndAnswers.transform.GetChild(4).gameObject.SetActive(false);
        }
        
    }
    
    private void NextQuestion()
    {
        questionNumber++;
        if (questionNumber >= languages.languageLevels[languageNumber].questionCollection[0].questions.Count)
        {
            OpenLevels();
        }
        else
        {
            questionsAndAnswers.SetActive(true);
            AnswerQuestionSeries();
        }
    }
    
    public void CheckResult(int ans)
    {
        //if true = win, else = lose
        questionsAndAnswers.SetActive(false);
        results.SetActive(true);
        
        if (languages.languageLevels[languageNumber].questionCollection[0].questions[questionNumber].rightAnswer == ans+1)
        {
            results.transform.GetChild(0).gameObject.SetActive(true);
            results.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(WaitToNextQuestion(true));
        }
        else
        {    
            results.transform.GetChild(0).gameObject.SetActive(false);
            results.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WaitToNextQuestion(false));
        }
    }

    IEnumerator WaitToNextQuestion(bool success)
    {
        yield return new WaitForSeconds(3);
        if (success)
        {
            if (questionNumber+1 <= languages.languageLevels[languageNumber].unlocked.Count-1)
            {
                languages.languageLevels[languageNumber].unlocked[questionNumber+1] = true;
            }
            
            NextQuestion();
        }
        else
        {
            OpenSublevels(languageNumber);
        }
        results.transform.GetChild(0).gameObject.SetActive(false);
        results.transform.GetChild(1).gameObject.SetActive(false);
    }

}

