using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour
{
    [SerializeField] private Button _continueButton;

    private void Awake()
    {
        _continueButton.onClick.AddListener(Continue);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
