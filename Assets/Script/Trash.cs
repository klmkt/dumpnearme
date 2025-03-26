using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum; // Menggunakan Enum agar TrashType dikenali

public class Trash : MonoBehaviour
{
    public TrashType trashType; // Gunakan Enum.TrashType dari Enum.cs

    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        tr.position -= new Vector3(0f, 0.12f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ScoreManager.Instance.isGameOver) return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            ScoreManager.Instance.HandleLostTrash();
            Destroy(gameObject);
        }
    }
}
