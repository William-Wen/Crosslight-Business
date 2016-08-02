using System;
using System.Collections.Generic;
using System.Linq;
using Intersoft.AppFramework.Identity;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Input;
using Intersoft.Crosslight.ViewModels;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for the navigation menu in the navigation drawer.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.ViewModels.ListViewModelBase{Intersoft.Crosslight.NavigationItem}" />
    public class NavigationViewModel : ListViewModelBase<NavigationItem>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigationViewModel" /> class.
        /// </summary>
        public NavigationViewModel()
        {
            List<NavigationItem> items = new List<NavigationItem>();

            NavigationParameter modalParameter = new NavigationParameter(NavigationMode.Modal)
            {
                ModalPresentationStyle = ModalPresentationStyle.FormSheet,
                EnsureNavigationContext = true
            };

            this.LogoutCommand = new DelegateCommand(this.ExecuteLogout);

            items.Add(new NavigationItem("Page 1", "Menu", typeof(SimpleViewModel)));
            items.Add(new NavigationItem("Page 2", "Menu", typeof(AboutNavigationViewModel)));
            items.Add(new NavigationItem("Page 3", "Menu", typeof(ItemListViewModel)));

            items.Add(new NavigationItem("Profile", "Settings", new NavigationTarget(typeof(UserProfileViewModel), modalParameter)));
            items.Add(new NavigationItem("Settings", "Settings", new NavigationTarget(typeof(UserSettingsViewModel), modalParameter)));
            items.Add(new NavigationItem("Logout", "Settings", this.LogoutCommand));

            items.Add(new NavigationItem("About This App", "About", typeof(AboutNavigationViewModel)));

            this.SourceItems = items;
            this.RefreshGroupItems();
        }

        #endregion

        #region Fields

        private User _user;

        #endregion

        #region Properties

        public string EmailDisplay
        {
            get
            {
                if (_user != null)
                    return _user.Email;
                else
                    return string.Empty;
            }
        }

        public string LoginDisplay
        {
            get
            {
                if (this.User != null)
                    return this.User.UserDetail.FirstName + " " + this.User.UserDetail.LastName;
                return string.Empty;
            }
        }

        public DelegateCommand LogoutCommand { get; set; }

        public virtual string ProfilePhotoUrl
        {
            get
            {
                if (this.User != null)
                    return this.User.UserDetail.ResolvedImageUrl;

                return "square_placeholder.png";
            }
        }

        public User User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged("User");
                    OnPropertyChanged("ProfilePhotoUrl");
                    OnPropertyChanged("LoginDisplay");
                    OnPropertyChanged("EmailDisplay");

                    this.GetService<IUserService>().SetCurrentUser(_user);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Executes the logout.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private async void ExecuteLogout(object parameter)
        {
            int selectedButtonIndex = await this.MessagePresenter.ShowAsync("Are you sure you want to log out?", "Logout", new[] { "Yes", "No" });

            if (selectedButtonIndex == 0)
            {
                try
                {
                    await this.GetService<IAccountService>().SignOutAsync();
                    this.NavigationService.Navigate<HomeViewModel>();
                }
                catch (Exception ex)
                {
                    this.MessagePresenter.Show(ex.Message, "Unable to logout due to an error");
                }
            }
        }

        /// <summary>
        ///     Initializes the user.
        /// </summary>
        internal void InitializeUser()
        {
            User user = this.GetService<IUserService>().GetCurrentUser();
            if (user != null)
                this.User = user;
        }

        /// <summary>
        ///     Called when this instance is navigated.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public override void Navigated(NavigatedParameter parameter)
        {
            base.Navigated(parameter);

            this.InitializeUser();
        }

        /// <summary>
        ///     Refreshs the group items.
        /// </summary>
        public override void RefreshGroupItems()
        {
            if (this.Items != null)
                this.GroupItems = this.Items.GroupBy(o => o.Group).Select(o => new GroupItem<NavigationItem>(o)).ToList();
        }

        #endregion
    }
}