using UnityEngine;
using System.Collections;

public class Comparator : Singleton<Comparator>{
    
    [SerializeField] float ratio;
    [SerializeField] float dotRadius;

    bool figureAreAsymmetric = false;

    void Start() {
        dotRadius = Config.Instance.dotRadius;
    }

    public float Overlap(FigureModel figure, DrawModel draw) {
        int value=0;
        Collider2D figureCollider = figure.GetComponent<Collider2D>();
        Transform[] dots = draw.Canvas.GetComponentsInChildren<Transform>();
        CorrectPosition(figure, draw);
        CorrectSize(figure, draw);
        for (int i=0;i<dots.Length;i++) {
            float radius = dotRadius;
            if(figureAreAsymmetric) {
                radius*=1.5f;
            }
            if(Physics2D.OverlapCircle(dots[i].position,radius) == figureCollider) {
                value++;
            }
        }

        return (float)value / dots.Length;
    }

    void CorrectSize(FigureModel figure, DrawModel draw) {
        Vector2 drawSize = draw.Diameter;
        Vector2 figureSize = figure.GetSize();
        if(Mathf.Abs(drawSize.x - figureSize.x)>Mathf.Abs(drawSize.y - figureSize.y)) {
            float x = figureSize.x/drawSize.x;
            draw.Canvas.transform.localScale = new Vector3 (x,x,1);
        } else {
            float y = figureSize.y/drawSize.y;
            draw.Canvas.transform.localScale = new Vector3 (y,y,1);
        }
    }

    void CorrectPosition(FigureModel figure, DrawModel draw) {
        Transform[] dots = draw.Canvas.GetComponentsInChildren<Transform>();
        if(figure.center == Vector2.zero) {
            figureAreAsymmetric = false;
        }else {
            figureAreAsymmetric = true;
        }
        for (int i=0;i<dots.Length;i++) {
            dots[i].position += new Vector3((draw.Center.x)*-1,(draw.Center.y)*-1);
            if(figureAreAsymmetric) {
                dots[i].position += new Vector3(figure.center.x,figure.center.y);
            }
        }
        draw.Canvas.transform.position = Vector3.zero;
    }

}
