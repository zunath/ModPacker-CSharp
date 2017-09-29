namespace ModPacker_CSharp.NWObjects
{
    public class NWPoint
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public NWPoint()
        {
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
        }

        public NWPoint(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
