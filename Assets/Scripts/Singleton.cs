using UnityEngine;
using System.Collections;

public abstract class Singleton<T>: MonoBehaviour where T:Singleton<T> {
	
    protected static T instance;
    public static T Instance
    {
        get{
            if (instance == null){
                instance = GameObject.FindObjectOfType<T>();
            }
			if (instance == null){
				SetDefaultPrefab();
			}
			if (instance == null){
				GameObject gmo = new GameObject();
				instance = gmo.AddComponent<T>();
				gmo.name = typeof(T).ToString();
			}
            return instance;
        }
    }

    void Awake() {
        T[] gmos =  GameObject.FindObjectsOfType<T>();
        if (gmos.Length > 1){
            DestroyImmediate(this.gameObject);
        }
		OnSingletonAwake();
    }

	protected virtual void OnSingletonAwake(){
	}

	private static void SetDefaultPrefab(){
		GameObject prefab = Resources.Load("Singleton/"+typeof(T).ToString()) as GameObject;
		if (prefab != null){
			GameObject gmo = Instantiate(prefab);
            gmo.name = gmo.name.Replace("(Clone)","");
			if (gmo != null){
				instance = gmo.GetComponent<T>();
			}
		}
	} 
	
}
