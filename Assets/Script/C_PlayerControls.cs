// Make player invisible for 0.5 sec after taking hit
// add sounds to player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class C_PlayerControls : MonoBehaviour
{
    //[SerializeField] float g_FloatMovementSpeed ;
    [SerializeField] float g_FloatMinMovement ;
    [SerializeField] float g_FloatMaxMovement ;
    [SerializeField] float g_FloatVerticalAxis ;
    [SerializeField] float g_FloatIncrementY ;
    [SerializeField] AudioClip g_HitAudio ;
    [SerializeField] AudioClip g_MoveAudio ;
    AudioSource g_AudioSource ;

    Rigidbody2D g_Rigidbody ;
    Vector3 g_PlayerPosition ;
    [SerializeField] GameObject g_ParticleSystem ;
    float g_FloatInvisibleTime ;
    float g_FloatCurrentInvisibleTime ;

    public bool g_BoolGameOver ;

    // Lives
    [SerializeField] UnityEngine.UI.Text g_LivesText ;
    int g_IntLives ;

    // Start is called before the first frame update
    void Start()
    {
        //g_FloatMovementSpeed = 100.0f ;
        g_FloatMinMovement = -6.0f ;
        g_FloatMaxMovement = 6.0f ;
        g_FloatIncrementY = 3.0f ;
        g_IntLives = 3 ;
        g_FloatInvisibleTime = 0.2f ;
        g_FloatCurrentInvisibleTime = 0.0f ;
        g_LivesText.text = g_IntLives.ToString() ;
        g_FloatVerticalAxis = Input.GetAxisRaw("Vertical");
        g_Rigidbody = this.GetComponent<Rigidbody2D>();
        g_AudioSource = this.GetComponent<AudioSource>();
        g_BoolGameOver = false ;
    }

    // Update is called once per frame
    void Update()
    {
        m_Move() ;
        m_ManageInvisibleTime () ;
    }

    void m_Move()
    {
       if ( Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) )
       {

           g_PlayerPosition = this.transform.position ;           
           g_PlayerPosition.y += g_FloatIncrementY ;
           
           if ( this.transform.position.y == g_FloatMaxMovement ) return ;

           GameObject l_ParticleSystem = Instantiate(g_ParticleSystem); 
           l_ParticleSystem.transform.position = g_PlayerPosition ;
           this.transform.position = g_PlayerPosition ;
           m_PlayMove();
       }

       if ( Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) )
       {
           g_PlayerPosition = this.transform.position ;
           g_PlayerPosition.y -= g_FloatIncrementY ;

           if ( this.transform.position.y == g_FloatMinMovement) return ;
           
           GameObject l_ParticleSystem = Instantiate(g_ParticleSystem); 
           l_ParticleSystem.transform.position = g_PlayerPosition ;
           this.transform.position = g_PlayerPosition ;           
           m_PlayMove ();
       }
    }

    void m_RediceLives()
    {
        if ( g_FloatCurrentInvisibleTime <= 0 )
        {
            g_IntLives -- ;            
        }
    }

    void OnTriggerEnter2D( Collider2D trigger )
    {
        m_PlayHit() ;
        m_RediceLives() ;
        g_LivesText.text = g_IntLives.ToString();
        
        if ( g_IntLives <= 0 )
        {
            StartCoroutine(m_GameOver());
        }

        g_FloatCurrentInvisibleTime = g_FloatInvisibleTime ;
        
    }

    IEnumerator m_GameOver()
    {
        g_BoolGameOver = true ;
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.8f);
    }

    void m_ManageInvisibleTime ()
    {
        if ( g_FloatCurrentInvisibleTime > 0 )
        {
            g_FloatCurrentInvisibleTime -= Time.deltaTime ;
        }           
    }

    void m_PlayMove ()
    {
        if ( !g_AudioSource.isPlaying )
        {
            g_AudioSource.volume = 1.2f ;
            g_AudioSource.pitch = 1.0f ;
            g_AudioSource.clip = g_MoveAudio ;
            g_AudioSource.Play () ;
        }
    }

    void m_PlayHit()
    {
        /*g_AudioSource.pitch = Random.Range(0.2f , 0.8f);
        g_AudioSource.clip = g_HitAudio ;
        g_AudioSource.volume = 0.4f ;*/
        AudioSource.PlayClipAtPoint( g_HitAudio , this.transform.position , 0.4f );
    }
}
