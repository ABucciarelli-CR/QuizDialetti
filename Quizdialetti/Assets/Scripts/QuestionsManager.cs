using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsManager : MonoBehaviour
{

    public LanguageCollection languages;
    public BackGroundCollection bgCollection;
    public AudioCollection audioCollection;
    public Text levelText;
    public GameObject homeButton;
    public GameObject backButton;
    public GameObject home;
    public GameObject levels;
    public GameObject subLevels;
    public GameObject questionsAndAnswers;
    public GameObject results;
    public GameObject backGround;
    public GameObject audioManager;
    [HideInInspector] public int languageNumber;
    private int questionPointer = 0;
   /* [HideInInspector] */public int questionNumber;

    private void Start()
    {
        audioManager.transform.GetChild(0).gameObject.GetComponent<AudioSource>().clip = audioCollection.bgAudio;
        audioManager.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
        GoHome();
    }

    private void Update() 
    {
        if (home.activeInHierarchy) 
        {
            homeButton.SetActive(false);
            backButton.SetActive(false);
        }
        else 
        {
            if (!results.activeInHierarchy) 
            {
                backButton.SetActive(true);
                homeButton.SetActive(true);
            }
            else
            {
                homeButton.SetActive(false);
                backButton.SetActive(false);
            }
        }

        if (questionsAndAnswers.activeInHierarchy || results.activeInHierarchy) 
        {
            levelText.enabled = true;
        }
        else 
        {
            levelText.enabled = false;
        }
    }
    
    //Home
    public void GoHome()
    {
        backGround.GetComponent<Image>().sprite = bgCollection.home;
        levels.SetActive(false);
        subLevels.SetActive(false);
        questionsAndAnswers.SetActive(false);
        results.SetActive(false);
        home.SetActive(true);
    }

    public void GoBack() 
    {
        if (levels.activeInHierarchy) 
        {
            GoHome();
        }
        else if (subLevels.activeInHierarchy) 
        {
            subLevels.SetActive(false);
            OpenLevels();
        }
        else if (questionsAndAnswers.activeInHierarchy) 
        {
            questionsAndAnswers.SetActive(false);
            OpenSublevels(languageNumber);
        }
    }

    //Language Selection
    public void OpenLevels()
    {
        backGround.GetComponent<Image>().sprite = bgCollection.languageSelection;
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
        backGround.GetComponent<Image>().sprite = languages.languageLevels[languageNumber].questionBG;
        questionPointer = 0;
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
        levelText.text = languages.languageLevels[languageNumber].name + " LV. " + (questionNumber + 1) + " - " + (questionPointer + 1);
        questionsAndAnswers.transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = languages.languageLevels[languageNumber].questionCollection[questionNumber].questions[questionPointer].question;
        questionsAndAnswers.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = languages.languageLevels[languageNumber].questionCollection[questionNumber].questions[questionPointer].answers[0];
        questionsAndAnswers.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = languages.languageLevels[languageNumber].questionCollection[questionNumber].questions[questionPointer].answers[1];
        questionsAndAnswers.transform.GetChild(3).gameObject.GetComponentInChildren<Text>().text = languages.languageLevels[languageNumber].questionCollection[questionNumber].questions[questionPointer].answers[2];
        if (languages.languageLevels[languageNumber].questionCollection[questionNumber].questions[questionPointer].answers.Count == 4) 
        {
            questionsAndAnswers.transform.GetChild(4).gameObject.SetActive(true);
            questionsAndAnswers.transform.GetChild(4).gameObject.GetComponentInChildren<Text>().text = languages.languageLevels[languageNumber].questionCollection[questionNumber].questions[questionPointer].answers[3];    
        }
        else
        {
            questionsAndAnswers.transform.GetChild(4).gameObject.SetActive(false);
        }
        
    }
    
    private void NextQuestion()
    {
        //problema qua
        questionPointer++;
        if (questionPointer >= languages.languageLevels[languageNumber].questionCollection[questionNumber].questions.Count && questionNumber >= languages.languageLevels[languageNumber].questionCollection.Count-1)
        {
            OpenLevels();
        }
        else if (questionPointer >= languages.languageLevels[languageNumber].questionCollection[questionNumber].questions.Count && questionNumber < languages.languageLevels[languageNumber].questionCollection.Count)
        {
            languages.languageLevels[languageNumber].unlocked[questionNumber+1] = true;
            OpenSublevels(languageNumber);
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
        
        if (languages.languageLevels[languageNumber].questionCollection[questionNumber].questions[questionPointer].rightAnswer == ans+1)
        {
            audioManager.transform.GetChild(1).gameObject.GetComponent<AudioSource>().clip = audioCollection.winAudio;
            audioManager.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
            results.transform.GetChild(0).gameObject.SetActive(true);
            results.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(WaitToNextQuestion(true));
        }
        else
        {    
            audioManager.transform.GetChild(1).gameObject.GetComponent<AudioSource>().clip = audioCollection.loseAudio;
            audioManager.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
            results.transform.GetChild(0).gameObject.SetActive(false);
            results.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WaitToNextQuestion(false));
        }
    }

    public void GameReset() 
    {
        for (int i=0; i<languages.languageLevels.Count; i++)
        {
            for (int j = 1; j < languages.languageLevels[languageNumber].unlocked.Count; j++) 
            {
                languages.languageLevels[i].unlocked[j] = false;
            }
            
        }
    }

    IEnumerator WaitToNextQuestion(bool success)
    {
        yield return new WaitForSeconds(3);
        if (success)
        {
            //controllo se hai finito le domande, nel caso vai alla selezione della lingua
            if (questionNumber+1 <= languages.languageLevels[languageNumber].questionCollection.Count)
            {
                NextQuestion();
            }
            else 
            {
                OpenSublevels(languageNumber);
            }
            
            
        }
        else
        {
            OpenSublevels(languageNumber);
        }
        results.transform.GetChild(0).gameObject.SetActive(false);
        results.transform.GetChild(1).gameObject.SetActive(false);
        results.SetActive(false);
    }

}

