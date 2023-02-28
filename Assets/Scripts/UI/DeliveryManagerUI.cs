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
        //UpdateVisual();

    }

    private void DeliveryManager_OnRecipeCompleted(object sender, DeliveryManager.OnRecipeArgs e)
    {
        foreach (Transform child in container)
        {
            //TODO: Use TryToGetComponent for safety
            if (child.GetComponent<DeliveryManagerSingleUI>().GetRecipeSO() == e.recipeSO && child != recipeTemplate)
            {
                Destroy(child.gameObject);
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



    //private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    //{
    //    UpdateVisual();

    //}

    //private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    //{

    //    UpdateVisual();
    //}



    //private void UpdateVisual()
    //{
    //    foreach(Transform child in container)
    //    {
    //        if (child != recipeTemplate)
    //        {
    //            Destroy(child.gameObject);
    //        }
    //    }

    //    foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()){
    //        Transform recipeTransform = Instantiate(recipeTemplate, container);
    //        recipeTransform.gameObject.SetActive(true);
    //        recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
    //    }


    }

