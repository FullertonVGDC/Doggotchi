using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    public SpriteRenderer[] backgrounds;
    public float scrollSpeed;

    void Start()
    {
        backgrounds[0].transform.localPosition = new Vector2(-backgrounds[1].bounds.size.x, backgrounds[1].transform.position.y);
        backgrounds[2].transform.localPosition = new Vector2(backgrounds[1].bounds.size.x, backgrounds[1].transform.position.y);
    }

    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        if (transform.position.x <= -backgrounds[0].bounds.size.x)
        {
            transform.position += Vector3.right * backgrounds[0].bounds.size.x;
        }

        //Fix this later maybe???
        /*for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
            if (backgrounds[i].transform.localPosition.x <= -backgrounds[i].bounds.size.x)
            {
                backgrounds[i].transform.localPosition = new Vector2(backgrounds[i].bounds.size.x, 0);
            }
        }*/
    }
}
