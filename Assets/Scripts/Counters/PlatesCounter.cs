using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] KitchenObjectSO plateKitchenObjectS0;
    [SerializeField] private float spawnPlateTimerMax = 4f;


    [SerializeField] List<KitchenObjectSO> validKitchenObjectSOList;
    
    private float spawnPlateTimer = 0f;

    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if(platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player is empty handed
            if (platesSpawnedAmount > 0)
            {
                platesSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectS0, player);

                OnPlateRemoved?.Invoke(this,EventArgs.Empty);
            }
        }
        else
        {
            if (platesSpawnedAmount > 0)
            {
                KitchenObject.SpawnKitchenObject(plateKitchenObjectS0, this);
                if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //There is a plate on the counter
                    if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        
                        player.GetKitchenObject().DestroySelf();
                        plateKitchenObject.SetKitchenObjectParent(player);
                        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                        platesSpawnedAmount--;

                    }
                }
                else
                {
                    GetKitchenObject().DestroySelf();
                    
                }
            }   
            //Check whether player carries a object that can be placed onto plate
        }
    }



}
