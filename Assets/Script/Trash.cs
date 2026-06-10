using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum; // Menggunakan Enum agar TrashType dikenali

public class Trash : MonoBehaviour
{
    public TrashType trashType; // Gunakan Enum.TrashType dari Enum.cs

    private Transform tr;
    private float fallSpeed = 0.12f;

    void Start()
    {
        tr = GetComponent<Transform>();
        if (gameObject.name.Contains("plasticbagAnorganic"))
        {
            fallSpeed = 0.28f; // Jatuh lebih cepat secara signifikan (lebih dari 2x lipat)
        }
    }

    void FixedUpdate()
    {
        tr.position -= new Vector3(0f, fallSpeed, 0f);
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
