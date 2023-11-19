using System;

namespace Sources.Data
{
    [Serializable]
    public class UpgradeForAnchor
    {
        public float RadiusForAnchor;

        public UpgradeForAnchor(float radiusForAnchor)
        {
            RadiusForAnchor = radiusForAnchor;
        }
    }
}