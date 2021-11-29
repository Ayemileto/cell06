using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyMateController : MonoBehaviour
{
    #region Variables

    [Header("Player Properties")]
    [SerializeField]
    private float playerSpeed = 2.0f;
    private float rotationSpeed = 4f;
    private bool isMoving = false;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform barrelTransform;
    [SerializeField]
    private Transform bulletParent;

    [SerializeField]
    private GameObject pollenPrefab;
    [SerializeField]
    private Transform pollenTransform;
    [SerializeField]
    private Transform pollenParent;



    [SerializeField]
    private float missDistance = 25f;
    private float stingerDamage = 10f;
    private float stingerRange = 50f;

    //private float jumpHeight = 1.0f;
    //private float gravityValue = -9.81f;



    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private PlayerControl playerInput;
    private Transform cameraMain;
    private Transform child;

    public GameObject audioManager;
    public AudioClip shootSound;
    public Animator playerMove;
    /* 
     public Animator playerShoot;
     public Animator pollinateFlower;
    */
    //public Animator playerIdle;
    //public GameObject pollenGrains;

    #endregion

    #region BuiltIn Methods
    private void Awake()
    {
        playerInput = new PlayerControl();
        controller = GetComponent<CharacterController>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        //pollenGrains = 
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }


    private void Start()
    {
        cameraMain = Camera.main.transform;
        child = transform.GetChild(0).transform;
    }

    void Update()
    {
        MovePlayer();
        ShootEnemy();
        PollinateFlower();
    }
    #endregion


    #region Custom Methods
    void MovePlayer()
    {

        #region (TBD)If we would ever jump
        /* isMoving = true;
   groundedPlayer = controller.isGrounded;
   if (groundedPlayer && playerVelocity.y < 0)
   {
       playerVelocity.y = 0f;
   }*/
        #endregion


        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();

        Vector3 move = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x);
        // move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);
        if (movementInput != Vector2.zero)
        {
            //  isMoving = true;
            Quaternion rotation = Quaternion.Euler(new Vector3(child.localEulerAngles.x, cameraMain.localEulerAngles.y, child.localEulerAngles.z));
            child.rotation = Quaternion.Lerp(child.rotation, rotation, Time.deltaTime * rotationSpeed);
            playerMove.SetTrigger("Move");
        }


    }


    void PollinateFlower()
    {
        if (playerInput.PlayerMain.Pollinate.triggered)
        {
            //playerShoot.SetTrigger("Pollinate");
            RaycastHit hit;
            GameObject pollen = GameObject.Instantiate(pollenPrefab, pollenTransform.position, Quaternion.identity, pollenParent);
            PollenGrains pollenController = pollen.GetComponent<PollenGrains>();

            if (Physics.Raycast(transform.position, -transform.up, out hit, stingerRange))
            {
                pollenController.target = hit.point;
                pollenController.hit = true;

            }
            else
            {
                pollenController.target = transform.position + (-transform.up) * missDistance;
                pollenController.hit = true;
            }

        }
    }



    void ShootEnemy()
    {
        if (playerInput.PlayerMain.Shoot.triggered)
        {
            // playerShoot.SetTrigger("Shoot");

            RaycastHit hit;
            GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
            StingerController stingerController = bullet.GetComponent<StingerController>();

            if (Physics.Raycast(cameraMain.position, cameraMain.forward, out hit, stingerRange))
            {
                stingerController.target = hit.point;
                stingerController.hit = true;

            }
            else
            {
                stingerController.target = cameraMain.position + cameraMain.forward * missDistance;
                stingerController.hit = true;
            }

            // Debug.Log("I am Shooting");


        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("flower"))
        {
            Debug.Log("Collide");
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.controller.tag == "flower")
        {
            Debug.Log("Pollinate");
        }
    }
    #endregion






}