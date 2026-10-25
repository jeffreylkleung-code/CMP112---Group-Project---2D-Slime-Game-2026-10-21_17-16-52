using UnityEngine;

public class EnemyDetectFront : MonoBehaviour
{

    //Checks if there is a object infront of it and signals parent on what object it is 

    private EnemyMovementController parent;

    void Start()
    {

        parent = GetComponentInParent<EnemyMovementController>();

    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{

    //    parent.DetectJump(collision);

    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        parent.DetectJump(true, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        parent.DetectJump(false, collision);
    }


}
