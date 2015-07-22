using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class FigureModel : MonoBehaviour {

    public Vector2 center;
    public string description;

    void Awake() {
       float delta = Random.Range(-Config.Instance.sizeVariation,Config.Instance.sizeVariation);
       this.transform.localScale += new Vector3(delta,delta);
       this.transform.localRotation = new Quaternion(0,0,Random.Range(.0f,1),1);
       this.GetComponent<SpriteRenderer>().color = new Color(Random.Range(.7f,1),Random.Range(.7f,1),Random.Range(.7f,1),1);
       FindCenter();
    }

    void FindCenter() {
        if (transform.childCount==0) {
            return;
        }
        center = Vector2.zero;
        for (int i=1;i<transform.childCount;i++) {
            Vector3 dot = transform.GetChild(i).transform.position;
            center.x += dot.x;
            center.y += dot.y;
        }
        center/=transform.childCount;
    }

	public Vector2 GetSize() {
        return new Vector2(GetComponent<Collider2D>().bounds.size.x,GetComponent<Collider2D>().bounds.size.y)*.9f;
    }

    public void DestroyMe() {
        Destroy(this.gameObject);
    }

}
