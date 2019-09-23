using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class Starfield : MonoBehaviour
{
    public int maxStars = 100;
    public float starSize = 0.1f;
    public float starSizeRange = 0.5f;
    public float fieldWidth = 20f;
    public float fieldHeight = 25f;
    public bool colorize = false;

    Transform cameraTransform;

    float xOffset;
    float yOffset;

    ParticleSystem particle;
    ParticleSystem.Particle[] stars;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Awake()
    {
        stars = new ParticleSystem.Particle[maxStars];
        particle = GetComponent<ParticleSystem>();

        Assert.IsNotNull(particle, "Missing particles");

        xOffset = fieldWidth * 0.5f;
        yOffset = fieldHeight * 0.5f;

        for (int i = 0; i < maxStars; i++)
        {
            float randSize = Random.RandomRange(starSizeRange, starSizeRange + 1f);
            float scaledColor = (true == colorize) ? randSize - starSizeRange : 1f;

            stars[i].position = GetRandomInRectangle(fieldWidth, fieldHeight) + transform.position;
            stars[i].startSize = starSize * randSize;
            stars[i].startColor = new Color(1f, scaledColor, scaledColor, 1f);
        }

        particle.SetParticles(stars, stars.Length);
    }

    private void Update()
    {
        for (int i = 0; i < maxStars; i++)
        {
            Vector3 pos = stars[i].position + transform.position;

            if (pos.x < (cameraTransform.position.x - xOffset))
            {
                pos.x += fieldWidth;
            } else if(pos.x > (cameraTransform.position.x + xOffset)){
                pos.x -= fieldWidth;
            } if(pos.y < (cameraTransform.position.y - yOffset))
            {
                pos.y += fieldHeight;
            } else if(pos.y > (cameraTransform.position.y + yOffset))
            {
                pos.y -= fieldHeight;
            }

            stars[i].position = pos - transform.position;
        }

        particle.SetParticles(stars, stars.Length);
        
    }

    private Vector3 GetRandomInRectangle(float width, float height)
    {
        float x = Random.Range(0, width);
        float y = Random.Range(0, height);
        return new Vector3(x - xOffset, y - yOffset, 0);
    }
}

