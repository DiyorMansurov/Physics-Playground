using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulatedPhysics : MonoBehaviour
{
    private Scene _simulatedScene;
    private PhysicsScene _physicsScene;
    [SerializeField] private Transform InteractablesParent;
    [SerializeField] private float collisionRadius = 0.1f;

    private void Start() {
        InteractablesParent = gameObject.transform.parent.root;
        CreateSimulatedScene();
    }

    private void CreateSimulatedScene()
    {
        if (SceneManager.GetSceneByName("Simulated Physics").isLoaded)
        return;

        var parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        _simulatedScene = SceneManager.CreateScene("Simulated Physics", parameters);
        _physicsScene = _simulatedScene.GetPhysicsScene();

        foreach (Transform child in InteractablesParent)
        {
            if (child.CompareTag("Interactable"))
            {
                var SimulatedObject = Instantiate(child.gameObject, child.position, child.rotation);
                
                if (SimulatedObject.TryGetComponent(out Rigidbody rb))
                    Destroy(rb);

                if (SimulatedObject.GetComponent<Renderer>() != null)
                {
                    SimulatedObject.GetComponent<Renderer>().enabled = false;
                }
                SceneManager.MoveGameObjectToScene(SimulatedObject, _simulatedScene);

            }
        }
    }
    [SerializeField] private LineRenderer line;
    [SerializeField] private int maxPhysicalIterations;
    [SerializeField] private LayerMask collisionMask;

    public void SimulateTrajectory(Projectile projectile, Vector3 pos, Vector3 velocity)
    {
        var simulatedProjectile = Instantiate(projectile, pos, Quaternion.identity);
        simulatedProjectile.GetComponent<Renderer>().enabled = false;
        SceneManager.MoveGameObjectToScene(simulatedProjectile.gameObject, _simulatedScene);
        simulatedProjectile.Launch(velocity);

        line.positionCount = maxPhysicalIterations;

        
        line.SetPosition(0, pos);

        int pointIndex = 0;
        line.positionCount = 0;
        


        Collider[] hits = new Collider[1];

        for (int i = 0; i < maxPhysicalIterations; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime * 2f);

            Vector3 posNow = simulatedProjectile.transform.position;

            line.positionCount = pointIndex + 1;
            line.SetPosition(pointIndex, posNow);
            pointIndex++;

            if (_physicsScene.OverlapSphere(posNow, collisionRadius, hits, collisionMask,QueryTriggerInteraction.Ignore ) > 0)
            {
                break;
            }
}

        Destroy(simulatedProjectile.gameObject);
    }



}