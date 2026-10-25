using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public int Damage;
    public int Health;
    //public PLAYERHEALTHSCRIPT

    private void Update()
    {
        CheckHealth();
    }

    // Checks if the enemy has 0 hp
    private void CheckHealth()
    {
        if (Health <= 0)
        {
            print("Enemy is dead");
            Destroy(gameObject);
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        // NEEDS TO CREATE TAG SAME AS PLAYER
        if (collision.gameObject.tag == "Player")
        {
            // NEEDS TO MATCH PLAYER TAKE DAMAGE SCRIPT



        }
    }

}
