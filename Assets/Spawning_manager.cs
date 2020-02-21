using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning_manager : MonoBehaviour
{
    public TapToPlaceObject TapToPlaceObject;
    public GameObject AvatarPrefab, cube_button, Walking_Plane, Test_Plane;
    [HideInInspector]
    public Vector3 ten_meters_from_Hololens, twenty_five_meters_from_Hololens, fourty_meters_from_Hololens;
    [HideInInspector]
    public int location = 0;
    [HideInInspector]
    public bool cube_selected = false;
    private GameObject avatar;
    [HideInInspector]
    public bool avatar_instantiated = false;
    [HideInInspector]
    public bool spawner_configuration_done = false;
    private Vector3 plane_position, updated_plane_position;

    // Start is called before the first frame update
    void Start()
    {
        ten_meters_from_Hololens = new Vector3(0.0f, 0.0f, 10.0f);
        twenty_five_meters_from_Hololens = new Vector3(0.0f, 0.0f, 25.0f);
        fourty_meters_from_Hololens = new Vector3(0.0f, 0.0f, 40.0f);
        cube_button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TapToPlaceObject.configuration_done == true && avatar_instantiated == false)
        {
            plane_position = TapToPlaceObject.position_of_plane;
            updated_plane_position = plane_position - new Vector3(plane_position.x, 0.0f, plane_position.z);
            Instantiate(Walking_Plane, updated_plane_position, Quaternion.identity);
            Destroy(Test_Plane);
            avatar = Instantiate(AvatarPrefab, updated_plane_position + ten_meters_from_Hololens, Quaternion.identity);
            avatar_instantiated = true;
            spawner_configuration_done = true;
            cube_button.SetActive(true);
        }

        if (cube_selected == true)
        {
            if (location == 1)
                avatar.transform.position = updated_plane_position + twenty_five_meters_from_Hololens;
            else if (location == 2)
                avatar.transform.position = updated_plane_position + fourty_meters_from_Hololens;
            else if (location == 0)
                avatar.transform.position = updated_plane_position + ten_meters_from_Hololens;

            cube_selected = false;
        }
    }
}
