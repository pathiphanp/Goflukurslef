using UnityEngine;
using UnityEngine.EventSystems; // สำหรับเช็คว่าไม่ได้คลิกโดน UI

public class MaskPainter2D : MonoBehaviour
{
    public Color paintColor = Color.red; // สีที่จะใช้ระบาย
    public int brushSize = 5; // ขนาดแปรง (รัศมีเป็นพิกเซล)

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Texture2D texture;
    private RectTransform rectTransform; // สำหรับการแปลงตำแหน่งเมาส์บน 2D UI

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // ตรวจสอบว่ามี SpriteRenderer และ Sprite พร้อมใช้งาน
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            // ตรวจสอบ Read/Write Enabled (ควรตั้งค่าใน Inspector)
            texture = spriteRenderer.sprite.texture;
            if (!texture.isReadable)
            {
                Debug.LogError("Texture for sprite is not readable. Please enable 'Read/Write Enabled' in the Import Settings.");
                return;
            }
        }
        else
        {
            Debug.LogError("SpriteRenderer or Sprite not found on this GameObject.");
            return;
        }

        // ถ้าใช้ Canvas UI สำหรับ Sprite ที่จะระบาย
        rectTransform = GetComponent<RectTransform>();
        ClearToWhite();
    }



    void Update()
    {
        PintColor();
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log(paintColor + " : " + GetColorPercentage(paintColor));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearToWhite();
        }
    }

    void PintColor()
    {
        if (Input.GetMouseButton(0) && texture != null) // ตรวจจับการคลิกซ้ายค้าง
        {
            // เช็คว่าเมาส์ไม่ได้อยู่บน UI (ถ้ามี UI ในฉาก)
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Vector2 mousePos = Input.mousePosition;
            Vector2 localPoint;

            // ตรวจสอบว่าเมาส์อยู่บน Sprite (UI Canvas หรือ World Space)
            if (rectTransform != null) // ถ้าเป็น UI Image
            {
                if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePos, Camera.main, out localPoint))
                {
                    return; // เมาส์ไม่ได้อยู่บน UI Image
                }
            }
            else // ถ้าเป็น SpriteRenderer ใน World Space
            {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
                // แปลงจาก world space เป็น local space ของ sprite
                localPoint = transform.InverseTransformPoint(worldPoint);
                float spriteWidth = spriteRenderer.sprite.bounds.size.x;
                float spriteHeight = spriteRenderer.sprite.bounds.size.y;
                // ต้องปรับ scale ให้เข้ากับพิกเซลของ texture
                localPoint.x *= (texture.width / spriteWidth);
                localPoint.y *= (texture.height / spriteHeight);
            }

            // แปลง Local Point เป็น Pixel Coordinate (0 ถึง width/height)
            int pixelX = Mathf.RoundToInt(localPoint.x + texture.width / 2);
            int pixelY = Mathf.RoundToInt(localPoint.y + texture.height / 2);

            // ระบายสีรอบๆ ตำแหน่งพิกเซล
            for (int x = -brushSize; x <= brushSize; x++)
            {
                for (int y = -brushSize; y <= brushSize; y++)
                {
                    int currentX = pixelX + x;
                    int currentY = pixelY + y;

                    // ตรวจสอบขอบเขตของ Texture
                    if (currentX >= 0 && currentX < texture.width &&
                        currentY >= 0 && currentY < texture.height)
                    {
                        texture.SetPixel(currentX, currentY, paintColor);
                    }
                }
            }
            texture.Apply(); // อัปเดต Texture
        }
    }

    // สำหรับการคำนวณเปอร์เซ็นต์
    public float GetColorPercentage(Color targetColor)
    {
        if (texture == null) return 0f;
        // 1. ดึงพิกเซลทั้งหมดออกมา (ระวัง: ถ้า Texture ใหญ่มาก การเรียก GetPixels บ่อยๆ อาจทำให้กระตุกได้)
        Color[] pixels = texture.GetPixels();
        int totalPixels = pixels.Length;
        int matchCount = 0;

        // 2. วนลูปเช็คทีละพิกเซล
        for (int i = 0; i < pixels.Length; i++)
        {
            // ใช้การเช็คระยะห่างของสี (Threshold) เพื่อความแม่นยำ
            // 0.1f คือค่าความเพี้ยนที่ยอมรับได้ (ปรับได้ตามความเหมาะสม)
            if (Vector4.Distance(pixels[i], targetColor) < 0.1f)
            {
                matchCount++;
            }
        }

        // 3. คำนวณเป็นเปอร์เซ็นต์
        float percentage = (float)matchCount / totalPixels * 100f;
        return percentage;
    }

    public void ClearToWhite()
    {
        if (texture == null) return;

        // 1. สร้าง Array ของสีขาวที่มีขนาดเท่ากับจำนวนพิกเซลทั้งหมด
        Color[] whitePixels = new Color[texture.width * texture.height];
        for (int i = 0; i < whitePixels.Length; i++)
        {
            whitePixels[i] = Color.white; // หรือ Color.clear ถ้าอยากให้โปร่งแสง
        }

        // 2. ส่งค่าสีขาวทั้งหมดกลับไปที่ Texture ทีเดียว (เร็วกว่า SetPixel ทีละจุด)
        texture.SetPixels(whitePixels);
        texture.Apply();
        Debug.Log("เคลียร์หน้ากากเป็นสีขาวเรียบร้อย!");
    }
}