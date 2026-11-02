using Unity.VisualScripting;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{

    private GroupOfSpikes groupOfSpikes;
    public float contactDamage;
    private IDamageable damageableTarget;

    private void Start()
    {

        // Checks if it is attached to a group of spikes; makes it easier to change all the damage at once
        if (transform.parent)
        {
            groupOfSpikes = GetComponentInParent<GroupOfSpikes>();
            contactDamage = groupOfSpikes.contactDamage;
            Debug.Log(contactDamage);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(collision.gameObject.name);
        
        if (collision.gameObject.TryGetComponent(out IDamageable i))
        {

            damageableTarget = collision.gameObject.GetComponent<IDamageable>();

            damageableTarget.Damage(contactDamage);

        }
    }
}
