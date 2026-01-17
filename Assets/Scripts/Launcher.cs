using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Launcher : MonoBehaviour
{
    [SerializeField] private List<Projectile> projectiles = new List<Projectile>();
    private Projectile projectilePrefab;
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private SimulatedPhysics simulatedPhysics;
    private InputPlayerActions playerInput;
    private Vector2 rotationInput;
    [SerializeField] private float rotationSpeed = 5f;
    private float Xrotation = 0f;
    private float Yrotation = 0f;
    private bool isSlowed = false;
    private void Awake() {
        playerInput = new InputPlayerActions();
    }

    private void OnEnable() {
        playerInput.Player.Enable();    
        playerInput.Player.Launch.performed += ctx => LaunchProjectile();
        playerInput.Player.Rotate.performed += ctx => rotationInput = ctx.ReadValue<Vector2>();
        playerInput.Player.Rotate.canceled += ctx => rotationInput = Vector2.zero;
        playerInput.Player.Slow.performed += ctx => isSlowed = true;
        playerInput.Player.Slow.canceled += ctx => isSlowed = false;
    }

    private void OnDisable() {
        playerInput.Player.Launch.performed -= ctx => LaunchProjectile();
        playerInput.Player.Disable();
    }
    private void Start() {
        projectilePrefab = projectiles[0];
    }

    private void Update() {
        RotateCannon();
    }
    private void FixedUpdate() {
        CreateSimulatedScene();
    }

    private void RotateCannon()
    {
        if (isSlowed)
        {
            rotationSpeed = 20f;
        }else
        {
            rotationSpeed = 40f;
        }

        Xrotation = rotationInput.x * rotationSpeed * Time.deltaTime;
        Yrotation = rotationInput.y * rotationSpeed * Time.deltaTime;

        // Horizontal: world Y
        transform.Rotate(0f, Xrotation, 0f, Space.World);

        // Vertical: local X
        transform.Rotate(-Yrotation, 0f, 0f, Space.Self);
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

        projectilePrefab = projectiles[Random.Range(0, projectiles.Count)];
    }
}
