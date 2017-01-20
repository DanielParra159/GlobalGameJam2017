using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LayerDefinitions {

    public readonly static int ENEMY_LAYER = 8;
    public readonly static int PLAYER_LAYER = 9;
    public readonly static int PLAYER_TRIGGER_LAYER = 10;
    public readonly static int INVISIBLE_OBSTACLE_LAYER = 11;
    public readonly static int GROUND_LAYER = 12;
    public readonly static int DECOR_LAYER = 13;
    public readonly static int WALL_LAYER = 14;
    public readonly static int PLAYER_PROJECTILE_LAYER = 15;
    public readonly static int ENEMY_PROJECTILE_LAYER = 16;

    public static int ENEMY_MASK = (1 << ENEMY_LAYER);
    public static int PLAYER_MASK = (1 << PLAYER_LAYER);
    public static int PLAYER_AND_ENEMIES_MASK = (1 << PLAYER_LAYER) | (1 << PLAYER_LAYER);

}
