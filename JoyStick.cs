using UnityEngine;
using System.Collections;
using GeekGame.Input;

public class JoyStick : MonoBehaviour
{
    private Animator anim;
    private float speed = .08f;
    private bool isRunning = false;
    [SerializeField]
    private GameObject gunStance1, gunStance2, bullet;
    [SerializeField]
    private Transform whereToShootFrom;
    private bool canFire = true;
    private float whenCanFire;
    private float howLongBtwnBullets = .1f;
    public AudioClip gunSound;
    private AudioSource audioSrc;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        updateGunStance();
        whenCanFire = Time.deltaTime;
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        updateCanFire();
        updateMovement();
        updateGunStance();
    }

    private void updateCanFire ()
    {
        if (!canFire && Time.time > whenCanFire)
        {
            canFire = true;
        }
    }

    private void updateMovement ()
    {
        transform.Translate(new Vector3(JoystickMove.instance.H, 0f, JoystickMove.instance.V) * speed * Time.deltaTime, Space.World);
        transform.LookAt(transform.position + new Vector3(JoystickRotate.instance.H, 0f, JoystickRotate.instance.V));

        float translation = Mathf.Abs(JoystickMove.instance.H) + Mathf.Abs(JoystickMove.instance.V);
        if (translation != 0)
        {
            anim.SetBool("isRunning", true);
            isRunning = true;
            if (JoystickMove.instance.H + JoystickMove.instance.V < 0)
            {
                anim.SetBool("runningForwards", false);
            } else
            {
                anim.SetBool("runningForwards", true);
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("runningForwards", false);
            isRunning = false;
        }

        if (JoystickFire.instance.Fire && canFire && isRunning) 
        {
            shootBullet();   
        }
    }

    private void updateGunStance ()
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

    private void shootBullet ()
    {
        canFire = false;
        audioSrc.PlayOneShot(gunSound);
        whenCanFire = Time.time + howLongBtwnBullets;
        GameObject bulletGO = Instantiate(bullet, whereToShootFrom.transform.position, transform.rotation) as GameObject;
        bulletGO.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
        Destroy(bulletGO, 2f);
    }
}