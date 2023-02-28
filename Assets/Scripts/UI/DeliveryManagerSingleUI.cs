using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DeliveryManagerSingleUI : MonoBehaviour, IHasProgress
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainter;
    [SerializeField] private Transform iconTemplate;

    private RecipeSO recipeSO;

    private float orderTimer = 0f;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnOrderTimeFinishedArgs> OnOrderTimeFinished;

    public class OnOrderTimeFinishedArgs : EventArgs
    {
        public GameObject gameObject;
    }

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Update()
    {
        orderTimer += Time.deltaTime;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = orderTimer / recipeSO.maxOrderTime
        });

        if(orderTimer > recipeSO.maxOrderTime)
        {
            //Can add another mechanics later.
            Debug.Log("BROOOO The time is up");
            //Destroy(gameObject);
        }
        
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
