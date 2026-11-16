using UnityEngine;

// Regenerate consumable every x secodnds to avoid being stuck being small
public class RegenerateConsumeable : MonoBehaviour
{
    public Transform Consumable;
    public float regenerateTime;
    public float regenerateTime_Count;

    void Start()
    {
        regenerateTime_Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the object is deactivated
        if (!Consumable.gameObject.activeSelf)
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
