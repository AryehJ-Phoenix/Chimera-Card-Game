using UnityEngine;

public class Damager : MonoBehaviour
{

    public float damage = 0;
    public float lifetime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<EnemyHealth>())
        {
            collision.gameObject.GetComponentInParent<EnemyHealth>().ChangeHealth(-damage);
        }
    }
}
