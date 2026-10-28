using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDetectTarget : MonoBehaviour
{
    private EnemyController parent;
    List<Collider2D> TargetsInArea = new List<Collider2D>();
    void Start()
    {
        parent = GetComponentInParent<EnemyController>();
    }

    // Add Target to list if it is in range
    private void OnTriggerStay2D(Collider2D detectedTargetCollision)
    {
        //Checks who has right tag
        if (detectedTargetCollision.gameObject.CompareTag(parent.targetTag)){

            //Ensures it doesnt keep adding the same target
            if (!TargetsInArea.Contains(detectedTargetCollision))
            {
                TargetsInArea.Add(detectedTargetCollision);

                
            }
            parent.UpdateCurrentTargetsInRange(TargetsInArea);
        }

    }
    // Remove target in range when it is outside of range
    private void OnTriggerExit2D(Collider2D detectedTargetCollision)
    {
        if (detectedTargetCollision.gameObject.CompareTag(parent.targetTag))
        {

            if (TargetsInArea.Contains(detectedTargetCollision))
            {
                TargetsInArea.Remove(detectedTargetCollision);

                parent.UpdateCurrentTargetsInRange(TargetsInArea);
            }


        }
    }

    


}
