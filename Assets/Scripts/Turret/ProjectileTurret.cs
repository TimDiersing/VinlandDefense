using UnityEngine;

public class ProjectileTurret : Turret
{
    [SerializeField] GameObject projectilePF;
    [SerializeField] float projectileSpeed;
    [SerializeField] float fireInaccuracy;
    [SerializeField] Sound[] fireSounds;
    private Transform Enemys;
    private Transform turretRotation;
    private Transform firePoint;
    private Animator animator;
    private Transform target;
    private float findInterval = 0.3f;
    private float fireTimer = 0;
    private float findTimer = 0;

    void Awake() {
        turretRotation = transform.GetChild(0);
        firePoint = turretRotation.GetChild(1);
        fireTimer = 1 / fireRate;
        Enemys = GameObject.Find("/Enemys").transform;
        animator = GetComponent<Animator>();

        foreach (Sound s in fireSounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.rawVolume;
            s.source.pitch = s.pitch;
        }
    }

    void Update() {
        if (fireTimer <= 0 && target) {
            FireTurret();
            fireTimer = 1 / fireRate;
        }

        if (findTimer <= 0) {
            SearchForTarget();
            findTimer = findInterval;
        }

        fireTimer -= Time.deltaTime;
        findTimer -= Time.deltaTime;
    }

    private void FireTurret() {
        animator.SetTrigger("Fire");

        // Play a random fire sound
        if (fireSounds.Length > 0)
            fireSounds[Random.Range(0, fireSounds.Length)].source.Play();

        // Create fire angle based off fireInaccuracy
        Quaternion fireAngle = Quaternion.Euler(turretRotation.eulerAngles + new Vector3(0, 0, Random.Range(-fireInaccuracy, fireInaccuracy)));
        GameObject projGO = Instantiate(projectilePF, firePoint.position, fireAngle);

        // Set projectile info
        Projectile proj = projGO.GetComponent<Projectile>();
        proj.SetSpeed(projectileSpeed);
        proj.SetDamage(damage);
    }

    void FixedUpdate() {

        // Rotate tward target
        if (target) {
            Vector3 vectorToTarget = target.transform.position - turretRotation.transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            turretRotation.transform.rotation = Quaternion.Slerp(turretRotation.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * turnSpeed);
        }
    }
    public void SetVolume(float volume) {
        
    }

    // Finds a target based on the search type
    void SearchForTarget() {

        Transform currentTarget = null;

        switch (searchType) {
            case 0:  // First
                foreach (Transform child in Enemys) {
                    if (child.GetComponent<Enemy>().IsAlive()) {
                        if (currentTarget != null) {
                            if (child.position.y < currentTarget.position.y) {
                                currentTarget = child;
                            }
                        } else {
                            currentTarget = child;
                        }
                    }
                }
                break;
            case 1: // Closest
                foreach (Transform child in Enemys) {
                    if (child.GetComponent<Enemy>().IsAlive()) {
                        if (currentTarget != null) {
                            if (Vector3.Distance(child.position, transform.position) < Vector3.Distance(currentTarget.position, transform.position)) {
                                currentTarget = child;
                            }
                        } else {
                            currentTarget = child;
                        }
                    }
                }
                break;
            case 2: // Farthest
                foreach (Transform child in Enemys) {
                    if (child.GetComponent<Enemy>().IsAlive()) {
                        if (currentTarget != null) {
                            if (Vector3.Distance(child.position, transform.position) > Vector3.Distance(currentTarget.position, transform.position)) {
                                currentTarget = child;
                            }
                        } else {
                            currentTarget = child;
                        }
                    }
                }
                break;
            case 3: // Strongest (Farthest strongest if multiple with same HP)
                foreach (Transform child in Enemys) {
                    if (child.GetComponent<Enemy>().IsAlive()) {
                        if (currentTarget != null) {
                            if (child.GetComponent<Enemy>().GetHealth() > currentTarget.GetComponent<Enemy>().GetHealth()) {
                                currentTarget = child;
                            } else if (child.GetComponent<Enemy>().GetHealth() == currentTarget.GetComponent<Enemy>().GetHealth()) {
                                if (child.position.y < currentTarget.position.y) {
                                    currentTarget = child;
                                }
                            }
                        } else {
                            currentTarget = child;
                        }
                    }
                }
                break;
        }

        target = currentTarget;
    }

}
