using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Crowd_Ui : MonoBehaviour {

    public Text max_agents, active_agents, ms;
    public Crowd crowd;

    string max_agents_text = "Max agents: {0}",
    active_agents_text = "Active agents: {0}",
    ms_text = "Last frame time: {0} ms";
	
	void Update () {
        max_agents.text = string.Format( max_agents_text, crowd.agents.Count );
        active_agents.text = string.Format( active_agents_text, crowd.culling_group.QueryIndices( true, null, 0 ) );
        ms.text = string.Format( ms_text, Mathf.Floor( Time.deltaTime * 1000f ) );
	}
}
