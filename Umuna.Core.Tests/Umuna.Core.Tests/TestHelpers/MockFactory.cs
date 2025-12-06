using System.Globalization;
using Umuna.Core.Tests.Mocking;

namespace Umuna.Core.Tests.TestHelpers
{
    /// <summary>
    /// Provides factory methods for creating test data objects
    /// </summary>
    public static class MockFactory
    {
        // Constants for sample values used by GetMockData and GetSampleValues
        private const int SAMPLE_USER_ID = 1;
        private const string SAMPLE_USER_NAME = "Test";
        private const string SAMPLE_USER_EMAIL = "a@b.com";
        private const int SAMPLE_USER_AGE = 30;
        private const bool SAMPLE_USER_ISACTIVE = true;

        private const int SAMPLE_PRODUCT_ID = 10;
        private const string SAMPLE_PRODUCT_NAME = "Product1";
        private const float SAMPLE_PRODUCT_PRICE = 9.99f;
        private const string SAMPLE_PRODUCT_CATEGORY = "Misc";
        private const bool SAMPLE_PRODUCT_INSTOCK = true;

        private const double VERSION = 1.0;
        private static readonly string LAST_UPDATED = DateOnly.MinValue.ToString();

        /// <summary>
        /// Creates a standard UmunaData object with predefined test values
        /// </summary>
        /// <returns>A populated UmunaData instance for testing</returns>
        public static MockData GetMockData()
        {
            return new MockData
            {
                users = [new User { id = SAMPLE_USER_ID, name = SAMPLE_USER_NAME, email = SAMPLE_USER_EMAIL, age = SAMPLE_USER_AGE, isActive = SAMPLE_USER_ISACTIVE }],
                products = [new Product { id = SAMPLE_PRODUCT_ID, name = SAMPLE_PRODUCT_NAME, price = SAMPLE_PRODUCT_PRICE, category = SAMPLE_PRODUCT_CATEGORY, inStock = SAMPLE_PRODUCT_INSTOCK }],
                metadata = new Metadata { version = VERSION, lastUpdated = LAST_UPDATED, totalRecords = 2 }
            };
        }

        public static string[] GetSampleValues()
        {
            return
            [
                SAMPLE_USER_ID.ToString(CultureInfo.InvariantCulture),
                SAMPLE_USER_NAME,
                SAMPLE_USER_EMAIL,
                SAMPLE_USER_AGE.ToString(CultureInfo.InvariantCulture),
                SAMPLE_USER_ISACTIVE.ToString().ToLowerInvariant(),
                SAMPLE_PRODUCT_ID.ToString(CultureInfo.InvariantCulture),
                SAMPLE_PRODUCT_NAME,
                SAMPLE_PRODUCT_PRICE.ToString(CultureInfo.InvariantCulture),
                SAMPLE_PRODUCT_CATEGORY,
                SAMPLE_PRODUCT_INSTOCK.ToString().ToLowerInvariant(),
                VERSION.ToString(CultureInfo.InvariantCulture),
                LAST_UPDATED
            ];
        }
    }
}
