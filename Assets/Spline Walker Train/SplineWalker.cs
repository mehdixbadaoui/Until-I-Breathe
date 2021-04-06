using UnityEngine;

public class SplineWalker : MonoBehaviour {

	public BezierSpline spline;

	public bool lookForward;

	public float speed = 1f;

	public SplineWalkerMode mode;

	[HideInInspector] public float progress;
	private bool goingForward = true;

	private void Update () {
		if (goingForward) {
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

        Vector3 position = Vector3.MoveTowards(transform.localPosition, spline.GetPoint(progress), speed);
		transform.localPosition = position;

		if (lookForward) {
			transform.LookAt(position + spline.GetDirection(progress));
		}
	}
}