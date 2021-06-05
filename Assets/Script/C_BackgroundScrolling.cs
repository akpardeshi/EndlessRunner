using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BackgroundScrolling : MonoBehaviour
{
    [SerializeField] float g_FloatScrollSpeed ;
    [SerializeField] float g_FloatResetLocation ;
    [SerializeField] Vector3 g_ResetVector ;

    // Update is called once per frame
    void Update()
    {
        m_ScrollBack() ;
        m_ResetBackground() ;
    }

    void m_ScrollBack()
    {
        this.transform.Translate(  Vector3.left * Time.deltaTime * g_FloatScrollSpeed , Space.World );
    }

    void m_ResetBackground()
    {
        if ( this.transform.position.x <= g_FloatResetLocation )
        {
            this.transform.position = g_ResetVector ;
        }
    }
}
