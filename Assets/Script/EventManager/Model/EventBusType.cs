using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventManager.Model
{
    public enum EventBusType
    {
        PLAY_HURT_ANIMATION,
        PLAY_DEAD_ANIMATION,
        ENABLE_CONTROL_PLAYER,
        DISABLE_CONTROL_PLAYER,
        RESET_POSITION_PLAYER,
        RESET_ANIMATION_PLAYER,
        SHOW_HIT_EFFECT_PLAYER,
        HIDE_HIT_EFFECT_PLAYER,
        HIDE_IGM,
        DISPLAY_IGM,
        START_GAME,
        RESTART_GAME,
        SPAWN_BLOCK,
        RESET_SPAWN_BLOCK,
        STOP_SCROLL,
        START_SCROLL,
        DISABLE_COLLISION,
        ENABLE_COLLISION
    }
}