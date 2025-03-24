 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum; // Menggunakan Enum agar TrashType dikenali

public class Generator : MonoBehaviour
{
    float timer = 1;
    public List<GameObject> trashPrefabs = new List<GameObject>();

    void Start()
    {
        timer = 0.7f;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            SpawnTrash();
            timer = 0.7f;
        }
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
        rb.gravityScale = 1;
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
