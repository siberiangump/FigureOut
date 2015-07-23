using UnityEngine;
using System.Collections;

public class StartButtonController : MonoBehaviour {

	public void OnClick() {
        GameModel.Instance.StartGame();
    }

}
