using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Rigidbody2D _rigidbody;

    private bool _isFreeMoving = true;
    private int _playerHearts;
    private Vector2 _inputMovement; 
    private Vector3 _direction;

    private static PlayerController instance;

    public static PlayerController GetInstance() => instance;

    public bool IsFreeMoving
    {
        set => _isFreeMoving = value;
    }

    public int PlayerHearts
    {
        get => _playerHearts;
    }

    #region Update Methods

    private void ApplyMovement()
    {
        if (!_isFreeMoving)
        {
            Vector2 direction = (transform.up * _inputMovement.y) + (transform.right * _inputMovement.x);
            _direction.x = direction.x;
            _direction.y = direction.y;
            _rigidbody.velocity = _direction * PlayerConstants.MOVING_SPEED;
        }
        else
        {
            Vector2 direction = (transform.up * _inputMovement.y) + (transform.right * -1);
            _direction.x = direction.x;
            _direction.y = direction.y;
            _rigidbody.velocity = _direction * CarGameController.GetInstance().GameSpeed;
        }

        float positionX = Mathf.Clamp(transform.localPosition.x,
                                          -PlayerConstants.MAX_POSITION_X,
                                          PlayerConstants.MAX_POSITION_X);
        float positionY = Mathf.Clamp(transform.localPosition.y,
                                      -PlayerConstants.MAX_POSITION_Y,
                                      PlayerConstants.MAX_POSITION_Y);
        transform.localPosition = new Vector2(positionX, positionY);
    } 

    #endregion

    private void Awake()
    {
        instance = this;
        _playerHearts = PlayerConstants.MAX_PLAYER_HEARTS;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    #region Public Methods

    public void ChangeMovement(Vector2 inputMovement)
    {
        _isFreeMoving = false;
        _inputMovement = inputMovement;
    }

    public void GetDamage()
    {
        if (_playerHearts <= 0) return;

        _playerHearts--;
        UIController.GetInstance().RemoveHeart();
    }
    
    #endregion
}
