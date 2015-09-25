using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CullingTest : MonoBehaviour {

    static CullingTest instance;
    static readonly object padlock = new object();

    public static CullingTest Instance {
        get {
            lock ( padlock ) {
                if ( instance == null ) {
                    instance = GameObject.FindObjectOfType( typeof( CullingTest ) ) as CullingTest;
                }
                if ( instance == null ) {
                    instance = new GameObject( typeof( CullingTest ).ToString() ).AddComponent<CullingTest>();
                }
                return instance;
            }
        }
    }
}
