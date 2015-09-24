using UnityEngine;

[RequireComponent (typeof (Animator))]
public class PlayQueue : MonoBehaviour {
    public AnimationClip[] clips_to_play;
 
    void Start () {
        PlayQueuePlayable play_queue = new PlayQueuePlayable();
        play_queue.SetInputs( clips_to_play );
 
        // Bind the queue to the player
        GetComponent<Animator>().Play( play_queue );
    }
}