using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsEffect : MonoBehaviour
{
    public float cloudSpeed, cloudSpeedVariance, cloudEffectLength;
    public float timeBetweenClouds, MaxClouds;
    private float timeLeft, actualSpeed;
    public Vector3 cloudDirection;

    private void Update()
    {
        if(timeLeft < 0 && transform.childCount < MaxClouds)
        {
            //Create New Cloud
            int cloudType = Random.Range(1, 5);
            GameObject newCloud = (GameObject)Instantiate(Resources.Load($"Clouds/cloud{cloudType}"));    //Cast isn't actually redundant
            newCloud.transform.parent = transform;
            newCloud.transform.position = transform.position + (Vector3.right * Random.Range(-70,70));
            newCloud.GetComponent<MeshRenderer>().receiveShadows = false;
            timeLeft = timeBetweenClouds;
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }

        for(int i = 0; i<transform.childCount; i++)
        {
            GameObject currentChild = transform.GetChild(i).gameObject;
            if (JesterAnimController.moving)
            {
                actualSpeed = cloudSpeed;
            }
            else
            {
                actualSpeed = cloudSpeed / 2;
            }
            //currentChild.transform.Translate(-Vector3.forward * Time.deltaTime * (actualSpeed + Random.Range(-cloudSpeedVariance, cloudSpeedVariance)));
            currentChild.transform.position += cloudDirection * (actualSpeed + Random.Range(-cloudSpeedVariance,cloudSpeedVariance)) * Time.deltaTime;
            if (Vector3.Distance(transform.position, currentChild.transform.position) > cloudEffectLength)
            {
                Destroy(currentChild);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,transform.position + (cloudDirection * cloudEffectLength));
    }
}
