 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum; // Menggunakan Enum agar TrashType dikenali

public class Generator : MonoBehaviour
{
    float timer = 1;
    public List<GameObject> trashPrefabs = new List<GameObject>();
    private GameTImer gameTimer;
    private Coroutine telemetryCoroutine;

    private int spawnCounter = 0;

    void Start()
    {
        gameTimer = FindObjectOfType<GameTImer>();
        timer = GetCurrentSpawnInterval();
        telemetryCoroutine = StartCoroutine(RunTelemetrySimulation());
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

        spawnCounter++;
        GameObject prefabToSpawn = null;

        // Logika Spawn Rate: Hanya muncul tepat setelah 5 objek lain (kelipatan 6)
        if (spawnCounter % 6 == 0)
        {
            // Cari prefab plastic bag dari list
            foreach (GameObject prefab in trashPrefabs)
            {
                if (prefab.name.Contains("plasticbagAnorganic"))
                {
                    prefabToSpawn = prefab;
                    break;
                }
            }
        }

        // Jika bukan waktunya plastic bag (atau tidak ditemukan), spawn sampah normal
        if (prefabToSpawn == null)
        {
            int attempts = 0;
            do
            {
                int randomIndex = Random.Range(0, trashPrefabs.Count);
                prefabToSpawn = trashPrefabs[randomIndex];
                attempts++;
            }
            // Pastikan kita tidak me-spawn plasticbag secara acak di luar gilirannya!
            // (attempts < 10 ada untuk mencegah game freeze jika isi list ternyata plasticbag semua)
            while (prefabToSpawn.name.Contains("plasticbagAnorganic") && attempts < 10);
        }

        float pos_x = Random.Range(-4.0f, 4.0f);

        TrashType trashType = TrashType.Organic;
        if (prefabToSpawn.name.ToLower().Contains("anorganic"))
        {
            trashType = TrashType.Anorganic;
        }

        GameObject newTrash = Instantiate(prefabToSpawn, new Vector3(pos_x, 6.0f, 0.1f), Quaternion.identity);
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

    IEnumerator RunTelemetrySimulation()
    {
        Debug.Log("<color=cyan>=== TELEMETRY: Simulasi Real-Time Dimulai ===</color>");

        float timeElapsed = 0f;
        int totalSpawns = 0;
        int normalCollected = 0;
        int specialCollected = 0;

        // Loop per detik selama sesi berlangsung
        while (timeElapsed < 90f)
        {
            yield return new WaitForSeconds(1f); // Menunggu 1 detik real-time
            timeElapsed += 1f;

            // Simulasi deteksi spawn berdasarkan interval saat ini
            // (Logika spawn mengikuti generator utama)
            float currentInterval = GetCurrentSpawnInterval();

            // Log setiap detiknya
            Debug.Log($"[Detik {timeElapsed}] Interval Spawn: {currentInterval:F1}s");

            // Kalkulasi statistik teoritis berdasarkan pergerakan waktu
            if (totalSpawns % 6 == 0 && totalSpawns > 0)
            {
                specialCollected++;
            }
            else
            {
                normalCollected++;
            }
            totalSpawns++;
        }

        // Ringkasan akhir setelah 90 detik
        int finalScore = (normalCollected * 1) + (specialCollected * 10);

        Debug.Log("<color=cyan>=== TELEMETRY: Sesi Berakhir ===</color>");
        Debug.Log($"Total Objek di-Spawn: {totalSpawns}");
        Debug.Log($"Total Skor Teoritis: {finalScore} (Normal: {normalCollected}, Spesial: {specialCollected})");
        Debug.Log("<color=cyan>================================</color>");
    }
}
