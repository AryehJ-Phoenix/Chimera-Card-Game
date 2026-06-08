using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health = 0;

    public void ChangeHealth(float amount)
    {
        health += amount;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
