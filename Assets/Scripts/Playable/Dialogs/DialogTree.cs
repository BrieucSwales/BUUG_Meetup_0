using UnityEngine;
using UnityEngine.Experimental.Director;
using System.Collections.Generic;

public class DialogTree : MonoBehaviour {
    DialogPlayable root;

    List<DialogPlayable> dialogs = new List<DialogPlayable>();
    DialogPlayer dialog_player;

    void Awake () {
        dialog_player = gameObject.AddComponent<DialogPlayer>();
        Debug.Log( dialog_player );
    }

    void Start () {
        InitGraph();

        DialogVisualizerClient.Show( root, gameObject.name );
    }

    void Update () {
        if ( Input.GetKeyDown( KeyCode.Space ) ) {
            Play();
        }
    }

    public void InitGraph () {
        
        dialogs.Clear();
        root = new DialogPlayable( "Root", null );
        dialogs.Add( root );

        DialogPlayable child = new DialogPlayable( "Hey!", "Hello,I'm a dialog node!" );
        Playable.Connect( child, root );
        dialogs.Add( child );

        child = new DialogPlayable( "Go away!", "Go away,you are not welcome!" );
        Playable.Connect( child, root );
        dialogs.Add( child );
    }

    public void Play () {
        dialog_player.Play( root );
    }    
}