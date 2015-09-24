using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CullingTest : MonoBehaviour {

    public CullingGroup culling_group;
    public Camera target_cam;

    public List<Agent> agents = new List<Agent>();
    public BoundingSphere[] bounding_spheres;
    public float[] bounding_distances;

    static CullingTest instance;
    static readonly object padlock = new object();

    public bool draw_bounding_spheres;

    public static CullingTest Instance {
        get {
            lock ( padlock ) {
                if ( instance == null ) {
                    instance = GameObject.FindObjectOfType( typeof( CullingTest ) ) as CullingTest;
                }
                if ( instance == null ) {
                    instance = new GameObject( typeof( CullingTest ).ToString() ).AddComponent<CullingTest>();
                }
                return instance;
            }
        }
    }

    public void Init () {    
        culling_group = new CullingGroup();        
    }

	public void Setup () {
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

    public void UpdateData () {        
        for ( int i = 0; i < agents.Count; ++i ) {
            bounding_spheres[i].position = agents[i].transform.position;
        }
	}

    public void OnStateChanged ( CullingGroupEvent evt ) {
        if ( evt.hasBecomeVisible ) {            
            agents[evt.index].OnVisible( evt );            
        } else if ( evt.hasBecomeInvisible ) {
            agents[evt.index].OnInvisible( evt );            
        }

//        Debug.Log( string.Format( "Sphere distance band index: {0}", sphere.currentDistance ) );
    }

    void OnDestroy () {
        culling_group.Dispose();
        culling_group = null;
    }

    void OnDrawGizmosSelected () {
        if ( bounding_spheres == null || !draw_bounding_spheres ) {
           return;
        }
        Gizmos.color = Color.yellow;
        for ( int i = 0; i < bounding_spheres.Length; ++i ) {
            Gizmos.DrawWireSphere( bounding_spheres[i].position, bounding_spheres[i].radius );
        }
    }
}
