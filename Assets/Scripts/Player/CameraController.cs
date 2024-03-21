using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float distanceFromPlayerX = 5.0f;
    public float distanceFromPlayerY = 5.0f;
    public float heightAbovePlayer = 2.0f;

    private Vector3 cameraOffset;
    private Vector3 shakeOffset;

    public static CameraController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        cameraOffset = new Vector3(distanceFromPlayerX, heightAbovePlayer, distanceFromPlayerY);
        transform.position = playerTransform.position + cameraOffset + shakeOffset;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalShakeOffset = shakeOffset;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            shakeOffset = new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        shakeOffset = originalShakeOffset;
    }
}
