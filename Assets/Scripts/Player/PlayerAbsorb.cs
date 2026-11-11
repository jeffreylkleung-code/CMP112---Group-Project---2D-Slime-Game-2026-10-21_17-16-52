using UnityEngine;

public class PlayerAbsorb : MonoBehaviour
{

    public float PlayerSize;

    private float InitalSize;
    private float newPlayerSize;

    IAbsorbable absorbableTarget;
    void Start()
    {
        // set intial size so it doesnt get too big 
        InitalSize = (gameObject.transform.localScale.x + gameObject.transform.localScale.y + gameObject.transform.localScale.z) / 3; 
    }

    // when target collides, absorb target
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IAbsorbable i))
        {
            absorbableTarget = collision.gameObject.GetComponent<IAbsorbable>();
            PlayerSize += absorbableTarget.Absorb(PlayerSize); // in enemy controller, if player size is smaller than enemy, it wont absorb
            updateSize();
        }
    }
    void updateSize()
    {
        newPlayerSize = (InitalSize + PlayerSize);
        gameObject.transform.localScale = new Vector3(newPlayerSize, newPlayerSize, newPlayerSize);

    }

}
