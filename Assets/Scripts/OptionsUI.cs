using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button closeButton;

    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI intearctText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamePadPauseText;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private Button gamePadPauseButton;

    [SerializeField] private Transform pressToRebindKeyTransform;

    //for gamepad settings --TODO : a better way ??
    [SerializeField] private Transform MusicSettingsUITransform;


    private Action onCloseButtonAction;


    private void Awake()
    {
        Instance = this;

        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveUp); });

        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveDown); });
        
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveLeft); });

        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveRight); });

        interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });

        interactAlternateButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.InteractAlternate); });

        pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });

        gamepadInteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamepadInteract); });

        gamepadInteractAlternateButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamepadInteractAlternate); });

        gamePadPauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePadPause); });


    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        Hide();
        UpdateVisual();
        HidePressToRebindKey();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Hide();
    }


    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        MusicSettingsUITransform.GetComponent<MusicSettingsUI>().getMusicButton().Select();
        
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


    public void UpdateVisual()
    {
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        intearctText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamePadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadPause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlternate);

    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }


    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding,() =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }


}
