using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    [SerializeField] int moneyOnDeath;
    [SerializeField] int soulsOnDeath;
    [SerializeField] float chanceToDropSouls;
    [SerializeField] float health = 5;
    [SerializeField] Sound[] hitSounds;
    private Rigidbody2D rb;
    private bool alive;
    [SerializeField] float finishY;
    void Start() {
        rb = transform.GetComponent<Rigidbody2D>();
        finishY = GameManager.Instance.GetFinishY();
        alive = true;

        foreach (Sound s in hitSounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.rawVolume;
            s.source.pitch = s.pitch;
        }
    }

    void FixedUpdate() {
        if (alive) {
            rb.MovePosition(new Vector3(transform.position.x, transform.position.y - Time.deltaTime * moveSpeed, transform.position.z));
            if (transform.position.y <= finishY) {
                ReachedEnd();
            }
        }
    }

    public void TakeDamage(float damage) {

        // Play a random hit sound
        hitSounds[Random.Range(0, hitSounds.Length)].source.Play();

        if (health <= damage) {
            health = 0;
            Kill();
        } else {
            health -= damage;
        }
    }

    public void Kill() {
        alive = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GameManager.Instance.ChangeGold(moneyOnDeath);

        if (Random.Range(0f, 1f) <= chanceToDropSouls) {
            GameManager.Instance.AddSouls(soulsOnDeath);
        }

        Destroy(gameObject, 1f);
    }

    private void ReachedEnd() {
        GameManager.Instance.RemoveLives(1);
        Destroy(gameObject);
    }
    public float GetHealth() {
        return health;
    }
    public bool IsAlive() {
        return alive;
    }
}