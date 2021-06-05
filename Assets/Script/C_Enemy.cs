using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Enemy : MonoBehaviour
{
    [SerializeField] float g_FloatMoveSpeed ;
    [SerializeField] GameObject g_EnemyParticleSystem ;

    // Start is called before the first frame update
    void Start()
    {
        g_FloatMoveSpeed = 12.0f ;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate( g_FloatMoveSpeed * Vector2.left * Time.deltaTime );
    }

    void OnTriggerEnter2D ( Collider2D trigger )
    {
        if (!trigger.gameObject.CompareTag("EnemyDestroyer"))
        {
            Instantiate( g_EnemyParticleSystem , this.transform.position , Quaternion.identity );
        }
        
        this.gameObject.SetActive(false);
    }
}
