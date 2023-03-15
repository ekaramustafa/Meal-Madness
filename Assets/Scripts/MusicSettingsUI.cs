using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicSettingsUI : MonoBehaviour
{
    [SerializeField] private Button musicButton;
    [SerializeField] private Button musicUpButton;
    [SerializeField] private Button musicDownButton;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        musicUpButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.TurnUpVolume();
            UpdateVisual();
        });


        musicDownButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.TurnDownVolume();
            UpdateVisual();
        });

        
    }

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        musicText.text = "Music : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }
}
