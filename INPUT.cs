using UnityEngine;

public static class INPUT
{
    // Fields
    static bool validInput_ = true;
    private static float timerInputPressJump_ = 0.0f;
    private static float timerInputPressLeft_ = -1f;
    private static float timerInputPressRight_ = -1f;
    private static float lastDashTime_ = -1f;
    private static float doubleTapThreshold_ = 0.5f; // Seconds within which a double tap is registered
    private static float dashCooldown_ = 1.0f; // Seconds to wait before allowing another dash

    // Properties for encapsulating timer fields with getters and setters
    public static float TimerInputPressJump
    {
        get => timerInputPressJump_;
        set => timerInputPressJump_ = value;
    }

    public static float TimerInputPressLeft
    {
        get => timerInputPressLeft_;
        set => timerInputPressLeft_ = value;
    }

    public static float TimerInputPressRight
    {
        get => timerInputPressRight_;
        set => timerInputPressRight_ = value;
    }

    public static float LastDashTime
    {
        get => lastDashTime_;
        set => lastDashTime_ = value;
    }

    // Detect initial press
    public static bool Input_Tap_Left() => validInput_ && Input.GetKeyDown(KeyCode.LeftArrow);
    public static bool Input_Tap_Right() => validInput_ && Input.GetKeyDown(KeyCode.RightArrow);
    public static bool Input_Tap_Jump() => validInput_ && Input.GetKeyDown(KeyCode.Space);

    // Detect continuous press
    public static bool Input_Move_Left() => validInput_ && Input.GetKey(KeyCode.LeftArrow);
    public static bool Input_Move_Right() => validInput_ && Input.GetKey(KeyCode.RightArrow);
    public static bool Input_Move_Jump() => validInput_ && Input.GetKey(KeyCode.Space);

    // Detect release
    public static bool Input_Release_Left() => validInput_ && Input.GetKeyUp(KeyCode.LeftArrow);
    public static bool Input_Release_Right() => validInput_ && Input.GetKeyUp(KeyCode.RightArrow);
    public static bool Input_Release_Jump() => validInput_ && Input.GetKeyUp(KeyCode.Space);

    // Dash input detection for left and right using GENERIC method
    public static bool Input_Dash_Left()
    {
        return GENERIC.ValidateDoubleTap(Input_Tap_Left, ref timerInputPressLeft_, ref lastDashTime_, dashCooldown_, doubleTapThreshold_);
    }

    public static bool Input_Dash_Right()
    {
        return GENERIC.ValidateDoubleTap(Input_Tap_Right, ref timerInputPressRight_, ref lastDashTime_, dashCooldown_, doubleTapThreshold_);
    }
}
