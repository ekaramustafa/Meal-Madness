using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainter;
    [SerializeField] private Transform iconTemplate;

    private RecipeSO recipeSO;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        this.recipeSO = recipeSO;

        recipeNameText.text = recipeSO.recipeName;
        foreach(Transform child in iconContainter)
        {
            if (child != iconTemplate)
                Destroy(child);
        }


        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainter);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }

    }

    public RecipeSO GetRecipeSO()
    {
        return recipeSO;
    }


}
