using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript<T> : MonoBehaviour where T : EnemyData
{


    public T EnemyStats;
    protected float currentSpeed;
    public float CurrentHealth;
    [SerializeField] Transform Eyes;

    protected bool IsPlayerInView = false;
    protected Rigidbody rb;

    [SerializeField] GameObject playerObject;
    protected Player playerScript;
    [SerializeField] LayerMask playerLayer;
    public NavMeshAgent agent;
    private EnemyWander wanderComponent;


    protected virtual void Awake()
    {
        playerScript = playerObject.GetComponent<Player>();
        wanderComponent = GetComponent<EnemyWander>();
        rb = GetComponent<Rigidbody>();
        CurrentHealth = EnemyStats.MaxHealth;
        currentSpeed = EnemyStats.Speed;
        rb.useGravity = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FindPlayer();
        if(transform.position.magnitude - playerObject.transform.position.magnitude < EnemyStats.Range&& IsPlayerInView) 
        {
            Attack();
        }
        else
        {
            IsPlayerInView = false;
        }
    }
    public void FindPlayer() 
    {

        Vector3 dirToPlayer = (playerObject.transform.position - Eyes.position).normalized;
        float angleToPlayerSide = Vector3.SignedAngle((playerObject.transform.position - Eyes.position),transform.forward, transform.up);
        float angleToPlayerUp = Vector3.SignedAngle((playerObject.transform.position - Eyes.position),transform.forward, transform.right);

        if(Mathf.Abs(angleToPlayerSide) <= EnemyStats.ViewAngleSide && Mathf.Abs(angleToPlayerUp) <= EnemyStats.ViewAngleUp) 
        {
                if (wanderComponent != null)
                    wanderComponent.enabled = false;
                agent.SetDestination(playerObject.transform.position);
                IsPlayerInView = true;
                Debug.Log("is in up Angle");
            
        }
        else
        {
            if (wanderComponent != null)
                wanderComponent.enabled = true;
        }
    }

    protected void Attack() 
    {
        playerScript.TakeDamage(EnemyStats.Damage);
    }







    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, EnemyStats.Range);
        // Don't draw if EnemyStats hasn't been assigned yet to avoid errors in the editor
        if (EnemyStats == null) return;

        // Set your gizmo color. You can change this based on alert status later!
        Gizmos.color = Color.yellow;

        float viewDistance = EnemyStats.Range;

        // The starting point of the vision (add an eye offset here if you are using one)
        Vector3 origin = Eyes.position;

        // Calculate the 4 corners of the view frustum by rotating the forward vector
        // Quaternion.Euler takes (X-axis pitch, Y-axis yaw, Z-axis roll)
        // Note: Negative X pitches UP, Positive X pitches DOWN in Unity
        Vector3 topLeftRay = Quaternion.Euler(-EnemyStats.ViewAngleUp, -EnemyStats.ViewAngleSide, 0) * transform.forward * viewDistance;
        Vector3 topRightRay = Quaternion.Euler(-EnemyStats.ViewAngleUp, EnemyStats.ViewAngleSide, 0) * transform.forward * viewDistance;
        Vector3 bottomLeftRay = Quaternion.Euler(EnemyStats.ViewAngleUp, -EnemyStats.ViewAngleSide, 0) * transform.forward * viewDistance;
        Vector3 bottomRightRay = Quaternion.Euler(EnemyStats.ViewAngleUp, EnemyStats.ViewAngleSide, 0) * transform.forward * viewDistance;

        // 1. Draw the 4 lines stretching out from the enemy's eyes
        Gizmos.DrawRay(origin, topLeftRay);
        Gizmos.DrawRay(origin, topRightRay);
        Gizmos.DrawRay(origin, bottomLeftRay);
        Gizmos.DrawRay(origin, bottomRightRay);

        // 2. Connect the 4 corners at the end of the view distance to make a rectangle
        Vector3 topLeftEnd = origin + topLeftRay;
        Vector3 topRightEnd = origin + topRightRay;
        Vector3 bottomLeftEnd = origin + bottomLeftRay;
        Vector3 bottomRightEnd = origin + bottomRightRay;

        Gizmos.DrawLine(topLeftEnd, topRightEnd);       // Top edge
        Gizmos.DrawLine(topRightEnd, bottomRightEnd);   // Right edge
        Gizmos.DrawLine(bottomRightEnd, bottomLeftEnd); // Bottom edge
        Gizmos.DrawLine(bottomLeftEnd, topLeftEnd);     // Left edge

        // 3. (Optional) Draw a line connecting the enemy to the player for easy debugging
        if (playerObject != null)
        {
            // Turn the line red if the player is in view!
            Gizmos.color = IsPlayerInView ? Color.red : Color.blue;
            Gizmos.DrawLine(origin, playerObject.transform.position);
        }
    }
}
