namespace Floppy.Utilities {
    public class FpsCounter {
        private float _timeSum = 0f;
        private int _frameCounter = 0;

        private float _samplingTimer = 0f;

        public float SamplingPeriod { get; set; }

        public float LatestFps { get; private set; }

        public void Update(float deltaTime) {
            _timeSum += deltaTime;
            _frameCounter++;

            _samplingTimer += deltaTime;

            if (_samplingTimer >= SamplingPeriod) {
                LatestFps = _frameCounter / _timeSum;

                _timeSum = 0f;
                _frameCounter = 0;

                _samplingTimer = 0f;
            }
        }
    }
}
