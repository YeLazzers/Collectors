using YeLazzers.Buildings;

namespace YeLazzers.Game
{
    public readonly struct BuildingModeContext
    {
        public readonly BuildingConfig Config;
        public readonly Building Source;

        public BuildingModeContext(BuildingConfig config, Building source)
        {
            Config = config;
            Source = source;
        }
    }
}
