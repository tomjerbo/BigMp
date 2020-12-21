using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class IceSpellHandler : MonoBehaviour
{
    
    private Vector3 destination;
    private Vector3 start;
    private float traveled;
    [SerializeField] private GameObject trailIceShard;
    [SerializeField] private GameObject mainIceShard;
    private float trailInterval = 0.1f;
    private float trailIntervalTimer = 0;
    private float spread = 0.5f;
    
    public void Setup(Vector3 _destination)
    {
        destination = _destination;
        start = transform.position;
    }

    private void SpawnTrailShard()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!Physics.Raycast(transform.position + transform.right * Random.Range(-spread, spread),
                transform.position - transform.up, out RaycastHit hit, 2f)) continue;
            
            Instantiate(trailIceShard, hit.point, new Quaternion(-90, 0, -180, 0));
            break;
        }
    }

    private void SpawnFinalShard()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!Physics.Raycast(transform.position + transform.right * Random.Range(-spread, spread),
                transform.position - transform.up, out RaycastHit hit, 2f)) continue;
            
            Instantiate(mainIceShard, hit.point, new Quaternion(-90, 0, -180, 0));
            break;
        }
    }


    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(start, destination, traveled * Time.fixedDeltaTime);
        spread += Time.fixedDeltaTime * 1.5f;

        if (traveled < 1)
        {
            traveled = Mathf.Clamp(traveled + Time.fixedDeltaTime, 0, 1);
            if (trailIntervalTimer < trailInterval) trailIntervalTimer += Time.fixedDeltaTime;
            else SpawnTrailShard();
        }
        else
        {
            SpawnFinalShard();
            Destroy(this.gameObject);
        }
    }
}
