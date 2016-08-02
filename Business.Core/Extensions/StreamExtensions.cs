using System.IO;

namespace Business
{
    /// <summary>
    ///     Class that provides extension methods for Stream instances.
    /// </summary>
    public static class StreamExtensions
    {
        #region Methods

        /// <summary>
        ///     Converts Stream to byte array.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static byte[] ToByte(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        #endregion
    }
}