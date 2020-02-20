using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class Spawner : MonoBehaviour
{
    public GameObject AvatarPrefab, HoloLensCamera, TestSphere, cube_button;
    private float timer = 0.0f;
    [HideInInspector]
    public bool avatar_instantiated = false;
    [HideInInspector]
    public bool cube_selected = false;
    private GameObject avatar;
    [HideInInspector]
    public int location = 0;
    [HideInInspector]
    public Vector3 ten_meters_from_Hololens, twenty_five_meters_from_Hololens, fourty_meters_from_Hololens;
    [HideInInspector]
    public bool spawner_configuration_done = false;
    private bool cube_instantiated = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 HololensCamera_without_y_position = HoloLensCamera.transform.position - new Vector3(0.0f, HoloLensCamera.transform.position.y, 0.0f);
        cube_button.SetActive(false);
        ten_meters_from_Hololens = HololensCamera_without_y_position + new Vector3(0.0f, 0.0f, /*1.0f*/10.0f);
        twenty_five_meters_from_Hololens = HololensCamera_without_y_position + new Vector3(0.0f, 0.0f, /*2.0f*/25.0f);
        fourty_meters_from_Hololens = HololensCamera_without_y_position + new Vector3(0.0f, 0.0f, /*3.0f*/40.0f);
        TestSphere = Instantiate(TestSphere, HoloLensCamera.transform.position + new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity).gameObject;
        TestSphere.SetActive(false);
        spawner_configuration_done = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 6.0f)
        {
            timer += Time.deltaTime;

            if (timer >= 4.0f && cube_instantiated == false)
            {
                TestSphere.SetActive(true);
                TestSphere.AddComponent<Rigidbody>();
                TestSphere.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                TestSphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                cube_instantiated = true;
                SpatialMappingManager.Instance.DrawVisualMeshes = false;
            }
        }

        else
        {
            if (avatar_instantiated == false)
            {
                Vector3 height_reduction_of_Test_Sphere = TestSphere.transform.position - new Vector3(0.0f, TestSphere.transform.position.y, 0.0f);
                TestSphere.transform.position += height_reduction_of_Test_Sphere + ten_meters_from_Hololens;
                Vector3 test_sphere_position = TestSphere.transform.position;
                Destroy(TestSphere);
                avatar = Instantiate(AvatarPrefab, test_sphere_position, Quaternion.identity).gameObject;
                avatar_instantiated = true;
                cube_button.SetActive(true);
            }
        }

        if (cube_selected == true)
        {
            Vector3 avatar_current_position_without_y_position = avatar.transform.position - new Vector3(0.0f, avatar.transform.localPosition.y, 0.0f);

            if (location == 1)
                avatar.transform.position += twenty_five_meters_from_Hololens - avatar_current_position_without_y_position;
            else if (location == 2)
                avatar.transform.position += fourty_meters_from_Hololens - avatar_current_position_without_y_position;
            else if (location == 0)
                avatar.transform.position += ten_meters_from_Hololens - avatar_current_position_without_y_position;

            cube_selected = false;
        }
    }
}
