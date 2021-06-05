using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_EnemyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> g_EnemySpawnPoints = new List<GameObject>();
    int g_IntLoopCounter ;
    [SerializeField] GameObject g_Enemy ;
    int g_IntEnemyPoolCount ;

    C_Enemy [] g_EnemyPool ;

    // Time 
    float g_FloatTimeToInstantiateEnemies ;
    float g_FloatMinimumInstantiateTime ;
    float g_FloatCurrentTime ;
    float g_FloatCountDownTimerReduction ;
    float g_FloatWaitTime ;

    // Start is called before the first frame update
    void Start()
    {
        g_IntLoopCounter = g_EnemySpawnPoints.Count ;
        g_IntEnemyPoolCount = 30 ;
        g_EnemyPool = new C_Enemy[g_IntEnemyPoolCount];
        g_FloatTimeToInstantiateEnemies = 1.0f ;
        g_FloatMinimumInstantiateTime = 0.5f ;
        g_FloatCurrentTime = g_FloatTimeToInstantiateEnemies ;
        g_FloatCountDownTimerReduction = 0.05f ;
        g_FloatWaitTime = 5.0f ;

        m_CreateEnemyPool () ;
        StartCoroutine ( m_ReduceCountdownTimer() );
    }

    void m_CreateEnemyPool ()
    {
        GameObject l_TempGameObject ;

        for (int i = 0 ; i < g_IntEnemyPoolCount ; i++ )
        {
            l_TempGameObject = Instantiate(g_Enemy);
            l_TempGameObject.transform.position = this.transform.position;
            l_TempGameObject.gameObject.SetActive(false);
            g_EnemyPool[i] = l_TempGameObject.GetComponent<C_Enemy>() ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_InstantiateEnemies () ;
        m_CheckTimer() ;
    }

    void m_InstantiateEnemies ()
    {
        if ( g_FloatCurrentTime <= 0 )
        {
            // A function to instatia enemies
            m_SpawnEnemies() ;
            m_ResetSpawnPoints() ;
            g_FloatCurrentTime = g_FloatTimeToInstantiateEnemies ;
        }

        else
        {
            g_FloatCurrentTime -= Time.deltaTime ;
        }
    }

    void m_SpawnEnemies()
    {
        int l_RandomEnemyCount = Random.Range( 0 , g_IntLoopCounter );
        int l_IntSpawnPointIndex = Random.Range( 0 , g_IntLoopCounter );
        C_Enemy l_TempGameObject = m_ReturnActiveEnemy() ;

        for ( int i = 0 ; i < l_RandomEnemyCount ; i++ )
        {
            l_IntSpawnPointIndex = Random.Range( 0, g_IntLoopCounter );
            l_TempGameObject = m_ReturnActiveEnemy() ;

            if ( l_TempGameObject != null && !g_EnemySpawnPoints[l_IntSpawnPointIndex].GetComponent<C_SpawnPoint>().g_BoolHasSpawned )
            {
                l_TempGameObject.transform.position = g_EnemySpawnPoints[l_IntSpawnPointIndex].transform.position;
                l_TempGameObject.gameObject.SetActive(true);
            }

            else 
            {
                i-- ;
            }
        }
    }

    C_Enemy m_ReturnActiveEnemy()
    {
        for (int i = 0 ; i < g_IntEnemyPoolCount ; i++ )
        {
            if ( !g_EnemyPool[i].gameObject.activeSelf )
            {
                //g_EnemyPool[i].gameObject.SetActive(true);
                return g_EnemyPool[i] ;
            }
        }

        return null ;
    }

    void m_ResetSpawnPoints()
    {
        for (int i = 0 ; i < g_IntLoopCounter ; i++ )
        {
            g_EnemySpawnPoints[i].GetComponent<C_SpawnPoint>().g_BoolHasSpawned = false ;
        }
    }

    IEnumerator m_ReduceCountdownTimer ()
    {
        yield return new WaitForSeconds(g_FloatWaitTime);
        g_FloatTimeToInstantiateEnemies -= g_FloatCountDownTimerReduction ;
        StartCoroutine ( m_ReduceCountdownTimer() );
    }

    void m_CheckTimer()
    {
        if (g_FloatTimeToInstantiateEnemies < g_FloatMinimumInstantiateTime)
        {
            g_FloatTimeToInstantiateEnemies = g_FloatMinimumInstantiateTime ;
        }
    }
}
