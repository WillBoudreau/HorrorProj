
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveWithCursor : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private PlayerInput playerInput;


    void Start()
    {
        mainCamera = Camera.main;
        playerInput = GetComponent<PlayerInput>();
    }
    void Update()
    {
        MoveTowardsCursor();
    }

    void MoveTowardsCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
       
        if(playerInput.actions["Fire"].IsPressed())
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Debug.Log("Hit Point: " + hitInfo.point);
                Debug.Log("Target Position Set To: " + hitInfo.point);
                Debug.DrawLine(transform.position, hitInfo.point, Color.green);
                targetPosition = hitInfo.point;
                Vector3 direction = (targetPosition - transform.position).normalized;
            }
        }
    }
}
