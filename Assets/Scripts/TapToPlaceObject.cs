using HoloToolkit.Unity.SpatialMapping;
using UnityEngine;

public class TapToPlaceObject : MonoBehaviour
{
    bool placing = false;
    public GameObject Starting_Text, Placing_Text;
    [HideInInspector]
    public bool configuration_done = false;
    [HideInInspector]
    public Vector3 position_of_plane;

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // If the sphere has no Rigidbody component, add one to enable physics.
        if (!this.GetComponent<Rigidbody>())
        {
            var rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        // On each Select gesture, toggle whether the user is in placing mode.
        placing = !placing;

        // If the user is in placing mode, display the spatial mapping mesh.
        if (placing)
        {
            SpatialMappingManager.Instance.DrawVisualMeshes = true;
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            SpatialMappingManager.Instance.DrawVisualMeshes = false;
            configuration_done = true;
            position_of_plane = this.transform.parent.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion toQuat = Camera.main.transform.localRotation;
        toQuat.x = 0;
        toQuat.z = 0;

        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (!placing)
        {
            if (Starting_Text.activeInHierarchy == false)
                Starting_Text.SetActive(true);
            
            Placing_Text.SetActive(false);
            // Rotate this object's parent object to face the user.
            this.transform.parent.rotation = toQuat;
        }

        if (placing)
        {
            Placing_Text.SetActive(true);
            Starting_Text.SetActive(false);

            // Do a raycast into the world that will only hit the Spatial Mapping mesh.
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMappingManager.Instance.LayerMask))
            {
                // Move this object's parent object to
                // where the raycast hit the Spatial Mapping mesh.
                this.transform.parent.position = hitInfo.point;

                // Rotate this object's parent object to face the user.
                this.transform.parent.rotation = toQuat;
            }
        }
    }
}
