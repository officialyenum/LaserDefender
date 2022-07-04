using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Camera mainCamera;
    Shooter shooter;
    Health health;
    Rigidbody2D playerRigidbody2D;

    void Awake()
    {
        shooter = FindObjectOfType<Shooter>();
        health = player.GetComponent<Health>();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.GetHealth() > 0)
        {
            if (Touch.activeTouches.Count == 0)
            {
                shooter.isFiring = false;
                return;
            }
            shooter.isFiring = true;
            Vector2 touchPosition = new Vector2();

            foreach (Touch touch in Touch.activeTouches)
            {
                touchPosition += touch.screenPosition;
            }
            touchPosition /= Touch.activeTouches.Count;
            Vector2 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            worldPosition.y += 2f;
            playerRigidbody2D.position = worldPosition;
        }
    }
}
