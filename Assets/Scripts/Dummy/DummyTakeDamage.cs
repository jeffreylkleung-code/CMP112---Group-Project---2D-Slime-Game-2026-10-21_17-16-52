using TMPro;
using UnityEngine;

public class DummyTakeDamage : MonoBehaviour, IDamageable
{
    public TextMeshPro DamageText;
    public float iFrame_Count;
    public float iFrame_Cooldown;
    
    int hitCount = 1;
    float TotalDamage = 0;

    private void Update()
    {
        if (iFrame_Count< iFrame_Cooldown)
        {
            iFrame_Count += 1 *Time.deltaTime;
        }
    }


    public void Damage(float DamageTaken)
    {
        if (iFrame_Count >= iFrame_Cooldown)
        {
            TotalDamage += DamageTaken;
            DamageText.text = "(" + hitCount + ")" + "Damage: " + DamageTaken.ToString() + "\n Total damage: " + TotalDamage;
            hitCount += 1;
            iFrame_Count = 0;
        }
    }
}
    