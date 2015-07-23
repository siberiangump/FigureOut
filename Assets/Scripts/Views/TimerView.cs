using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerView : MonoBehaviour {

    [SerializeField]GameModel model;
    [SerializeField]Text text;

	void Start() {
	    model = GameModel.Instance;
        model.Subscribe(Draw);
 	}

    void Draw() {
        if(model.status!=GameStatus.play) {
            this.gameObject.SetActive(false);
            return;
        }
        this.gameObject.SetActive(true);
    }

    void Update () {
	    text.text = model.timer.ToString("0.0");
	}
}
