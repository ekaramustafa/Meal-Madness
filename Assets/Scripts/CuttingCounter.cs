using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player does not hold any kitchen object
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                //player already has the kitchen object
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }
    public override void InteractAlternate(Player player)
    {
        
        if (HasKitchenObject())
        {
            //There is a KitchenObject and cut it
            KitchenObjectSO outputKitchenSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            if (outputKitchenSO != null)
            {
                //if the KitchenObject has a cutting recipe
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenSO, this);
            }

        }

    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
