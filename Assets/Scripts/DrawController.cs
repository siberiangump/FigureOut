using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class DrawController : MonoBehaviour{
    
    [SerializeField]bool keyPointer=false;

    [SerializeField]DrawModel model;
    [SerializeField]bool onDraw=false;
    [SerializeField]float distance=20;
    Vector2 previous;
    Camera camera;

    void Awake() {
        camera= this.GetComponent<Camera>();
        previous=Vector2.zero;
    }

	void Update () {
        if(GameModel.Instance.status != GameStatus.play && !keyPointer) {
            return;
        }
        if(Input.GetMouseButtonDown(0)){
            onDraw=true;
            previous=Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0)){
            onDraw=false;
            //check
            if(!keyPointer) {
                if(model.Canvas.transform.childCount>5) {
                    GameModel.Instance.TryScore(model);
                }
                model.CleanUp();
            }
        }		
        if(Vector2.Distance(previous,Input.mousePosition)>distance && onDraw) {
            model.AddMarker(camera.ScreenToWorldPoint(Input.mousePosition));
            previous = Input.mousePosition;
        }
	}

}
