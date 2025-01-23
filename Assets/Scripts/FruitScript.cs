using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public EventHandler<FruitPickedUpEventArgs> OnFruitPickedUp;

    public class FruitPickedUpEventArgs : EventArgs
    {
        public int Score { get; }
        public FruitType Type { get; }

        public FruitPickedUpEventArgs(int score, FruitType type)
        {
            Score = score;
            Type = type;
        }
    }

    public enum FruitType
    {
        Apple,
        Banana,
        Cherry
    }

    public FruitType type;

    public int score = 50;

    [SerializeField]
    private Animator fruitAnimator;
    [SerializeField]
    private CircleCollider2D circleCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var eventArgs = new FruitPickedUpEventArgs(score, type);
            OnFruitPickedUp?.Invoke(this, eventArgs);
            fruitAnimator.Play("Collected");
            Destroy(circleCollider);
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RemoveSpawnedObject(gameObject);
        }
    }
}
