using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public EventHandler<int> OnFruitPickedUp;

    public int scoreWorth = 50;

    [SerializeField]
    private Animator fruitAnimator;
    [SerializeField]
    private CircleCollider2D circleCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnFruitPickedUp?.Invoke(this, scoreWorth);
            fruitAnimator.Play("Collected");
            Destroy(circleCollider);
            Destroy(gameObject, 0.5f);
        }
    }
}
