using Intersoft.Crosslight.Input;
using Intersoft.Crosslight.ViewModels;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for About view (page 2).
    /// </summary>
    /// <seealso cref="Business.ViewModels.AboutViewModelBase" />
    public class AboutNavigationViewModel : ViewModelBase
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AboutNavigationViewModel" /> class.
        /// </summary>
        public AboutNavigationViewModel()
        {
            this.Title = "About";
            this.AboutText = "Crosslight includes over more than 12,000 APIs and built-in services to make enterprise apps development easier than ever. This template represents a typical business app which leverages many of the services available in Crosslight such as local storage service, user management services, data access services, single sign-on with social services, push notification services, and much more.";
            this.FooterText = "Powered by Crosslight®";
            this.IntroductionText = "Intersoft Crosslight makes native enterprise cross-platform mobile development truly a breeze, thanks to the rock-solid frameworks and comprehensive data components.";
            this.LearnMoreCommand = new DelegateCommand(ExecuteLearnMore);
        }

        #endregion

        #region Fields

        private string _aboutText;
        private string _footerText;
        private string _introductionText;

        #endregion

        #region Properties

        public string AboutText
        {
            get { return _aboutText; }
            set
            {
                if (_aboutText != value)
                {
                    _aboutText = value;
                    this.OnPropertyChanged("AboutText");
                }
            }
        }

        public string FooterText
        {
            get { return _footerText; }
            set
            {
                if (_footerText != value)
                {
                    _footerText = value;
                    this.OnPropertyChanged("FooterText");
                }
            }
        }

        public string IntroductionText
        {
            get { return _introductionText; }
            set
            {
                if (_introductionText != value)
                {
                    _introductionText = value;
                    this.OnPropertyChanged("IntroductionText");
                }
            }
        }

        public DelegateCommand LearnMoreCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Executes the LearnMore action.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void ExecuteLearnMore(object parameter)
        {
            this.MobileService.Browser.Navigate("http://www.intersoftsolutions.com/crosslight");
        }

        #endregion
    }
}