using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum; // Menggunakan Enum agar TrashType dikenali

public class MovementTong : MonoBehaviour
{
    public TrashType binType; // Gunakan TrashType dari Enum

    public GameObject tongOrganik; // Referensi ke tempat sampah organik
    public GameObject tongAnorganik; // Referensi ke tempat sampah anorganik

    private Transform trOrganik;
    private Transform trAnorganik;
    private static TrashType currentBinType = TrashType.Organic;

    void Start()
    {
        trOrganik = tongOrganik.GetComponent<Transform>();
        trAnorganik = tongAnorganik.GetComponent<Transform>();

        // Inisialisasi tong sampah berdasarkan tipe yang aktif
        SetActiveBin(currentBinType);
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
        Debug.Log($"Switched to {newBinType} Bin");
    }

    private void SetActiveBin(TrashType binType)
    {
        tongOrganik.SetActive(binType == TrashType.Organic);
        tongAnorganik.SetActive(binType == TrashType.Anorganic);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trash"))
        {
            Trash trash = collision.GetComponent<Trash>();
            if (trash != null && trash.trashType == binType) // Bandingkan tipe yang sama
            {
                Debug.Log("Correct trash! Accepted.");
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("Wrong bin! Rejected.");
            }
        }
    }
}