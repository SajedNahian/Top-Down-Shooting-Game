using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    private GameObject player;
    private Animator anim;
    private bool inHitAnim = false;
    private bool attacking = false;
    private float speed = 4f;
    private int health = 6;

	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (health > 0)
        {
            updateBase();
        }
	}

    void updateBase ()
    {
        transform.LookAt(player.transform);
        if (Vector3.Distance(transform.position, player.transform.position) > 2f && !attacking)
        {
            if (!inHitAnim)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
        }
        else
        {
            if (!attacking)
            {
                attacking = true;
                anim.SetTrigger("attack");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && health > 0)
        {
            health--;
            if (health == 0)
            {
                GetComponent<BoxCollider>().isTrigger = true;
                anim.Play("ZombieDie");
                Destroy(this.gameObject, 4f);
            } else
            {
                Destroy(other.gameObject);
                if (!inHitAnim)
                {
                    anim.SetTrigger("hit");
                    inHitAnim = true;
                }
            }
        }
    }

    public void hitAnimOver ()
    {
        inHitAnim = false;
    }

    public void attackAnimOver ()
    {
        attacking = false;
    }

    public void checkIfHit ()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            Debug.Log("Successful Attack!");
        }
    }
}
