using Intersoft.Crosslight;
using Intersoft.Crosslight.Input;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for the Login screen.
    /// </summary>
    /// <seealso cref="Business.ViewModels.LoginViewModel" />
    public class HomeViewModel : LoginViewModel
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HomeViewModel" /> class.
        /// </summary>
        public HomeViewModel()
        {
            this.RegisterCommand = new DelegateCommand(ExecuteRegister);
        }

        #endregion

        #region Properties

        public DelegateCommand RegisterCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Executes the register.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private async void ExecuteRegister(object parameter)
        {
            NavigationParameter navigationParameter = new NavigationParameter(NavigationMode.Modal)
            {
                ModalPresentationStyle = ModalPresentationStyle.FormSheet,
                EnsureNavigationContext = true
            };

            NavigationResult result = await this.NavigationService.NavigateAsync<RegisterViewModel>(navigationParameter);
            if (result.Action == NavigationResultAction.Done)
            {
                if (this.AccountService.IsLoggedIn())
                    this.NavigateToMainViewModel();
            }
        }

        #endregion
    }
}