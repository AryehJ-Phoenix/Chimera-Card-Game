using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health = 0;

    public void ChangeHealth(float amount)
    {
        health += amount;

        if (health <= 0)
        {
            print(name + " killed");
            Destroy(this.gameObject);
        }
    }
}
