using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Crowd_Ui : MonoBehaviour {

    public Text max_agents, active_agents, ms;
    public Crowd crowd;
    
	void Start () {
        InvokeRepeating( "DisplayMS", 0f, 2f );
	}
	
	void Update () {
        max_agents.text = string.Format( "Max agents: {0}", crowd.agents.Count );
        active_agents.text = string.Format( "Active agents: {0}", crowd.active_agents.Count );
	}

    void DisplayMS () {
        ms.text = string.Format( "{0} ms", Mathf.Floor( Time.deltaTime * 1000f ) / 1000f );
    }
}
