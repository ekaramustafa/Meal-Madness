using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable] // to be serializeField
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjectsList;


    private void Start()
    {
        
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjectsList)
        {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
        
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        CheckExistingIngredients();
       
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSO_GameObjectsList)
        {
            //ingredient added to plate
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
               
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }

        }
    }

    private void CheckExistingIngredients()
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSO_GameObjectsList)
        {
            //ingredient added to plate
            if (plateKitchenObject.GetKitchenObjectSOList().Contains(kitchenObjectSOGameObject.kitchenObjectSO))
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }

        }
    }
}
