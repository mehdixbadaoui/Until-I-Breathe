using UnityEngine;

public class SplineWalker : MonoBehaviour {

	public BezierSpline spline;

	public bool lookForward;

	public float speed = 1f;

	public SplineWalkerMode mode;
	public bool onTrain_GoingForward = false;
	public bool onFlyingCar_GoingForward = false;
	private PlayEventSounds playEvent;
	public float maxDist = 60f;
	public Vector3 distWithUni; 
	
	[HideInInspector] public float progress;
	private bool goingForward = true;
    private void Start()
    {
		playEvent = GameObject.FindGameObjectWithTag("uni").GetComponent<PlayEventSounds>(); 
    }
    private void Update () {
		if (goingForward) {
			distWithUni = playEvent.CalculateDistanceUniFromObject(this.gameObject.transform.position);
			
			//progress += (Time.deltaTime / duration);
			progress += (Time.deltaTime * speed / 100);
			if (progress > 1f) {
				
				if (mode == SplineWalkerMode.Once) {
					progress = 1f;
				}
				else if (mode == SplineWalkerMode.Loop) {
					progress -= 1f;
				}
				else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		}
		else {
			progress -= Time.deltaTime * speed / 100;
			if (progress < 0f) {
				progress = - progress;
				goingForward = true;
			}
		}

        //Vector3 position = spline.GetPoint(progress);
        //transform.localPosition = position;
        if (onTrain_GoingForward)
        {
            playEvent.RTPCGameObjectValueForTrain(distWithUni, maxDist, this.gameObject, "Train_exterieur_event", "TrainVolume");
        }

        else if (onFlyingCar_GoingForward)
        {
			playEvent.RTPCGameObjectValueForTrain(distWithUni, maxDist, this.gameObject, "Voiture_volante_event", "VoitureVolanteVolume");


        }
		//playEvent.RTPCGameObjectValueForTrain(distWithUni, maxDist, this.gameObject, "Voiture_volante_event", "VoitureVolanteVolume");

		Vector3 position = Vector3.MoveTowards(transform.localPosition, spline.GetPoint(progress), speed);
		
		transform.localPosition = position;

		if (lookForward) {
			transform.LookAt(position + spline.GetDirection(progress));
		}
	}
}