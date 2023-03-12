using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{


    public static event EventHandler OnAnyObjectTrashed;

    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    // Start is called before the first frame update
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
