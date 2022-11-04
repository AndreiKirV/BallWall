using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _GameOverPanel;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _numberOfAttempts;
    private int _complexity = 1;
    public int Complexity => _complexity;
    public static string ComplexityKey = "Complexity";
    public static string NumberOfAttemptsKey = "NumberOfAttempts";
    public UnityEvent ComplexityHasChanged = new UnityEvent();
    public delegate float OutputFloat();
    public OutputFloat TimeRequest;

    private void Awake() 
    {
        Time.timeScale = 0;

        if (PlayerPrefs.HasKey(ComplexityKey))
        _complexity = PlayerPrefs.GetInt(ComplexityKey);
    }

    private void SetTimerText()
    {
        if (TimeRequest!=null)
        {
            float temp = TimeRequest.Invoke();
            _timer.text = $"Продолжительность последней попытки - {(int)temp / 60} : {(int)temp % 60}";  
        }
    }

    public void CloseStartPanel()
    {
        Time.timeScale = 1;
        ClosePanel(_startPanel);
    }

    public void SetComplexity(int value)
    {
        _complexity = value;
        PlayerPrefs.SetInt(ComplexityKey, value);

        if (ComplexityHasChanged != null)
        ComplexityHasChanged.Invoke();
    }

    public void ClosePanel(GameObject targetObject)
    {
        targetObject.SetActive(false);
    }

    public void OpenPanel(GameObject targetObject)
    {
        targetObject.SetActive(true);
    }

    public void OpenGameOver()
    {
        Time.timeScale = 0;
        OpenPanel(_GameOverPanel);

        if (!PlayerPrefs.HasKey(NumberOfAttemptsKey))
        _numberOfAttempts.text = $"Количество попыток - 1";
        else
        _numberOfAttempts.text = $"Количество попыток - {PlayerPrefs.GetInt(NumberOfAttemptsKey) + 1}";

        SetTimerText();
    }

    public void Restart()
    {
        if (!PlayerPrefs.HasKey(NumberOfAttemptsKey))
        PlayerPrefs.SetInt(NumberOfAttemptsKey, 1);
        else
        PlayerPrefs.SetInt(NumberOfAttemptsKey,  PlayerPrefs.GetInt(NumberOfAttemptsKey) + 1);

        SceneManager.LoadScene("SampleScene");
    }
}