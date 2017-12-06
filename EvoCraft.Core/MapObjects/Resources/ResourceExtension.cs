using EvoCraft.Common.MapObjects.Resources;

namespace EvoCraft.Core.MapObjects.Resources
{
    public static class ResourceExtension
    {
        public static void Update(this Resource resource)
        {
            if (resource.Capacity <= 0)
            {
                Engine.DestroyMapObject(resource);
            }
        }
    }
}
