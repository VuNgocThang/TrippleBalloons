using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Scale
{
    Full,
    Horizontal,
    Vertical
}

public class AutoScale : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Scale scaleMode;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        transform.localScale = new Vector3(
            0.52f,
            worldScreenHeight / sr.sprite.bounds.size.y, 1);
        // switch (scaleMode.ToString())
        // {
        //     case "Full":
        //         transform.localScale = new Vector3(
        //                         worldScreenWidth / sr.sprite.bounds.size.x,
        //                         worldScreenHeight / sr.sprite.bounds.size.y, 1);
        //         break;
        //     case "Horizontal":
        //         transform.localScale = new Vector3(
        //             worldScreenWidth / sr.sprite.bounds.size.x,
        //             0.5f, 1);
        //         break;
        //     case "Vertical":
        //         transform.localScale = new Vector3(
        //                         0.52f,
        //                         worldScreenHeight / sr.sprite.bounds.size.y, 1);
        //         break;
        // }
    }
}