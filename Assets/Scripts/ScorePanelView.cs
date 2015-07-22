using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScorePanelView : MonoBehaviour {
	
    [SerializeField]GameModel model;
    [SerializeField]Text score;
    [SerializeField]Text name;
    [SerializeField]Image image;


	void Start() {
	    model = GameModel.Instance;
        model.Subscribe(Draw);
 	}

    void Draw() {
        if(model.status!=GameStatus.on_score) {
            this.gameObject.SetActive(false);
            return;
        }
        this.gameObject.SetActive(true);
        score.text = model.score.ToString();
        name.text = model.currentFigure.description;
        image.sprite = model.currentFigure.GetComponent<SpriteRenderer>().sprite;
        image.color = model.currentFigure.GetComponent<SpriteRenderer>().color;
    }
}
