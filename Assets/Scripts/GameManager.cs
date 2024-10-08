using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager InputManager { get; private set; }

    // Start acontece no primeiro frame
    // Awake acontece antes do primeiro frame
    private void Awake() 
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        InputManager = new InputManager();
    }
}
