using System;
using System.Reflection;
using Android.App;
using Android.Runtime;
using Intersoft.Crosslight.Android;

namespace Business.Android.Infrastructure
{
    /// <summary>
    ///     Class that manages the entire Android Application lifecycle.
    ///     Useful for intercepting global events.
    /// </summary>
    [Application]
    public class AndroidApp : AndroidApplication
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AndroidApp" /> class.
        /// </summary>
        /// <param name="intPtr">The int PTR.</param>
        /// <param name="jniHandleOwnership">The jni handle ownership.</param>
        public AndroidApp(IntPtr intPtr, JniHandleOwnership jniHandleOwnership)
            : base(intPtr, jniHandleOwnership)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Preserves the assembly to be included in Xamarin's linker.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public static void PreserveAssembly(Assembly assembly)
        {
        }

        #endregion
    }
}