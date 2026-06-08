# 🗑️ Dump Near Me - Technical Design Document (TDD)

## 📖 Deskripsi Proyek
**Dump Near Me** adalah game kasual 2D berbasis Unity yang dirancang untuk mengedukasi pemain (khususnya anak-anak dan remaja) mengenai pentingnya memilah sampah. Pemain harus menggerakkan tempat sampah (tong) untuk menangkap sampah yang berjatuhan dan memasukkannya ke dalam kategori yang tepat: **Organik** atau **Anorganik**.

---

## 🏗️ Arsitektur Sistem & Alur Game (Game Flow)
Game ini memiliki siklus sederhana:
1. **Menu Utama (`MainMenu.unity`):** Pemain dapat memulai game, melihat kredit, atau keluar.
2. **Gameplay (`Gameplay.unity`):** - Script `Generator.cs` akan memunculkan (spawn) sampah organik dan anorganik secara acak dari atas layar.
   - Pemain mengendalikan tong menggunakan `MovementTong.cs` ke kiri dan kanan.
   - Jika sampah masuk ke tong yang benar (berdasarkan `Enum.cs`), `ScoreManager.cs` akan menambah poin.
   - Jika salah atau sampah jatuh, nyawa/kondisi akan berkurang hingga memicu `GameOverUI.cs`.
3. **Kredit (`Credit.unity`):** Menampilkan informasi pengembang game.

---

## 📂 Dokumentasi Script Utama (Scripts Documentation)
Semua logika kode berada di dalam folder `Assets/Script/`. Berikut adalah penjelasan fungsi masing-masing script agar AI atau developer lain dapat memahami cara kerja game tanpa membaca kode sumber:

### 1. Sistem Inti Gameplay
* **`Enum.cs`**: Berisi definisi tipe data Enumeration untuk membedakan jenis sampah dan jenis tong (contoh: `Organic` dan `Anorganic`). Ini adalah fondasi dari sistem validasi skor.
* **`Trash.cs`**: Script kelas dasar (Base Class) yang dipasang pada prefab sampah. Mengatur logika gravitasi/jatuh (kemungkinan menggunakan `Rigidbody2D` dan `Collider2D`) serta mendeteksi interaksi/tabrakan dengan objek di bawahnya.
* **`KulitPisang.cs`**: Script turunan atau spesifik untuk sampah kulit pisang. Mengandung properti khusus jika diperlukan (misalnya kecepatan jatuh yang berbeda).
* **`MovementTong.cs`**: Script pengontrol pemain. Membaca input (seperti tombol panah/A & D, atau sentuhan layar) untuk memanipulasi komponen `Transform` pada karakter tong sampah agar bergerak secara horizontal. Terdapat batasan batas layar (Screen bounds) agar tong tidak keluar arena.
* **`Generator.cs`**: Script Spawner. Menggunakan sistem *Timer* atau *Coroutine* untuk melakukan `Instantiate` prefab sampah secara acak pada koordinat sumbu X tertentu di bagian atas layar.

### 2. Sistem Skor & Audio
* **`ScoreManager.cs`**: Script *Singleton* atau manajer statis yang menyimpan nilai `int score`. Script ini memiliki fungsi `AddScore()` yang dipanggil oleh `Trash.cs` atau keranjang ketika sampah organik masuk ke tong organik, dsb.
* **`ScoreDisplay.cs`**: Berfungsi menjembatani `ScoreManager` dengan UI. Script ini mengambil data dari `ScoreManager` dan memperbarui teks pada layar menggunakan komponen TextMeshPro (`TMPro`).
* **`AudioManager.cs`**: Mengatur pemutaran efek suara (SFX) seperti `Button.mp3` saat UI ditekan, dan `background.mp3` untuk Background Music (BGM).

### 3. Sistem UI & Scene Management
* **`MainMenu.cs`**: Mengatur fungsionalitas tombol di menu utama (Play, Quit, Credits) menggunakan `SceneManager.LoadScene()`.
* **`PauseMenu.cs`**: Mengatur jeda waktu game (menyetel `Time.timeScale = 0` saat pause dan `1` saat resume), serta menampilkan panel pause.
* **`GameOverUI.cs`**: Menampilkan panel Game Over ketika kondisi kalah terpenuhi, memberikan opsi untuk "Restart" (memuat ulang scene `Gameplay`) atau "Main Menu".
* **`HomeCredit.cs`**: Script sederhana untuk mengatur tombol kembali (Back) dari scene Credit ke Main Menu.

---

## 📦 Daftar Prefabs (Assets Mapping)
Prefabs disimpan di folder `Assets/prefabs/`. Daftar ini digunakan untuk referensi objek apa saja yang di-*spawn* oleh `Generator.cs`:

**1. Sampah Organik (Organic Trash):**
- `fishboneOrganic.prefab` (Tulang Ikan)
- `kertasOrganic.prefab` (Kertas / Kertas bungkus)
- `kulitpisangOrganic.prefab` (Kulit Pisang)

**2. Sampah Anorganik (Anorganic Trash):**
- `kalengAnorganic.prefab` (Kaleng bekas)
- `plasticbagAnorganic.prefab` (Kantong Plastik)

**3. Pemain / Keranjang (Bins):**
- `tongOrganik.prefab` / `Tong1.prefab`
- `tongAnorganik.prefab` / `Tong2.prefab`

---

## 🛠️ Tech Stack & Pengaturan
* **Engine:** Unity Engine (menggunakan arsitektur 2D).
* **UI System:** Unity Canvas + TextMeshPro (TMP).
* **Version Control:** Git (Terdapat sistem `.gitignore` standar Unity untuk mengabaikan file build dan temporary).