using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum; // Menggunakan Enum agar TrashType dikenali

public class MovementTong : MonoBehaviour
{
    public TrashType binType; // Gunakan TrashType dari Enum

    public GameObject tongOrganik; // Referensi ke tempat sampah organik
    public GameObject tongAnorganik; // Referensi ke tempat sampah anorganik

    public GameObject Tong1; // Referensi ke Tong1
    public GameObject Tong2; // Referensi ke Tong2

    private Transform trOrganik;
    private Transform trAnorganik;
    private static TrashType currentBinType = TrashType.Organic;

    private Color activeColor = Color.white;
    private Color inactiveColor = Color.black;

    void Start()
    {
        trOrganik = tongOrganik.GetComponent<Transform>();
        trAnorganik = tongAnorganik.GetComponent<Transform>();

        // Inisialisasi tong sampah berdasarkan tipe yang aktif
        SetActiveBin(currentBinType);
        UpdateTongColors(currentBinType);
    }

    void Update()
    {
        // Ganti antara tong sampah dengan tombol 1 dan 2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchBin(TrashType.Organic);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchBin(TrashType.Anorganic);
        }
    }

    void FixedUpdate()
    {
        // Always handle movement for both bins
        HandleMovement();
    }

    private void HandleMovement()
    {
        float movement = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (trOrganik.position.x < 8f)
                movement = 0.2f;

            // Flip to face right
            trOrganik.localScale = new Vector3(-0.1314079f, 0.09286255f, 0.2f);
            trAnorganik.localScale = new Vector3(-0.1314079f, 0.09286255f, 0.2f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (trOrganik.position.x > -8f)
                movement = -0.2f;

            // Flip to face left
            trOrganik.localScale = new Vector3(0.1314079f, 0.09286255f, 0.2f);
            trAnorganik.localScale = new Vector3(0.1314079f, 0.09286255f, 0.2f);
        }

        // Move both bins simultaneously
        trOrganik.position += Vector3.right * movement;
        trAnorganik.position += Vector3.right * movement;
    }

    private void SwitchBin(TrashType newBinType)
    {
        currentBinType = newBinType;
        SetActiveBin(newBinType);
        UpdateTongColors(newBinType);
        Debug.Log($"Switched to {newBinType} Bin");
    }

    private void SetActiveBin(TrashType binType)
    {
        tongOrganik.SetActive(binType == TrashType.Organic);
        tongAnorganik.SetActive(binType == TrashType.Anorganic);
    }

    private void UpdateTongColors(TrashType binType)
    {
        if (Tong1 != null)
        {
            Tong1.GetComponent<SpriteRenderer>().color = binType == TrashType.Organic ? activeColor : inactiveColor;
        }
        if (Tong2 != null)
        {
            Tong2.GetComponent<SpriteRenderer>().color = binType == TrashType.Anorganic ? activeColor : inactiveColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ScoreManager.Instance.isGameOver) return;

        if (collision.CompareTag("Trash"))
        {
            Trash trash = collision.GetComponent<Trash>();
            if (trash != null)
            {
                if (trash.trashType == binType) // Correct bin
                {
                    ScoreManager.Instance.AddScore(1);
                    Destroy(collision.gameObject);
                }
                else // Wrong bin
                {
                    ScoreManager.Instance.HandleWrongHit();
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
