using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Gravity")]
    [Tooltip("Gaya gravitasi yang dibutuhkan pada saat puncak")]
    [HideInInspector] public float gravityStrength;
    [Tooltip("Besar Gaya gravitasi")]
    [HideInInspector] public float gravityScale;
    [Space(3)]
    [Tooltip("Tekanan Gaya gravitasi pada saat player terjun")]
    public float fallGravityMult;
    [Tooltip("Kecepatan pada saat player terjun")]
    public float maxFallSpeed;
    [Space(3)]
    [Tooltip("Kecepatan*X pada saat player terjun")]
    public float fastFallGravityMult;
    [Tooltip("Kecepatan maximum pada saat player terjun")]
    public float maxFastFallSpeed;

    [Space(10)]
    [Header("Run")]
    public float runMaxSpeed; 
    public float runAcceleration; 
    [HideInInspector] public float runAccelAmount; 
    public float runDecceleration; 
    [HideInInspector] public float runDeccelAmount; 
    [Space(5)]
    [Range(0f, 1)] public float accelInAir; 
    [Range(0f, 1)] public float deccelInAir;
    [Space(5)]
    public bool doConserveMomentum = true;

    [Space(20)]

    [Header("Jump")]
    public float jumpHeight; 
    public float jumpTimeToApex; 
    [HideInInspector] public float jumpForce; 

    [Header("Both Jumps")]
    public float jumpCutGravityMult;
    [Range(0f, 1)] public float jumpHangGravityMult;
    public float jumpHangTimeThreshold; 
    [Space(0.5f)]
    public float jumpHangAccelerationMult;
    public float jumpHangMaxSpeedMult;

    [Header("Wall Jump")]
    public Vector2 wallJumpForce; 
    [Space(5)]
    [Range(0f, 1f)] public float wallJumpRunLerp; 
    [Range(0f, 1.5f)] public float wallJumpTime; 
    public bool doTurnOnWallJump; 

    [Space(20)]

    [Header("Slide")]
    public float slideSpeed;
    public float slideAccel;

    [Header("Assists")]
    [Range(0.01f, 0.5f)] public float coyoteTime; 
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime; 

    [Space(20)]

    [Header("Dash")]
    public int dashAmount;
    public float dashSpeed;
    public float dashSleepTime; 
    [Space(5)]
    public float dashAttackTime;
    [Space(5)]
    public float dashEndTime; 
    public Vector2 dashEndSpeed;
    [Range(0f, 1f)] public float dashEndRunLerp;
    [Space(5)]
    public float dashRefillTime;
    [Space(5)]
    [Range(0.01f, 0.5f)] public float dashInputBufferTime;


    private void OnValidate()
    {
        //Calculate gravity strength using the formula (gravity = 2 * jumpHeight / timeToJumpApex^2) 
        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

        //Calculate the rigidbody's gravity scale (ie: gravity strength relative to unity's gravity value, see project settings/Physics2D)
        gravityScale = gravityStrength / Physics2D.gravity.y;

        //Calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        //Calculate jumpForce using the formula (initialJumpVelocity = gravity * timeToJumpApex)
        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        #region Variable Ranges
        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
        #endregion
    }
}
