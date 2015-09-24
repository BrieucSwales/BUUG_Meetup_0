using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {

    public Crowd crowd;
    NavMeshAgent navmesh_agent;
    Vector3 dest;

    Animator animator;
    public GameObject character;

    public int forward_hash;

    public bool in_view;

    NavMeshHit hit;
    
	void Awake () {
        navmesh_agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
	}

    void Start () {
        forward_hash = Animator.StringToHash( "Forward" );
        navmesh_agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        navmesh_agent.avoidancePriority = 80;
        animator.enabled = false;
        character.SetActive( false );
        
        SetNextDestination();        
	}
	
	void Update () {
        Update_AI();
        if ( in_view ) {
            Update_Animator();
        }
	}

    void Update_Animator () {
        animator.SetFloat( forward_hash, navmesh_agent.velocity.magnitude, 0.1f, Time.deltaTime );
    }

    void Update_AI () {
        if ( navmesh_agent.remainingDistance < 4f ) {
            SetNextDestination();
        }
    }

    void SetNextDestination () {
//        dest = crowd.agents[Random.Range( 0, crowd.active_agents.Count )].transform.position;
        if ( NavMesh.SamplePosition( transform.position + Random.insideUnitSphere * 10f, out hit, 1f, NavMesh.AllAreas ) ) {
            dest = hit.position;
        }
        navmesh_agent.SetDestination( dest );        
    }

    public void OnVisible ( CullingGroupEvent evt ) {
        //Debug.Log( string.Format( "Agent {0} <color=green>appeared</color>", evt.index ) );
        in_view = true;
        navmesh_agent.obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;
        navmesh_agent.avoidancePriority = 50;
        animator.enabled = true;
        character.SetActive( true );

        crowd.active_agents.Add( this );
    }

    public void OnInvisible ( CullingGroupEvent evt ) {
        //Debug.Log( string.Format( "Agent {0} <color=red>disappeared</color>", evt.index ) );
        in_view = false;
        navmesh_agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        navmesh_agent.avoidancePriority = 80;
        animator.enabled = false;
        character.SetActive( false );

        crowd.active_agents.Remove( this );
    }

    public void OnDistanceBandChanged ( CullingGroupEvent evt ) {

    }

    void OnDrawGizmos () {
        if ( !in_view ) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere( transform.position, 0.5f );
        }
    }
    
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine( transform.position, dest );
        Gizmos.DrawWireSphere( dest, 0.5f );
    }
}
