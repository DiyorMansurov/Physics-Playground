using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private SimulatedPhysics simulatedPhysics;

    private void Update() {
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile();
        }
    }

    private void FixedUpdate() {
        CreateSimulatedScene();
    }
    private void CreateSimulatedScene()
    {
        if (simulatedPhysics != null)
        {
            simulatedPhysics.SimulateTrajectory(projectilePrefab, launchPoint.position, launchPoint.right * launchForce);
        }
    }

    private void LaunchProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
        projectile.transform.parent = gameObject.transform.root;
        projectile.Launch(launchPoint.right * launchForce);
    }
}
