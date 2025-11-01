using UnityEngine;

public enum PowerUpType
{
    Magnet,
    SpeedBoost,
    Shield,
    DoubleCoins
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType;
    public float duration = 5f;
    public float rotationSpeed = 100f;
    
    void Update()
    {
        // Rotate
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    
    public void Activate(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        
        switch (powerUpType)
        {
            case PowerUpType.Magnet:
                // Attract nearby coins
                StartCoroutine(MagnetEffect(player));
                break;
                
            case PowerUpType.SpeedBoost:
                if (playerController != null)
                {
                    playerController.IncreaseSpeed(5f);
                }
                break;
                
            case PowerUpType.Shield:
                // Make player invincible temporarily
                StartCoroutine(ShieldEffect(player));
                break;
                
            case PowerUpType.DoubleCoins:
                // Double coin value temporarily
                StartCoroutine(DoubleCoinsEffect());
                break;
        }
    }
    
    System.Collections.IEnumerator MagnetEffect(GameObject player)
    {
        float timer = 0f;
        
        while (timer < duration)
        {
            // Find all coins in range
            Collider[] coins = Physics.OverlapSphere(player.transform.position, 10f);
            
            foreach (Collider coin in coins)
            {
                if (coin.CompareTag("Coin"))
                {
                    // Move coin towards player
                    coin.transform.position = Vector3.MoveTowards(
                        coin.transform.position,
                        player.transform.position,
                        20f * Time.deltaTime
                    );
                }
            }
            
            timer += Time.deltaTime;
            yield return null;
        }
    }
    
    System.Collections.IEnumerator ShieldEffect(GameObject player)
    {
        // Disable obstacle collision temporarily
        PlayerController playerController = player.GetComponent<PlayerController>();
        
        // Visual effect: Add glowing shield
        GameObject shield = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        shield.transform.SetParent(player.transform);
        shield.transform.localPosition = Vector3.zero;
        shield.transform.localScale = Vector3.one * 2.5f;
        
        Renderer renderer = shield.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material shieldMat = new Material(Shader.Find("Standard"));
            shieldMat.color = new Color(0f, 1f, 1f, 0.3f);
            shieldMat.SetFloat("_Mode", 3); // Transparent mode
            shieldMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            shieldMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            shieldMat.SetInt("_ZWrite", 0);
            shieldMat.DisableKeyword("_ALPHATEST_ON");
            shieldMat.EnableKeyword("_ALPHABLEND_ON");
            shieldMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            shieldMat.renderQueue = 3000;
            renderer.material = shieldMat;
        }
        
        Destroy(shield.GetComponent<Collider>());
        
        yield return new WaitForSeconds(duration);
        
        Destroy(shield);
    }
    
    System.Collections.IEnumerator DoubleCoinsEffect()
    {
        // This would need to be implemented in GameManager
        // For now, just wait
        yield return new WaitForSeconds(duration);
    }
}
