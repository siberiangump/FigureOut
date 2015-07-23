using UnityEngine;
using System.Collections;

public class MouseShadowView : MonoBehaviour {
    
    [SerializeField]ParticleSystem particleSystem;
    [SerializeField]Camera camera;

    void Update () {
        if(Input.GetMouseButtonDown(0)){
            particleSystem.enableEmission=true;
        }
        if(Input.GetMouseButtonUp(0)){
            particleSystem.enableEmission=false;
        }	
        this.transform.position = camera.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position += new Vector3(0,0,-1);
        //
	}

}
