using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public Vector3 windDirection = Vector2.left;
    public float minSpeed, windSpeed, resetRadius;
    Transform[] clouds;
    float[] speeds;


    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 25;
    }
    void Start()
    {
        clouds = new Transform[transform.childCount];
        speeds = new float[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            clouds[i] = transform.GetChild(i);
            speeds[i] = Random.value;
        }

    }

    void Update()
    {
        float r2 = resetRadius * resetRadius;
        for(int i = 0; i < transform.childCount; i++)
        {
            float speed = Mathf.Lerp(minSpeed, windSpeed, speeds[i]);
            clouds[i].position += windDirection * speed;
            if (clouds[i].localPosition.sqrMagnitude > r2)
            {
                clouds[i].position = -clouds[i].position;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, resetRadius);
    }
}

   /* 
    void Update()
    {
        var r2 = resetRadius * resetRadius;
        for (var i = 0; i < speeds.Length; i++)
        {
            var cloud = clouds[i];
            var speed = Mathf.Lerp(minSpeed, windSpeed, speeds[i]);
            cloud.position += windDirection * speed;
            if (cloud.localPosition.sqrMagnitude > r2)
            {
                cloud.position = -cloud.position;
            }
        }
    }

   
}
   */