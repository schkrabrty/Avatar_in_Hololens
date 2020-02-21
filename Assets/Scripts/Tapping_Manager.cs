using HoloToolkit.Unity.Buttons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapping_Manager : MonoBehaviour
{
    public Spawning_manager spawner;
    private Avatar_animation_manager aam;
    private bool configuration_done = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Quaternion toQuat = Camera.main.transform.localRotation;
        toQuat.x = 0;
        toQuat.z = 0;

        // Rotate this object's parent object to face the user.
        this.transform.rotation = toQuat;

        if (spawner.avatar_instantiated == true && configuration_done == false)
        {
            aam = GameObject.Find("Avatar Variant(Clone)").GetComponent<Avatar_animation_manager>();
            configuration_done = true;
        }
    }

    void OnSelect()
    {
        if (spawner.location == 0)
        {
            int[] choices = new int[] { 1, 2 };
            Next_Location_Calculation(choices);
        }
        else if (spawner.location == 1)
        {
            int[] choices = new int[] { 0, 2 };
            Next_Location_Calculation(choices);
        }
        else if (spawner.location == 2)
        {
            int[] choices = new int[] { 0, 1 };
            Next_Location_Calculation(choices);
        }
    }

    void Next_Location_Calculation (int[] choices_array)
    {
        int[] choices = choices_array;
        int howManyChoices = choices.Length;
        int RandomIndex = Random.Range(0, howManyChoices);
        int next_location = choices[RandomIndex];
        spawner.location = next_location;
        aam.animator.SetBool("IsWalking", false);
        spawner.cube_selected = true;
        aam.configuration_done = false;
    }
}
