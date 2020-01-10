using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [SerializeField]
    float initialVelocity = 20;

    [SerializeField]
    float lifeTime = 4;

	void Start ()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * initialVelocity;

        Destroy(this.gameObject, lifeTime);
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

}
