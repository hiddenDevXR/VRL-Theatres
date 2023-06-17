using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPrint : MonoBehaviour
{
    public Renderer m_renderer;
    Material material;

    public bool pressed = true;
    float fade = 1f;

    private void Start()
    {
        material = m_renderer.material;
        InvokeRepeating("StartDissolving", 3f, 3f);
    }

    private void Update()
    {
        if(!pressed)
        {
            fade -= Time.deltaTime;

            if(fade <= 0f)
                Destroy(gameObject);

            material.SetFloat("_Fade", fade);
        }
    }

    void StartDissolving()
    {
        pressed = false;
    }

}
