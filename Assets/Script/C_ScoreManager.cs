using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ScoreManager : MonoBehaviour
{
    [SerializeField] Text g_Text ;
    public int g_IntPlayerScore ;

    // Start is called before the first frame update
    void Start()
    {
        g_IntPlayerScore = 0 ;
    }

    void OnTriggerEnter2D ( Collider2D trigger )
    {
        if ( trigger.gameObject.CompareTag("Enemy"))
        {
            g_IntPlayerScore ++ ;
            g_Text.text = g_IntPlayerScore.ToString();
        }
    }
}
