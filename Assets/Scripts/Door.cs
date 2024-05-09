using System;
using KittyFarm;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private Animator animator;

    private static readonly int AnimatorHash_Open = Animator.StringToHash("Open");
    private static readonly int AnimatorHash_Close = Animator.StringToHash("Close");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetTrigger(AnimatorHash_Open);
        PlayDoorSound();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        animator.SetTrigger(AnimatorHash_Close);
        PlayDoorSound();
    }

    private void PlayDoorSound()
    {
        AudioManager.Instance.PlaySoundEffect(GameSoundEffect.Door);
    }
}
