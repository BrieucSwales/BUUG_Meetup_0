using UnityEngine;
using UnityEngine.Experimental.Director;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class MixAnimation : MonoBehaviour {

    public AnimationClip clip_0, clip_1;
    public float weight;
    
    AnimationMixerPlayable mixer;
    
	void Start () {
        mixer = new AnimationMixerPlayable();
        mixer.SetInputs( new []{ clip_0, clip_1 }  );

        GetComponent<Animator>().Play( mixer );
	}

    void Update () {
        weight = ( Time.time % 10 ) / 10f;
        mixer.SetInputWeight( 0, weight );
        mixer.SetInputWeight( 1, 1f - weight );
    }
}
