using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum; // Menggunakan Enum agar TrashType dikenali

public class MovementTong : MonoBehaviour
{
    public TrashType binType; // Gunakan TrashType dari Enum

    public GameObject tongOrganik; // Referensi ke tempat sampah organik
    public GameObject tongAnorganik; // Referensi ke tempat sampah anorganik

    private Transform tr;
    private static TrashType currentBinType = TrashType.Organic;

    void Start()
    {
        tr = GetComponent<Transform>();

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
        // Hanya gerakkan tong yang aktif
        if (binType == currentBinType)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (tr.position.x < 8f)
                tr.position += Vector3.right * 0.2f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (tr.position.x > -8f)
                tr.position += Vector3.left * 0.2f;
        }
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
