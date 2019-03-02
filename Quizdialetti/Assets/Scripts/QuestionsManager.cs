using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsManager : MonoBehaviour
{

    public Languages languages;
    public GameObject levels;
    public GameObject subLevels;
    public GameObject questionsAndAnswers;
    public GameObject results;
    [HideInInspector] public int languageNumber;
   /* [HideInInspector] */public int questionNumber;

    private void Start()
    {
        levels.SetActive(false);
        subLevels.SetActive(false);
        questionsAndAnswers.SetActive(false);
        results.SetActive(false);
        OpenLevels();
    }

    public void OpenLevels()
    {
        int i = 0;
        //chi chiama va false
        levels.SetActive(true);
        
        foreach (QuestionCollection ansAndQuest in languages.languageQuestionCollection)
        {
            levels.transform.GetChild(i).gameObject.GetComponentInChildren<Text>().text = ansAndQuest.name;
            i++;
        }
    }
    
    public void OpenSublevels(int x)
    {
        languageNumber = x;
        levels.SetActive(false);
        subLevels.SetActive(true);
        
        for (int j = 0; j < languages.languageQuestionCollection[languageNumber].unlocked.Count; j++)
        {
            if (!languages.languageQuestionCollection[languageNumber].unlocked[j])
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
    
    public void OpenQuestionsAndAnswers(int i)
    {
        questionNumber = i;
        subLevels.SetActive(false);
        questionsAndAnswers.SetActive(true);

        AnswerQuestionSeries(); 
    }

    private void AnswerQuestionSeries()
    {
        questionsAndAnswers.transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = languages.languageQuestionCollection[languageNumber].questions[questionNumber].question;
        questionsAndAnswers.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = languages.languageQuestionCollection[languageNumber].questions[questionNumber].answers[0];
        questionsAndAnswers.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = languages.languageQuestionCollection[languageNumber].questions[questionNumber].answers[1];
        questionsAndAnswers.transform.GetChild(3).gameObject.GetComponentInChildren<Text>().text = languages.languageQuestionCollection[languageNumber].questions[questionNumber].answers[2];
    }
    
    private void NextQuestion()
    {
        questionNumber++;
        if (questionNumber >= languages.languageQuestionCollection[languageNumber].questions.Count)
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
        
        if (languages.languageQuestionCollection[languageNumber].questions[questionNumber].rightAnswer == ans+1)
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
            if (questionNumber+1 <= languages.languageQuestionCollection[languageNumber].unlocked.Count-1)
            {
                languages.languageQuestionCollection[languageNumber].unlocked[questionNumber+1] = true;
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

