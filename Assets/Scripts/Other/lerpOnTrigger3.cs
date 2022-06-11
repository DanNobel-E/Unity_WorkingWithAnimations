using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LerpBackType{ONEWAY, TWOWAY, TWOSTEPS}
public enum LerpState{FORWARD, BACKWARD}

public class lerpOnTrigger3 : MonoBehaviour {
	public lerpType type;
	public LerpBackType lerpBackType;
	public LerpState lerpState;
	public bool performRotation;
	public Vector3 finalRotation;
	public bool performTranslation;
	public Vector3 finalTranslation;
	public bool performScale;
	public Vector3 finalScale;
	public Vector3 finalScale2;
	public bool performAlpha;
	public float finalAlpha;

    public bool loop;
    public bool triggerOnStart;
    public bool triggerOnEnable;
    public bool stopLerpOnCollisionEnter;
    public bool stopAfterStopTime;
    public float stopTime;
    float timeSinceStart;

    Quaternion startRotationQ, finalRotationQ;
	Vector3 startTranslation;
	Vector3 startScale;
	float startAlpha;
	Material mat;

	public float lerpDuration = 2f;
	float frac;
	public bool performLerp = false;

	public bool onTriggerDebug;

	void Start () {
		startScale 			= transform.localScale;
		startRotationQ 		= transform.localRotation;
		startTranslation 	= transform.position;
		Renderer renderer 	= GetComponent<Renderer> ();
		if(renderer != null){
			mat = renderer.material;
			startAlpha = mat.color.a;
		}
        if (triggerOnStart)
            OnTriggerEnter();
        if (stopAfterStopTime)
        {
            timeSinceStart = Time.time;
        }
	}

    private void OnEnable()
    {
        if (triggerOnEnable)
        {
            onTriggerDebug = true;
        }
    }

    void Update () {
		if (performLerp) {
			
			if (frac >= lerpDuration) {
				if (lerpBackType == LerpBackType.TWOWAY || lerpBackType == LerpBackType.TWOWAY) {
					lerpState = LerpState.BACKWARD;
				} else if (lerpBackType == LerpBackType.TWOSTEPS) {
					lerpState = LerpState.BACKWARD;
					startScale = finalScale2;
				}
				else {
					performLerp = false;
                    if (performAlpha && finalAlpha == 0)
                        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, finalAlpha);
                    if (loop)
                        OnTriggerEnter();
                    else
                        return;
				}
			} else if (frac < 0) {
				performLerp = false;
                if (loop)
                    OnTriggerEnter();
                else
                    return;
			}

			if (performScale) {
				transform.localScale = Vector3.Lerp (startScale, finalScale, customLerp.getFraction(frac, lerpDuration, type));
			}
			if (performRotation) {
				transform.localRotation = Quaternion.Slerp (startRotationQ, finalRotationQ, customLerp.getFraction(frac, lerpDuration, type));
			}
			if (performTranslation) {
				transform.position = Vector3.Lerp (startTranslation, startTranslation+finalTranslation, customLerp.getFraction(frac, lerpDuration, type));
			}
			if (performAlpha) {
				mat.color = Color.Lerp (new Color(mat.color.r, mat.color.g, mat.color.b, startAlpha), new Color(mat.color.r, mat.color.g, mat.color.b, finalAlpha), customLerp.getFraction(frac, lerpDuration, type));
			}

			if(lerpState == LerpState.FORWARD)
				frac += Time.deltaTime;
			else
				frac -= Time.deltaTime;
			
		}

        if (stopAfterStopTime && Time.time - timeSinceStart > stopTime)
            performLerp = false;

        if (onTriggerDebug) {
			onTriggerDebug = false;
			OnTriggerEnter ();
		}
		
	}

    void OnTriggerEnter()
    {
        if (performScale || performRotation || performTranslation || performAlpha)
        {
            lerpState = LerpState.FORWARD;
            frac = 0;
            performLerp = true;
        }
        if (performRotation)
        {
            finalRotationQ = Quaternion.Euler(finalRotation);
        }
    }
    void OnCollisionEnter()
    {
        if(stopLerpOnCollisionEnter)
            performLerp = false;
    }
}
