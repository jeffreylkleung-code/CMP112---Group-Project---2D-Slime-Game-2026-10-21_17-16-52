using UnityEngine;

public class EnemyDetectFront : MonoBehaviour
{

    //Checks if there is a object infront of it and signals parent on what object it is 

    private EnemyController parent;

    void Start()
    {

        parent = GetComponentInParent<EnemyController>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        parent.DetectJump(true, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        parent.DetectJump(false, collision);
    }


}
