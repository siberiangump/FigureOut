using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameModel : Singleton<GameModel> {

    public int score;
    public float baseTime;
    public float timer;
    public GameStatus status;

    [SerializeField]FigureModel[] figureSet;

    public FigureModel currentFigure;
    int currentFigureIndex=0;

    UnityEvent changeEvent;
	
    protected override void OnSingletonAwake(){
        status = GameStatus.on_start;
    }

    public void TryScore(DrawModel drawing) {
        if(Comparator.Instance.Overlap(currentFigure,drawing)>0.8f) {
            score++;
            currentFigure.DestroyMe();
            int index=Random.Range(0,figureSet.Length-1);
            while(index==currentFigureIndex) {
                index=Random.Range(0,figureSet.Length-1);
            }
            currentFigure = CloneFigureByIndex(index);
            baseTime*=Config.instance.timeDelta;
            timer = baseTime;
        };
    }

    void Update() {
        if(status==GameStatus.play) {
            timer -= Time.deltaTime;
            if(timer<0) {
                Endspiel();
            }
        }
    }

    public void StartGame() {
        if(currentFigure!=null) {
            currentFigure.DestroyMe();
        }
        currentFigure = CloneRandomFigure();
        status = GameStatus.play;
        score = 0;
        timer = baseTime = Config.Instance.timeOnFigure;
        Changed();
    }

    void Endspiel() {
        status = GameStatus.on_score;
        Changed();
    }

    public FigureModel CloneRandomFigure() {
        return CloneFigureByIndex(Random.Range(0,figureSet.Length-1));;
    }

    public FigureModel CloneFigureByIndex(int index) {
        currentFigureIndex = index;
        GameObject figure = Instantiate(figureSet[index].gameObject,Vector3.zero,Quaternion.identity) as GameObject;
        return figure.GetComponent<FigureModel>();
    }
   
	public void Subscribe(UnityAction action){
		if(changeEvent==null)changeEvent = new UnityEvent();
		changeEvent.AddListener(action);
		action();
	}
	
	public void Changed(){
		if(changeEvent==null){
			Debug.Log ("no listeners",this.gameObject); 
			return;
		}
		changeEvent.Invoke();
	}

}

public enum GameStatus {play, on_start, on_score}  