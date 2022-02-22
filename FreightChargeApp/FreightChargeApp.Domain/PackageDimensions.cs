namespace FreightChargeApp.Domain
{
    public record PackageDimensions
    {
        public PackageDimensions(float length, float width, float height)
            => Volume = CalculateVolume(length, width, height);

        public PackageDimensions(float volume)
            => Volume = volume;

        public float Volume { get; }

        private static float CalculateVolume(float length, float width, float height)
            => length * width * height;
    }
}
