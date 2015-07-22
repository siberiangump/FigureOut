using UnityEngine;
using System.Collections;

public class StartButtonView : MonoBehaviour {

    [SerializeField]GameStatus showOn; 

	void Start () {
	    GameModel.Instance.Subscribe(Draw);
	}

    void Draw() {
        if(GameModel.Instance.status!=showOn) {
            this.gameObject.SetActive(false);
            return;
        }
        this.gameObject.SetActive(true);
    }
}
