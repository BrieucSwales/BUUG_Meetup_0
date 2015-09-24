using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crowd : MonoBehaviour {

    public Agent agent_prefab;
    public int max_agents = 1000;

    public Bounds bounds;
    public List<Agent> agents = new List<Agent>();
    public List<Agent> active_agents = new List<Agent>();
    
	void Awake () {
        agent_prefab.CreatePool( max_agents );
	}

    void SpawnGroup () {
        Vector3 new_pos;
        for ( int i = 0; i < max_agents; ++i  ) {
            new_pos  = transform.position + bounds.center + new Vector3( Random.Range( -1f, 1f ) * bounds.extents.x,
                                                    0f,
                                                    Random.Range( -1f, 1f ) * bounds.extents.z);
            Agent agent = agent_prefab.Spawn( new_pos );
            agent.transform.SetParent( transform );
            agent.crowd = this;
            agents.Add( agent );
        }
    }

    void Start () {
        SpawnGroup();
        CullingTest.Instance.Init();
        CullingTest.Instance.agents = agents;
        CullingTest.Instance.Setup();
	}
	
	void Update () {
        CullingTest.Instance.UpdateData();
	}

    void OnDrawGizmos () {
        Gizmos.DrawWireCube( transform.position + bounds.center, bounds.size );
    }
}
