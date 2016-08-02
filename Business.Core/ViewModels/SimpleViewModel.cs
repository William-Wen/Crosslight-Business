using Intersoft.Crosslight;
using Intersoft.Crosslight.Input;
using Intersoft.Crosslight.ViewModels;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for the Hello World screen (page 1).
    /// </summary>
    /// <seealso cref="Business.ViewModels.SampleViewModelBase" />
    public class SimpleViewModel : ViewModelBase
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimpleViewModel" /> class.
        /// </summary>
        public SimpleViewModel()
        {
            IApplicationContext context = this.GetService<IApplicationService>().GetContext();
            if (context.Platform.OperatingSystem == OSKind.Android)
                this.GreetingText = "Hello Android from Crosslight!";
            else if (context.Platform.OperatingSystem == OSKind.WinPhone)
                this.GreetingText = "Hello WinPhone from Crosslight!";
            else if (context.Platform.OperatingSystem == OSKind.WinRT)
                this.GreetingText = "Hello WinRT from Crosslight!";
            else if (context.Platform.OperatingSystem == OSKind.iOS)
                this.GreetingText = "Hello iOS from Crosslight!";

            this.Title = "Home";
            this.FooterText = "Powered by Crosslight®";
            this.ShowToastCommand = new DelegateCommand(ShowToast);
        }

        #endregion

        #region Fields

        private string _footerText;
        private string _greetingText;
        private string _newText;

        #endregion

        #region Properties

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

        public string GreetingText
        {
            get { return _greetingText; }
            set
            {
                if (_greetingText != value)
                {
                    _greetingText = value;
                    this.OnPropertyChanged("GreetingText");
                }
            }
        }

        public string NewText
        {
            get { return _newText; }
            set
            {
                if (_newText != value)
                {
                    _newText = value;
                    this.OnPropertyChanged("NewText");
                }
            }
        }

        public DelegateCommand ShowToastCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Shows the toast.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void ShowToast(object parameter)
        {
            this.ToastPresenter.Show("You entered: " + this.NewText);
            this.GreetingText = this.NewText;
        }

        #endregion
    }
}