using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 7.5f;

    private Vector2 _worldPosition;

    private bool _isWalking;
    
    private void Update()
    {
        if (!_isWalking)
        {
            Debug.Log("Stop");
            return;
        }
        Debug.Log("Walking");

        // Calculate the direction to the target position
        Vector3 targetPosition3D = new Vector3(_worldPosition.x, transform.position.y, _worldPosition.y);
        Vector3 direction = (targetPosition3D - transform.position).normalized;
        
        // Move the player towards the target position
        transform.position += direction * (moveSpeed * Time.deltaTime);

        // Check if we're close enough to the target position
        if (Vector2.Distance(transform.position, _worldPosition) >= 0.1f)
        {
            return;
        }
        
        // Stop moving once we're close enough
        _worldPosition = Vector2.zero;
        _isWalking = false;
    }

    public void OnFire()
    {
        Debug.Log("FIRE");
        // Cast a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }
        
        // Set the target position to the point where the ray hits the ground
        _worldPosition = new Vector2(hit.point.x, hit.point.z);
        _isWalking = true;
    }
}