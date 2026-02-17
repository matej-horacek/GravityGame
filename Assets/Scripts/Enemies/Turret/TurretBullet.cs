using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    [SerializeField] float speed = 1500f;
    [SerializeField] float damage = 5f;
    private bool hasHit = false;
    private Rigidbody rb;

    private Vector3 direction;
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Setup(Vector3 shootDirection) 
    {
        rb.linearVelocity = shootDirection.normalized * speed;
    }
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (hasHit)
            return;

        if (collider.CompareTag("Player")) 
        {
            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                hasHit = true;

                player.health -= damage * player.level;
                Debug.Log("Player hit with hp left :" + player.health);

                Destroy(gameObject);

            }
        }
        else if (collider.CompareTag("RWall") || collider.CompareTag("LWall") || collider.CompareTag("Floor") || collider.CompareTag("Roof") || collider.CompareTag("Back"))
        {
            hasHit = true;
            Debug.Log("Hit a wall");
            Destroy(gameObject);
        }
        else
        {
            hasHit = true;
            Debug.Log("Hit object with tag: " + collider.tag);
        }

    }
}
