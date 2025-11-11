using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public enum GunType
    {
        Pistol,
        Rifle,
        Shotgun
    }
    public GunType gunType;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float gunRange = 100f;
    /// <summary>
    /// Fires the gun when the fire input is performed.
    /// </summary>
    public void FireGun()
    {
        Ray ray = new Ray(bulletSpawnPoint.position, bulletSpawnPoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, gunRange))
        {
            Debug.Log($"Hit {hit.collider.name} at distance {hit.distance}");
        }
        InstantiateBullet();
    }
    /// <summary>
    /// Instantiates a bullet prefab at the gun's position.
    /// </summary>
    private void InstantiateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }
    /// <param name="dropForce">The force with which to drop the gun.</param>
    public void DropGun(Vector3 dropForce)
    {
       
    }

    void OnDrawGizmosSelected()
    {
        if (bulletSpawnPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(bulletSpawnPoint.position, 0.1f);
        }
    }
}
