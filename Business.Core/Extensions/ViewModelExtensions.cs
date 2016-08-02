using System;
using Business.ViewModels;
using Intersoft.Crosslight;

namespace Business
{
    /// <summary>
    ///     Class that provides extension methods to ViewModel instances.
    /// </summary>
    public static class ViewModelExtensions
    {
        #region Methods

        /// <summary>
        ///     Gets the exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static string GetExceptionMessage(this Exception exception)
        {
            string exceptionMessage = exception.Message;

            if (exception is AggregateException)
            {
                var aggregrateException = exception as AggregateException;
                if (aggregrateException.InnerExceptions.Count == 1)
                    exceptionMessage = aggregrateException.InnerExceptions[0].Message;
            }

            if (string.IsNullOrEmpty(exceptionMessage) && exception.InnerException != null)
                exceptionMessage = exception.InnerException.Message;

            return exceptionMessage.Replace("\"", "");
        }

        /// <summary>
        ///     Navigates to main view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public static void NavigateToMainViewModel(this INavigable viewModel)
        {
            viewModel.NavigationService.Navigate<DrawerViewModel>();
        }

        #endregion
    }
}