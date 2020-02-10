using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    int count = 0;
    public Transform player;
    public GameObject bulletPrefab;
    public static Vector3 direction;

    void Update()
    {
        if (PlayerController.hasTriggeredEnemy)
        {
            direction = transform.position - player.position;
            transform.LookAt(player);
        }
    }
    public IEnumerator BulletFiring()
    {
        //prefabs[count]=Instantiate(bulletPrefab, transform.position, transform.rotation);
        //count++;
        //yield return new WaitForSeconds(3f);
        //for(int i=0;i<count;i++)
        //{
        //    Destroy(prefabs[i]);
        //}
        if (PlayerController.hasTriggeredEnemy)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(2f);
            StartCoroutine("BulletFiring");
        }
    }
}

