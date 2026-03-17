using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string Name;
    public float MaxHealth;
    public float Speed;
    public float Range;
    public float ViewAngleSide;
    public float ViewAngleUp;
    public float RotationSpeed;

    public virtual void Die()
    {
        Debug.Log(Name + " died");
    }
}
