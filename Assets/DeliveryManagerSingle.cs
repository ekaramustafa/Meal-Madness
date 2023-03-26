using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerSingle : MonoBehaviour, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    private float orderTimer = 0f;
    private RecipeSO recipeSO;

    private void Start()
    {
        recipeSO = GetComponent<DeliveryManagerSingleUI>().GetRecipeSO();
    }
    private void Update()
    {
        //If the game is not playing then do not update the rest
        if (!GameManager.Instance.IsGamePlaying()) return;

        orderTimer += Time.deltaTime;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = orderTimer / recipeSO.maxOrderTime
        });

        if (orderTimer > recipeSO.maxOrderTime)
        {
            GamePointsUI.Instance.DecremenPoints(recipeSO.recipePoints);
            Destroy(transform.gameObject);
        }
    }



}
