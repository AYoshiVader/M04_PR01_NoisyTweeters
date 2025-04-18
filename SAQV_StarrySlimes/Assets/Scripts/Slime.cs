using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    bool _hasDied;

    void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
    }

    void Awake()
    {
        float red = Random.Range(0f, 1f);
        float green = Random.Range(0f, 1f);
        float blue = Random.Range(0f, 1f);
        if (red > green && red >= blue)
        {
            red = 1f;
            if (green > blue)
            {
                blue = 0f;
            }
            else
            {
                green = 0f;
            }
        }
        else if (green > blue)
        {
            green = 1f;
            if (blue > red)
            {
                red = 0f;
            }
            else
            {
                blue = 0f;
            }
        } 
        else
        {
            blue = 1f;
            if (red > green)
            {
                green = 0f;
            }
            else
            {
                red = 0f;
            }
        }
        Color color = new Color(red, green, blue);
        GetComponent<SpriteRenderer>().color = color;
    }

    IEnumerator Start()
    {
        float delay = UnityEngine.Random.Range(5f, 30f);
        yield return new WaitForSeconds(delay);
        while (!_hasDied)
        {
            GetComponent<AudioSource>().Play();
            delay = UnityEngine.Random.Range(5f, 30f);
            yield return new WaitForSeconds(delay);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;

        if (collision.contacts[0].normal.y < -0.5)
            return true;

        return false;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
