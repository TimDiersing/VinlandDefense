using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private const float DESTROY_TIME = 0.6f;
    private float speed = 0;

    private float damage = 0;
    void Awake() {
        Destroy(gameObject, 5);
    }
    void Update() {
        transform.position += transform.up.normalized * speed * Time.deltaTime;
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            
        } 

        Destroy(gameObject, DESTROY_TIME * 1/speed);
    }
}
