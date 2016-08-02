using System;
using System.Linq;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Android.v7;
using Intersoft.Crosslight.Android.v7.FormBuilders;
using Intersoft.Crosslight.Forms;

namespace Business.Android.Fragments
{
    /// <summary>
    ///     Crosslight Form Builder Activity for user registration at the login page.
    /// </summary>
    public class RegisterFragment : FormFragment<RegisterViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisterFragment" /> class.
        /// </summary>
        public RegisterFragment()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisterFragment" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public RegisterFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            this.AddBarItem(new BarItem("SaveButton", CommandItemType.Done));

            this.IconId = Resource.Drawable.ic_toolbar;
        }

        /// <summary>
        ///     Called when the view is created.
        /// </summary>
        protected override void OnViewCreated()
        {
            base.OnViewCreated();

            PropertyDefinition facebookLoginProperty = this.Form.GetProperties().Where(o => o.PropertyName == "FacebookLoginButton").FirstOrDefault();
            if (facebookLoginProperty != null)
            {
                ButtonWidget buttonWidget = facebookLoginProperty.View as ButtonWidget;
                if (buttonWidget != null)
                    buttonWidget.View.SetBackgroundResource(Resource.Drawable.facebook_button_style);
            }
        }

        #endregion
    }
}