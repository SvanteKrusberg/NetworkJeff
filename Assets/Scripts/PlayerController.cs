using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour {

    [SerializeField]
    float movementSpeed = 3f; // Unity-enheter per sekund

    [SerializeField]
    float rotationSpeed = 150f; // Grader per sekund

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform bulletSpawnPoint;

    public float timeBetweenShots = 0.5f;
    float timeSinceLastShot = 0f;
    public float thrust = 500f;
    public Rigidbody rb;
    public bool isGrounded = true;

    private void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    public override void OnStartLocalPlayer()
    {
        rb = GetComponent<Rigidbody>();
    } 

    void Update () {

        if (!isLocalPlayer)
        {
            return;
        }

        float yRotation = Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime;
        float zMovement = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

        Vector3 rotationVector = new Vector3(0, yRotation, 0);
        Vector3 movementVector = new Vector3(0, 0, zMovement);

        transform.Rotate(rotationVector);
        transform.Translate(movementVector);

        timeSinceLastShot += Time.deltaTime;

        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (timeSinceLastShot > timeBetweenShots)
            {
                CmdFire();
                timeSinceLastShot = 0;
            }
        }
        if (Input.GetAxisRaw("Jump") > 0 && isGrounded == true)
        {
            rb.AddForce(0, thrust, 0, ForceMode.Impulse);
        }

    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        NetworkServer.Spawn(bullet);
    }
}
