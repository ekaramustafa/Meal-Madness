using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GamePointsUI : MonoBehaviour
{
    public static GamePointsUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI pointsText;
    private int points = 0;

    private void Awake()
    {
        Instance = this; 
        UpdateVisual();
        
    }

    private void Start()
    {

        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, DeliveryManager.OnRecipeArgs e)
    {
        points += e.recipeSO.recipePoints;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        points -= 30;
        UpdateVisual();
    }
    
    public void DecremenPoints(int value)
    {
        points -= value;
        UpdateVisual();
        SoundManager.Instance.PlayRecipeTimeOver();
    }
    private void UpdateVisual()
    {
        pointsText.text = points.ToString();
    }

    public int GetPoints()
    {
        return points;
    }
}
