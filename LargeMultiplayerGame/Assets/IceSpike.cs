using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IceSpike : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    private void Start()
    {
        transform.localScale = Vector3.one * Random.Range(.35f, 0.8f);
    }

    private void Update()
    {
        if (duration > 0) duration -= Time.deltaTime;
        else Destroy(this.gameObject);
    }
}
