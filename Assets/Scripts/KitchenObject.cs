using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private float fryingTimer;
    private float burningTimer;
    private int cuttingProgress;

    private void Awake()
    {
        fryingTimer = 0f;
        burningTimer = 0f;
        cuttingProgress = 0;
    }

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

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        kitchenObject.gameObject.SetActive(true);
        return kitchenObject;
    }

    public float GetFryingTimer()
    {
        return fryingTimer;
    }
    public float GetBurningTimer()
    {
        return burningTimer;
    }

    public void SetFryingTimer(float fryingTimer)
    {
        this.fryingTimer = fryingTimer;
    }

    public void SetBurningTimer(float burningTimer)
    {
        this.burningTimer = burningTimer;
    }

    public int GetCuttingProgress()
    {
        return cuttingProgress;
    }

    public void SetCuttingProgress(int cuttingProgress)
    {
        this.cuttingProgress = cuttingProgress;
    }


}
