using UnityEngine;

public class ObjectAbsorbable : MonoBehaviour, IAbsorbable
{
    // Take the player's size value > if player bigger than object, absorb, return the size the player got

    private ParentObjectAbsorbable parent;
    public float ObjectSize;
    public float ReturnSizeValue;
    private bool ObjectAbsorbed = false; // unused var

    //void Start()
    //{
    //    // Checks if there is a parent object with the appropriate script, easier to edit all the values
    //    // Currently unused, slightly reductant as you can select all in folder, uncomment to use ig
    //    if (TryGetComponent<ParentObjectAbsorbable>(out ParentObjectAbsorbable unusedVar))
    //    {
    //        parent = GetComponentInParent<ParentObjectAbsorbable>();

    //        ObjectSize = parent.ObjectSize;
    //        ReturnSizeValue = parent.ReturnSizeValue;
    //    }
    //}

    public float Absorb(float PlayerSizeValue)
    {
        // If the player is larger than object, absorb, increase player size, and delete object
        if (PlayerSizeValue >= ObjectSize && !ObjectAbsorbed)
        {
            
            ObjectAbsorbed = true;
            Destroy(gameObject);
            return ReturnSizeValue;
            
        }
        // else, return 0; which the player would need to check if it got 0
        else
        {
            return 0;
        }

    }



}
