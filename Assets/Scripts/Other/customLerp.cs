using UnityEngine;
using System.Collections;

public enum lerpType {linear, sin, cos, exp, smooth}

static public class customLerp
{
    //0 < currTime < duration.
    //Calculates lerp fraction depending on lerpType
    static public float getFraction(float currTime, float duration, lerpType type){
        currTime = Mathf.Clamp(currTime, 0, duration);

        //linear lerp
        float fraction = currTime / duration;

		if (type == lerpType.sin)
			fraction = Mathf.Sin (fraction * Mathf.PI * 0.5f);
		else if (type == lerpType.cos)
			fraction = 1f - Mathf.Cos (fraction * Mathf.PI * 0.5f);
		else if (type == lerpType.exp)
			fraction *= fraction;
		else if (type == lerpType.smooth)
			fraction = fraction * fraction * (3f - 2f * fraction);

		return fraction;
	}
}
