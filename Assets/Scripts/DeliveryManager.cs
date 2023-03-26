using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    //Singleton Pattern
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler<OnRecipeArgs> OnRecipeSpawned;
    public event EventHandler<OnRecipeArgs> OnRecipeCompleted;

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    private int successfulRecipesCount;

    public class OnRecipeArgs : EventArgs
    {
        public RecipeSO recipeSO;
    }


    [SerializeField] private RecipeListSO recipeListSO;
    List<RecipeSO> waitingRecipeSOList;

    [SerializeField] private float spawnRecipeTimerMax = 8f;
    [SerializeField] private int waitingRecipesMax = 4;
    private float spawnRecipeTimer;
    

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, new OnRecipeArgs
                {
                    recipeSO = waitingRecipeSO
                });

            }
        }
    }


    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i=0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has the same number of ingredients
                bool plateContentsMatchesRecipe = true;

                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //Cycling through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycling through all ingredients in the Plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //Ingredients matches!
                            ingredientFound = true;
                            break;
                        }

                    }
                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }

                }

                if (plateContentsMatchesRecipe)
                {
                    //Player delivered the correct recipe!
                    OnRecipeCompleted?.Invoke(this, new OnRecipeArgs
                    {
                        recipeSO = waitingRecipeSOList[i]
                    });
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    waitingRecipeSOList.RemoveAt(i);
                    successfulRecipesCount++;

                    return;
                }

            }
            
            }
        
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);

        //No matches found
        //Player did the wrong recipe
    }


    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesCount()
    {
        return successfulRecipesCount;
    }

}

         


