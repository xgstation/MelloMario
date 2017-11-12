namespace MelloMario.Theming
{
    static class GameConst
    {
        // general

        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 600;

        // boundary and collision

        public const int GRID = 32;
        public const int SCANRANGE = 24;

        // physics

        public const float VELOCITY_MAX_LR = 10f;
        public const float VELOCITY_MAX_U = 15f;
        public const float VELOCITY_MAX_D = 20f;

        public const float FORCE_G = 40f;
        public const float FORCE_INPUT_X = 120f;
        public const float FORCE_INPUT_Y = 150f;
        public const float FORCE_F_GROUND = 100f;
        public const float FORCE_F_AIR = 20f;

        // gameplay

        public const int LEVEL_TIME = 400;
        public const int SCORE_COIN = 200;

        // visual
        public const int TEXTURE_GRID = 16;
        public const int ANIMATION_INTERVAL = 100;
    }
}
