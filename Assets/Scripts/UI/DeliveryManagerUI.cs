using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    
    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        

    }

    private void DeliveryManager_OnRecipeCompleted(object sender, DeliveryManager.OnRecipeArgs e)
    {
        foreach (Transform child in container)
        {
            //TODO: Use TryToGetComponent for safety
            if (child.GetComponent<DeliveryManagerSingleUI>().GetRecipeSO() == e.recipeSO && child != recipeTemplate)
            {
                Destroy(child.gameObject);
                return;
            }
        }
        
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, DeliveryManager.OnRecipeArgs e)
    {
        //create one more Recipe Template store in list
        Transform recipeTransform = Instantiate(recipeTemplate, container);
        recipeTransform.gameObject.SetActive(true);
        recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(e.recipeSO);
    }

    }

