using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public abstract class EventEmiter : MonoBehaviour {

	protected UnityEvent changeEvent;
	
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
