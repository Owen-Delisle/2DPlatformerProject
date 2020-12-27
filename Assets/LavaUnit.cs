using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaUnit : MonoBehaviour
{
    Rigidbody2D rb;

    int mapHeight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mapHeight = -(70 / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Despawn();
    }

    private void Despawn()
    {
        if (gameObject != null)
        {
            if (rb.velocity == Vector2.zero || transform.position.y <= mapHeight)
            {
                Destroy(gameObject);
            }
        }
    }
}
