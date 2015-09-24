using UnityEngine;
using UnityEngine.Experimental.Director;
using System.Collections;

public class DialogPlayable : Playable {
    public string name, content;

    public DialogPlayable ( string name, string content ) {
        this.name = name;
        this.content = content;
    }

    public override void PrepareFrame( FrameData info ) {
        Debug.Log( "Preparing " + name );
    }    
}
