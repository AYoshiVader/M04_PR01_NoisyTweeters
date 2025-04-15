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
        int full = Random.Range(0, 3);
        int part = Random.Range(0, 3);
        while (part == full)
        {
            part = Random.Range(0, 3);
        }
        Color color = new Color(
            1f,
            1f,
            1f,
            1);
        GetComponent<SpriteRenderer>().color = Color.
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
