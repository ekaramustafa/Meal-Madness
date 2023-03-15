using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectsSettingsUI : MonoBehaviour
{

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button soundEffectsUpButton;
    [SerializeField] private Button soundEffectsDownButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;


    private void Awake()
    {
        soundEffectsUpButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.TurnUpVolume();
            UpdateVisual();

        });

        soundEffectsDownButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.TurnDownVolume();
            UpdateVisual();
        });

        

    }

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects : " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
    }
}
