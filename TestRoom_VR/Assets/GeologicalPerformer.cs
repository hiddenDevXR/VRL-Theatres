using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeologicalPerformer : MonoBehaviour
{
    public float minDistance = 1f;
    public enum ActivationType { Animator, Animation, Shader };
    public ActivationType activationType;

    public string shaderParameterName;
    public float shaderParameterNewValue;
    public int targetIntensity;

    public Animator animator;
    public Animation m_animation;
    public Renderer renderer;
    public Light m_light;

    float distance = 0f;
    bool enableTracking = false;

    void Update()
    {
        if(TrackerCalibrator.steinRock != null)
        {
            distance = Vector3.Distance(TrackerCalibrator.steinRock.position, transform.position);

            Debug.Log("Distance: " + distance);

            if (distance < minDistance)
            {
                if (activationType == ActivationType.Animator)
                    SetAnimator(true);
                else if (activationType == ActivationType.Shader)
                    SetShaderParameter(shaderParameterNewValue);
                else if (activationType == ActivationType.Animation)
                    SetAnimation(true);
                
                TurnOnLights();
            }

            else
            {
                if (activationType == ActivationType.Animator)
                    SetAnimator(false);
                else if (activationType == ActivationType.Shader)
                    SetShaderParameter(0);
                else if (activationType == ActivationType.Animation)
                    SetAnimation(false);
            }
        }
    }

    void SetAnimator(bool newState)
    {
        animator.enabled = newState;
    }

    void SetAnimation(bool newState)
    {
        m_animation.enabled = newState;
    }

    void SetShaderParameter(float parameter)
    {
        renderer.material.SetFloat(shaderParameterName, parameter);
    }

    void TurnOnLights()
    {
        m_light.GetComponent<Animator>().enabled = true;
    }
}
