using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crowd : MonoBehaviour {

    public CullingGroup culling_group;
    public Camera target_cam;

    public BoundingSphere[] bounding_spheres;
    public float[] bounding_distances;
    public bool draw_bounding_spheres;

    public Agent agent_prefab;
    public int max_agents = 1000;

    public Bounds bounds;
    public List<Agent> agents = new List<Agent>();

    public int forward_hash;

    Agent curr_agent;

    NavMeshHit hit;
    
	void Awake () {
        agent_prefab.CreatePool( max_agents );
        culling_group = new CullingGroup();
	}

    void SpawnGroup () {
        Vector3 new_pos;
        for ( int i = 0; i < max_agents; ++i  ) {
            new_pos  = transform.position + bounds.center + new Vector3( Random.Range( -1f, 1f ) * bounds.extents.x,
                                                    0f,
                                                    Random.Range( -1f, 1f ) * bounds.extents.z);
            Agent agent = agent_prefab.Spawn( new_pos );
            agent.navmesh_agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
            agent.navmesh_agent.avoidancePriority = 80;        
            agent.character.SetActive( false );       
            //agent.transform.SetParent( transform );
            agents.Add( agent );            
        }
    }

    void Start () {
        forward_hash = Animator.StringToHash( "Forward" );
        SpawnGroup();
        
        culling_group.targetCamera = target_cam;
        
        culling_group.onStateChanged += OnStateChanged;

        bounding_spheres = new BoundingSphere[agents.Count];
        for ( int i = 0; i < agents.Count; ++i ) {
            BoundingSphere bounding_sphere = new BoundingSphere( agents[i].transform.position, 1.5f );
            bounding_spheres[i] = bounding_sphere;
        }        
       
        culling_group.SetBoundingSpheres( bounding_spheres );

        culling_group.SetDistanceReferencePoint( target_cam.transform );
        culling_group.SetBoundingDistances( bounding_distances );
	}

    void Update () {
        for ( int i = 0; i < agents.Count; ++i ) {
            curr_agent = agents[i];
            // Update bouding spheres
            bounding_spheres[i].position = curr_agent.transform.position;
            // AI
            if ( curr_agent.navmesh_agent.remainingDistance < 4f ) {
                if ( NavMesh.SamplePosition( curr_agent.transform.position + Random.insideUnitSphere * 10f, out hit, 1f, NavMesh.AllAreas ) ) {
                    curr_agent.navmesh_agent.SetDestination( hit.position );        
                }        
            }
            // Other
            if ( curr_agent.in_view ) {
                curr_agent.animator.SetFloat( forward_hash,
                                         curr_agent.navmesh_agent.velocity.magnitude,
                                         0.1f,
                                         Time.deltaTime );        
            }
        }        
    }

    public void OnStateChanged ( CullingGroupEvent evt ) {
        Agent agent = agents[evt.index];
        if ( evt.hasBecomeVisible ) {
            agent.in_view = true;
            agent.navmesh_agent.obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;
            agent.navmesh_agent.avoidancePriority = 50;
            agent.character.SetActive( true );
        } else if ( evt.hasBecomeInvisible ) {
            agent.in_view = false;
            agent.navmesh_agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
            agent.navmesh_agent.avoidancePriority = 80;
            agent.character.SetActive( false );
        }
    }
    
    void OnDrawGizmos () {
        Gizmos.DrawWireCube( transform.position + bounds.center, bounds.size );
    }

    void OnDrawGizmosSelected () {
        if ( bounding_spheres == null || !draw_bounding_spheres ) {
            return;
        }
        Gizmos.color = Color.yellow;
        for ( int i = 0; i < bounding_spheres.Length; ++i ) {
            Gizmos.DrawSphere( bounding_spheres[i].position, bounding_spheres[i].radius );
        }
    }

    void OnDestroy () {
        culling_group.Dispose();
        culling_group = null;
    }
}
