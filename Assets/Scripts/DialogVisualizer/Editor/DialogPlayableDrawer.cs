using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using UnityEngine.Experimental.Director;

// Example of how to implement a custom drawing method for a specific playable type
// as part of the Graph Visualizer tool.
[CustomPlayableDrawer(typeof(DialogPlayable))]
public class DialogPlayableDrawer {
    
    public static void OnGUI ( Rect position, DialogPlayable p ) {

        string style_string = "flow node 6";

        GUIStyle nodeStyle = new GUIStyle( style_string );
        string title = "";
        
        if ( p != null ) {
            title = p.name ?? "Node";

            if ( p.content != null ) {
                style_string = "flow node 3";
            }

            if ( DialogRenderer.selected_node == p ) {
                style_string += " on";
            }

            nodeStyle = new GUIStyle( style_string );
            
            GUILayout.BeginArea( position, title, nodeStyle );

            GUILayout.BeginVertical();

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.FlexibleSpace();
        
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label( DialogRenderer.dialog_bubble, GUILayout.Width( 64f ) );
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.EndVertical();
        
            GUILayout.EndArea();
            
        } else {
            GUI.Label( position, "", nodeStyle );
        }
        
        
    }
}