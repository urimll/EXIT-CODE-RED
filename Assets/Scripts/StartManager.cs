using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject lessonPanel;
    [SerializeField] TMP_Text lessonText;
    [SerializeField] Button preButton;    
    [SerializeField] Button nextButton;
    [SerializeField] Button nextSceneButton;
    [SerializeField] Image progressBar;
    [SerializeField] GameObject[] lessonBG;
    int progress = 1;
    BackendManager _backendManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        preButton.onClick.AddListener(() => PreButtonClick());
        nextButton.onClick.AddListener(() => NextButtonClick());
        _backendManager = FindAnyObjectByType<BackendManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LessonButtonClick()
    {
        startPanel.SetActive(false);
        lessonPanel.SetActive(true);
        _backendManager.level = 0;
    }

    public void PlayButtonClick()
    {
        _backendManager.level = 1;
        SceneManager.LoadScene(1);
    }

    public void NextButtonClick()
    {
        if (progress == 5)
        {
            SceneManager.LoadScene(1);
        }
        if (progress == 4)
        {
            nextButton.gameObject.SetActive(false);
            nextSceneButton.gameObject.SetActive(true);
        }
        preButton.interactable = true;
        progress++;
        progressBar.fillAmount = (float)progress / 5;
        lessonText.text = $"{progress}/5";
        for (int i = 0; i < lessonBG.Length; i++)
        {
            lessonBG[i].SetActive(false);
        }
        lessonBG[progress -1].SetActive(true);
    }

    public void PreButtonClick()
    {
        progress--;
        progressBar.fillAmount = (float)progress / 5;
        lessonText.text = $"{progress}/5";
        nextButton.gameObject.SetActive(true);
        nextSceneButton.gameObject.SetActive(false);
        if (progress == 1)
        {
            preButton.interactable = false;
        }

        for (int i = 0; i < lessonBG.Length; i++)
        {
            lessonBG[i].SetActive(false);
        }
        lessonBG[progress - 1].SetActive(true);
    }

}
