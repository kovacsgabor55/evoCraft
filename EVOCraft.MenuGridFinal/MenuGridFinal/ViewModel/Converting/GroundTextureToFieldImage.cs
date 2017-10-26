using EvoCraft.Core;

namespace View
{
    public class GroundTextureToFieldImage
    {
        public static FieldImage Convert(GroundTexture ground)
        {
            switch (ground)
            {
                case GroundTexture.Grass1: return FieldImage.Grass1;
                case GroundTexture.Grass2: return FieldImage.Grass2;
                case GroundTexture.Grass3: return FieldImage.Grass3;
                case GroundTexture.Grass4: return FieldImage.Grass4;
                case GroundTexture.Grass5: return FieldImage.Grass5;
                default: return FieldImage.Grass1;
            }
        }
    }
}
