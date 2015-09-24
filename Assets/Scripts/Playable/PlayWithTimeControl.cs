using UnityEngine;
using UnityEngine.Experimental.Director;
 
[RequireComponent (typeof (Animator))]
public class PlayWithTimeControl : MonoBehaviour {
    
    public AnimationClip clip; 
    Playable root;
    const float speed_factor = 0.1f;
    float horizontal_input;
 
    void Start () {
        root = new AnimationClipPlayable( clip );
        
        // Bind the playable to the player
        GetComponent<Animator>().Play( root );
 
        root.state = PlayState.Paused;
    }
 
    void Update () {
        // Control the time manually based on the input
        horizontal_input = Input.GetAxis("Horizontal");
        root.time += horizontal_input * speed_factor;
    }
}