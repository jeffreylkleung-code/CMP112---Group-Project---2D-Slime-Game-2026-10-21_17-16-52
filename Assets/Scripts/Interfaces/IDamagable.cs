using UnityEngine;

public interface IDamageable
{
    // This always exists and does not need to be attached to a object because it is not a monobehaviour 

    void Damage(float DamageTaken);

}
