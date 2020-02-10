using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody rb;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(EnemyController.direction, ForceMode.VelocityChange);
        StartCoroutine("SelfDestroy");
       // rb.AddExplosionForce(2000f, EnemyController.direction, 10f);
      //  rb.AddRelativeForce(EnemyController.direction,ForceMode.Impulse);
       // rb.AddRelativeForce(400, 0, 400);
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
