using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Director;

// Bridge between runtime and editor code: the graph created in runtime code can call GraphVisualizerClient.Show(...)
// and the EditorWindow will register itself with the client to display any available graph.
public class DialogVisualizerClient
{
    public delegate void UpdateGraphDelegate(DialogPlayable p, string title);
    public UpdateGraphDelegate updateGraph;

    private static DialogVisualizerClient s_Instance;

    public static DialogVisualizerClient instance
    {
        get
        {
            if (s_Instance == null)
                s_Instance = new DialogVisualizerClient();
            return s_Instance;
        }
    }

    public static void Show(DialogPlayable p, string title)
    {
        if (instance.updateGraph != null)
            instance.updateGraph(p, title);
    }
}
