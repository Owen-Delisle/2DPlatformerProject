using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision: MonoBehaviour
{
    // Reference to Parent Player Script
    [SerializeField] MainPlayerScript mainPlayerScript;

    // Collider Tags
    const string waterCollider = "Water";
    const string squareMaceCollider = "SquareMace";

    // ================ Collision ===============

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case squareMaceCollider:
                StartCoroutine(DamageCollision());
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case waterCollider:
                Debug.Log("Im Wet!");
                break;
        }
    }

    private IEnumerator DamageCollision()
    {
        mainPlayerScript.spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1);
        mainPlayerScript.spriteRenderer.color = Color.white;
    }
}
