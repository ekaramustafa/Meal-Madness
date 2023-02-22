using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;



    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //No KitchenObject on the clear counter
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player has nothing. No action
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                //Player has kitchen object
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }



    }

}
