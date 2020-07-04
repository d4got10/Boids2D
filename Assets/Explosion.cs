using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Awake()
    {
        Invoke(nameof(SelfDestroy), 1f);
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
