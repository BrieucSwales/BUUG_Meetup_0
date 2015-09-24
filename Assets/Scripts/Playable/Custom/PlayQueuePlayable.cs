using UnityEngine;
using UnityEngine.Experimental.Director;
 
public class PlayQueuePlayable : AnimationMixerPlayable {
    public int current_clip_index = -1;
    public float time_to_next_clip;
 
    public void PrepareFrame ( FrameData info ) {
        Playable[] inputs = GetInputs();
 
        // Advance to next clip if necessary
        time_to_next_clip -= (float)info.deltaTime;
        if ( time_to_next_clip <= 0.0f ) {
            current_clip_index++;
            if ( current_clip_index < inputs.Length ) {
                AnimationClipPlayable current_clip = inputs[current_clip_index] as AnimationClipPlayable;
 
                // Reset the time so that the next clip starts at the correct position
                inputs[current_clip_index].time = 0;
                time_to_next_clip = current_clip.clip.length;
            } else {
                // Pause when queue is complete
                state = PlayState.Paused;
            }
        }
 
        // Adjust the weight of the inputs
        for ( int a = 0; a < inputs.Length; a++ ) {
            if ( a == current_clip_index ) {
                SetInputWeight( a, 1.0f );
            } else {
                SetInputWeight( a, 0.0f );
            }
        }
    }
}