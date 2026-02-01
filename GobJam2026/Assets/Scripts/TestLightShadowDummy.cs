using UnityEngine;

public class TestLightShadowDummy : MonoBehaviour
{
    public Transform lightSource; // ลากตัวแปรแสง (เช่น ตะเกียงของน้อง) มาใส่ในนี้
    public float shadowLength = 1.2f; // ความยาวของเงา

    void Update()
    {
        if (lightSource == null) return;

        // 1. หาเวกเตอร์ทิศทางจากแสงมายังตัวละคร
        Vector3 dirFromLight = transform.position - lightSource.position;
        dirFromLight.z = 0; // ล็อคแกน Z ไว้สำหรับเกม 2D

        // 2. คำนวณองศาที่จะให้เงาหันไป (ใช้ Atan2 หาค่ามุม)
        float angle = Mathf.Atan2(dirFromLight.y, dirFromLight.x) * Mathf.Rad2Deg;

        // 3. ปรับหมุนแกน Z ของเงาให้ชี้ไปทางเดียวกับทิศทางแสงที่ส่องมา
        // (ปกติเงาจะตั้งฉากกับแสง เราจึงต้อง -90 หรือปรับตามตำแหน่ง Sprite ของคุณ)
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        // 4. (Optional) ปรับ Scale แกน Y ให้ยาวขึ้นตามระยะห่างจากแสงเพื่อให้ดูสมจริง
        float dist = dirFromLight.magnitude;
        transform.localScale = new Vector3(1, shadowLength + (dist * 0.05f), 1);
    }
}
