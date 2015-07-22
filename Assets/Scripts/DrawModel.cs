using UnityEngine;
using System.Collections;

public class DrawModel : MonoBehaviour {

    [SerializeField] GameObject dotPrefab;
    GameObject canvas;
    public GameObject Canvas {
       get {
           return canvas;
       }
    }
    [SerializeField] Vector2 diameter;
    public Vector2 Diameter {
        get {
            if( diameter == Vector2.zero ) {
                GetCenterAndDiametr();
            }
            return diameter;
        }
    }
    [SerializeField] Vector2 center;
    public Vector2 Center {
        get {
            if( center == Vector2.zero ) {
                GetCenterAndDiametr();
            }
            return center;
        }
    }

    void Start() {
        CleanUp();
    }

    [ContextMenu ("clean")]
	public void CleanUp() {
        if(canvas!=null) {
            Destroy(canvas);
        }
        diameter = Vector2.zero;
        center = Vector2.zero;
        canvas = new GameObject();
        canvas.name = "Canvas";
        canvas.transform.parent = this.transform;
    }
    
    [ContextMenu ("Get Center And Diametr")]
    void GetCenterAndDiametr() {
        float x = canvas.transform.GetChild(0).transform.position.x;
        float y = canvas.transform.GetChild(0).transform.position.y;
        float xmin = canvas.transform.GetChild(0).transform.position.x;
        float xmax = canvas.transform.GetChild(0).transform.position.x;
        float ymin = canvas.transform.GetChild(0).transform.position.y;
        float ymax = canvas.transform.GetChild(0).transform.position.y;
        
        for (int i=1;i<canvas.transform.childCount;i++) {
            Vector3 dot = canvas.transform.GetChild(i).transform.position;
            x += dot.x;
            y += dot.y;
            if(dot.x<xmin) {
                xmin = dot.x;
            } else if(dot.x>xmax) {
                xmax = dot.x;
            }
            if(dot.y<ymin) {
                ymin = dot.y;
            } else if(dot.y>ymax) {
                ymax = dot.y;
            }
        }

        center = new Vector2(x,y)/canvas.transform.childCount;
        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = center;
        diameter = new Vector3(xmax-xmin,ymax-ymin);
    }

    public void AddMarker(Vector2 position) {
        GameObject marker = Instantiate(dotPrefab,position,Quaternion.identity) as GameObject;
        marker.transform.parent = canvas.transform;
    }
}

