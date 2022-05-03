using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMaterialManager : MonoBehaviour
{
    [SerializeField]
    Material m_SkyboxMat;
    float m_rotationValue;
    IEnumerator enumerator;
    private void Start()
    {
        enumerator = RotateSkybox();
        m_rotationValue = 360;
        m_SkyboxMat = RenderSettings.skybox;
        InvokeRepeating(enumerator.ToString(), 1,0.2f);
    }
    IEnumerator RotateSkybox() 
    {
        m_SkyboxMat.SetFloat("_Rotation",m_rotationValue);
        m_rotationValue -= 0.01f;
        yield return new WaitForEndOfFrame();
    }
}
