using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    [SerializeField] private GameObject TurretBullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GetComponent<TurretAim>().OnShoot += TurretAim_OnShoot;
    }
    private void TurretAim_OnShoot(object sender, TurretAim.OnShootEventArgs e)
    {
        //Debug.Log("Turret Shoot");
        GameObject newBullet = Instantiate(TurretBullet,e.spawnPosition, Quaternion.LookRotation(e.shootDirection));
        //Vector3 direction = (e.shootDirection).normalized;
        newBullet.GetComponent<TurretBullet>().Setup(e.shootDirection); 
        //Instantiate(TurretBullet, e.spawnPosition, Quaternion.LookRotation(e.shootDirection
        Destroy(newBullet, 5f);
    }
}
