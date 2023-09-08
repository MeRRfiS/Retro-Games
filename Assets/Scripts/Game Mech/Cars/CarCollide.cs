using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CarCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != TagConstants.PLAYER) return;

        AudioController.GetInstance().PlayExplosionSound();
        PlayerController.GetInstance().GetDamage();
        Destroy(gameObject);
    }
}
