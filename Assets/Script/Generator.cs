 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum; // Menggunakan Enum agar TrashType dikenali

public class Generator : MonoBehaviour
{
    float timer = 1;
    public List<GameObject> trashPrefabs = new List<GameObject>();
    private GameTImer gameTimer;

    void Start()
    {
        gameTimer = FindObjectOfType<GameTImer>();
        timer = GetCurrentSpawnInterval();
    }

    void Update()
    {
        // Stop spawning if game over
        if (ScoreManager.Instance != null && ScoreManager.Instance.isGameOver) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            SpawnTrash();
            timer = GetCurrentSpawnInterval();
        }
    }

    float GetCurrentSpawnInterval()
    {
        if (gameTimer == null) return 2.0f; // Default if timer is not found

        // Calculate elapsed time (assuming a 90 seconds total duration)
        float elapsed = 90f - gameTimer.timeRemaining;

        if (elapsed <= 15f) return 2.0f;
        else if (elapsed <= 30f) return 1.5f;
        else if (elapsed <= 45f) return 1.1f;
        else if (elapsed <= 60f) return 0.8f;
        else if (elapsed <= 75f) return 0.6f;
        else return 0.5f;
    }

    void SpawnTrash()
    {
        if (trashPrefabs.Count == 0) return;

        float pos_x = Random.Range(-4.0f, 4.0f);
        int randomIndex = Random.Range(0, trashPrefabs.Count);
        GameObject trashPrefab = trashPrefabs[randomIndex];

        // Determine the TrashType based on the prefab name
        TrashType trashType = TrashType.Organic; // Default to Organic
        if (trashPrefab.name.ToLower().Contains("anorganic"))
        {
            trashType = TrashType.Anorganic;
        }

        GameObject newTrash = Instantiate(trashPrefab, new Vector3(pos_x, 6.0f, 0.1f), Quaternion.identity);

        ConfigureTrash(newTrash, trashType);
    }

    void ConfigureTrash(GameObject trash, TrashType trashType)
    {
        // Add this debug log to check the assigned TrashType
        Debug.Log($"Configuring trash: {trash.name} with type: {trashType}");

        Rigidbody2D rb = trash.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = trash.AddComponent<Rigidbody2D>();
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0; // Gravitasi dinonaktifkan agar kecepatan jatuh murni dari Trash.cs
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        Collider2D collider = trash.GetComponent<Collider2D>();
        if (collider == null)
        {
            collider = trash.AddComponent<BoxCollider2D>();
        }

        if (!trash.CompareTag("Trash"))
        {
            trash.tag = "Trash";
        }

        Trash trashScript = trash.GetComponent<Trash>();
        if (trashScript == null)
        {
            trashScript = trash.AddComponent<Trash>();
        }
        trashScript.trashType = trashType;
    }
}
