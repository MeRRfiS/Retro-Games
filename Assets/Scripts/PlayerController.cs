using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Rigidbody2D _rigidbody;

    private Vector2 _inputMovement; 
    private Vector3 _direction;

    private static PlayerController instance;

    public static PlayerController GetInstance() => instance;

    #region Update Methods

    private void ApplyMovement()
    {
        Vector2 direction = (transform.up * _inputMovement.y) + (transform.right * _inputMovement.x);
        _direction.x = direction.x;
        _direction.y = direction.y;
        _rigidbody.velocity = direction;
    } 

    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    #region Public Methods

    public void ChangeMovement(Vector2 inputMovement)
    {
        _inputMovement = inputMovement;
    }
    
    #endregion
}
