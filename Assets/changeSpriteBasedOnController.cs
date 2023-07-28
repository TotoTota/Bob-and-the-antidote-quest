using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class changeSpriteBasedOnController : MonoBehaviour
{
    public string currentControlScheme { get; }
    public SpriteRenderer sr;
    public PlayerInput playerInput;

    public Sprite XboxSprite;
    public Sprite KeyboardSprite;

    void Update()
    {
        if(playerInput.currentControlScheme.Equals("Keyboard"))
        {
            sr.sprite = KeyboardSprite;
        }

        if (playerInput.currentControlScheme.Equals("Xbox"))
        {
            sr.sprite = XboxSprite;
        }
    }
}
