using UnityEngine;
using UnityEngine.Experimental.Director;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class PlayAnimation : MonoBehaviour {

    public AnimationClip clip;
    
	void Start () {
        // Wrap the clip in a playable
        AnimationClipPlayable clipPlayable = new AnimationClipPlayable( clip );
 
        // Bind the playable to the player
        GetComponent<Animator>().Play( clipPlayable );
	}	
}
