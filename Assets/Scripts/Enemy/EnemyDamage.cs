using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    private EnemyController parent;


    //Dealing damage
    public bool DealDamageOnContact;
    public float contactDamage;
    IDamageable DamageableTarget;

    void Start()
    {
        parent = GetComponentInParent<EnemyController>();
    }

    //Deal damage on contact with the target
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DealDamageOnContact)
        {
            if (collision.gameObject.CompareTag(parent.targetTag))
            {
                Debug.Log(collision); // DEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUG

                if (collision.gameObject.TryGetComponent(out IDamageable i))
                {
                    DamageableTarget = collision.gameObject.GetComponent<IDamageable>();

                    DamageableTarget.Damage(contactDamage);
                }
            }
        }
    }

    




}
