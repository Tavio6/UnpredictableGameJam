using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject enemyGroup;
    private void Update()
    {
        if (enemyGroup.transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
