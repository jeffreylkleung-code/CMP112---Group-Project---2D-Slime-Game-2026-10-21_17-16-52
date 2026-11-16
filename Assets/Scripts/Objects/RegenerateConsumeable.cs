using UnityEngine;

// Regenerate consumable every x secodnds to avoid being stuck being small
public class RegenerateConsumeable : MonoBehaviour
{
    public Transform Consumable;
    private bool objectConsumed;
    public float regenerateTime;
    public float regenerateTime_Count;

    void Start()
    {
        regenerateTime_Count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        try
        {
            objectConsumed = !Consumable.gameObject.activeSelf;
        }
        catch
        {
            objectConsumed = true;
        }

        //Check if the object is deactivated
        if (objectConsumed)
        {
            // Add cooldown
            if (regenerateTime_Count < regenerateTime)
            {
                regenerateTime_Count += 1 * Time.deltaTime;
            }
            else
            {
                Consumable.gameObject.SetActive(true);

                regenerateTime_Count = 0;
            }
        }
        




    }
}
