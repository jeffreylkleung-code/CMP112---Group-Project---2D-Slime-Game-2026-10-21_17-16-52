using UnityEngine;

public interface IDamageable
{
    // This always exists and does not need to be attached to a object because it is not a monobehaviour 
    // implement interface to all damagable objects
    void Damage(float DamageTaken);

}
