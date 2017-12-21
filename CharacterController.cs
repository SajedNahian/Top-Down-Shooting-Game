using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    private Animator anim;
    private float speed = 10f;
    private bool isRunning;
    [SerializeField]
    private GameObject gunStance1, gunStance2;
	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        gunStance2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        //playerMovement();
        gunStanceUpdate();
        playerLooking();
    }

    private void playerMovement ()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        translation *= Time.deltaTime;
        transform.Translate(0, 0, translation);

        if (translation != 0)
        {
            anim.SetBool("isRunning", true);
            isRunning = true;
        }
        else
        {
            anim.SetBool("isRunning", false);
            isRunning = false;
        }
    }
    private void playerLooking ()
    {
        
    }

    private void gunStanceUpdate ()
    {
        if (isRunning)
        {
            gunStance2.SetActive(true);
            gunStance1.SetActive(false);
        }
        else
        {
            gunStance2.SetActive(false);
            gunStance1.SetActive(true);
        }
    }
}
