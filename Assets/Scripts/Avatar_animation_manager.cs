using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar_animation_manager : MonoBehaviour
{
    public Animator animator;
    private Vector3 positive_offset = new Vector3(0.0f, 0.0f, /*1.0f*/3.0f);
    private Vector3 negative_offset = new Vector3(0.0f, 0.0f, /*-1.0f*/ -3.0f);
    private Vector3 forward_highest_distance_for_avatar, backward_highest_distance_for_avatar;
    Spawner s = new Spawner();
    private bool avatar_rotated = false;
    [HideInInspector]
    public bool configuration_done = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        s = GameObject.Find("Spawner").GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (configuration_done == false && s.spawner_configuration_done == true)
        {
            if (s.location == 0)
            {
                forward_highest_distance_for_avatar = s.ten_meters_from_Hololens + positive_offset;
                backward_highest_distance_for_avatar = s.ten_meters_from_Hololens + negative_offset;
            }
            else if (s.location == 1)
            {
                forward_highest_distance_for_avatar = s.twenty_five_meters_from_Hololens + positive_offset;
                backward_highest_distance_for_avatar = s.twenty_five_meters_from_Hololens + negative_offset;
            }
            else if (s.location == 2)
            {
                forward_highest_distance_for_avatar = s.fourty_meters_from_Hololens + positive_offset;
                backward_highest_distance_for_avatar = s.fourty_meters_from_Hololens + negative_offset;
            }
            configuration_done = true;
            animator.SetBool("IsWalking", true);
        }

        if ((forward_highest_distance_for_avatar.z - this.gameObject.transform.position.z) > 0.0f && avatar_rotated == false)
        {
            this.gameObject.transform.position += new Vector3(0.0f, 0.0f, 0.025f);
        }
        
        if ((this.gameObject.transform.position.z - backward_highest_distance_for_avatar.z) > 0.0f && avatar_rotated == true)
        {
            this.gameObject.transform.position -= new Vector3(0.0f, 0.0f, 0.025f);
        }

        if (((forward_highest_distance_for_avatar.z - this.gameObject.transform.position.z) <= 0.0f) || ((this.gameObject.transform.position.z - backward_highest_distance_for_avatar.z) <= 0.0f))
        {
            this.gameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
            avatar_rotated = !avatar_rotated;
        }
    }
}
