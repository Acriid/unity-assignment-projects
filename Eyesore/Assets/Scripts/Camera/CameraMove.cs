using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("Camera Movement Settings")]
    [SerializeField] private int _horizontalMoveAmount = 23;
    [SerializeField] private int _verticalMoveAmount = 15;
    
    [Header("Player Reference")]
    [SerializeField] private GameObject _player;
    
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
        
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    
    private void LateUpdate()
    {
        if (_player == null || mainCamera == null)
            return;
        
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(_player.transform.position);
        
        float distanceToLeft = viewportPosition.x;
        float distanceToRight = 1f - viewportPosition.x;
        float distanceToBottom = viewportPosition.y;
        float distanceToTop = 1f - viewportPosition.y;
        
        float minHorizontalDistance = Mathf.Min(distanceToLeft, distanceToRight);
        float minVerticalDistance = Mathf.Min(distanceToBottom, distanceToTop);
        
        bool playerOutsideView = distanceToLeft < 0f || distanceToLeft > 1f || 
            distanceToBottom < 0f || distanceToBottom > 1f;

        if (playerOutsideView)
        {
            if (minHorizontalDistance < minVerticalDistance)
            {
                if (distanceToLeft < 0f)
                {
                    transform.position += new Vector3(-_horizontalMoveAmount, 0, 0);
                }
                else if (distanceToLeft > 1f)
                {
                    transform.position += new Vector3(_horizontalMoveAmount, 0, 0);
                }
            }
            else
            {
                if (distanceToBottom < 0f)
                {
                    transform.position += new Vector3(0, -_verticalMoveAmount, 0);
                }
                else if (distanceToBottom > 1f)
                {
                    transform.position += new Vector3(0, _verticalMoveAmount, 0);
                }
            }
        }
    }
}