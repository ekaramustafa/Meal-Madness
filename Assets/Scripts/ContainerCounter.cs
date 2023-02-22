using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

 
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player has not kitchen object
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
          
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

}
