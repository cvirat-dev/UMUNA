using Umuna.Core.SharedData;
using Umuna.Core.SharedData.Common;

namespace Umuna.Core.Tests.TestHelpers
{
    /// <summary>
    /// Provides factory methods for creating test data objects
    /// </summary>
    public static class TestDataFactory
    {
        /// <summary>
        /// Creates a standard UmunaData object with predefined test values
        /// </summary>
        /// <returns>A populated UmunaData instance for testing</returns>
        public static UmunaData CreateTestData()
        {
            UmunaData data = new();
            data.UserData.PlayerName = "DemoPlayer";
            data.UserData.PlayerId = "123456789";
            data.GameName = "DemoGame";

            SpatialOrientationData sod1 = new("Position1", new Vector3Data(1, 2, 3), new Vector3Data(0, 90, 0));
            SpatialOrientationData sod2 = new("Position2", new Vector3Data(4, 5, 6), new Vector3Data(0, 180, 0));
            data.CameraData.AddPosition(sod1);
            data.CameraData.AddPosition(sod2);

            return data;
        }
    }
}
