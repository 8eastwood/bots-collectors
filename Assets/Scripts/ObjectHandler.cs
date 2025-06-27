using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    public void PickUp(SupplyBox supplyToCollect)
    {
        supplyToCollect.TryPickUp(transform);
    }
}
