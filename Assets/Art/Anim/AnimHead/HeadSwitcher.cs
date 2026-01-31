using UnityEngine;

public class HeadSwitcher : MonoBehaviour
{
    public Animator headAnim; // Masukkan Animator Kepala di sini

    void Update()
    {
        // Contoh: Tekan tombol 1, 2, 3 di keyboard untuk tes
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchFace(0); // Ke Wajah 1
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchFace(1); // Ke Wajah 2
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchFace(2); // Ke Wajah 3
        }
    }

    public void SwitchFace(int index)
    {
        // Kirim perintah ke Animator
        headAnim.SetInteger("FaceID", index);
    }
}