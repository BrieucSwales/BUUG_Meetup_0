using UnityEngine;
using UnityEngine.Experimental.Director;

[RequireComponent (typeof (Animator))]
public class BlendAnimatorController : MonoBehaviour {
    public AnimationClip clip;
    public RuntimeAnimatorController anim_controller;
 
    void Start () {
        // Wrap the clip and the controller in playables
        AnimationClipPlayable clip_playable = new AnimationClipPlayable( clip );
        AnimatorControllerPlayable controller_playable = new AnimatorControllerPlayable( anim_controller );
        AnimationMixerPlayable mixer = new AnimationMixerPlayable();
        mixer.SetInputs( new AnimationPlayable[] {clip_playable, controller_playable} );
        
        // Bind the playable graph to the player
        GetComponent<Animator>().Play( mixer );
    }
}