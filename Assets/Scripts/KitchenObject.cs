using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }


    public void SetKitchenObjectParent(IKitchenObjectParent KithcenObjectParent)
    {
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = KithcenObjectParent;
        
        if (KithcenObjectParent.HasKitchenObject())
        {
            Debug.LogError("KithcenObjectParent already has a KitchenObject");
        }

        KithcenObjectParent.SetKitchenObject(this);

        transform.parent = KithcenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
}
