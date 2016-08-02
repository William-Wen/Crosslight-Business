using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Intersoft.Crosslight;

namespace Business.Models
{
    /// <summary>
    ///     A handy model class that conforms to Crosslight lifecycle.
    ///     Most of the time you will only need to subclass this class when introducing a new domain model.
    ///     Includes validation features as well.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.ComponentModel.INotifyDataErrorInfo" />
    /// <seealso cref="Intersoft.Crosslight.IDataValidation" />
    public class ModelBase : INotifyPropertyChanged, INotifyDataErrorInfo, IDataValidation
    {
        #region Fields

        private readonly List<ValidationResult> _validationResultList = new List<ValidationResult>();

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        public virtual bool HasErrors
        {
            get { return _validationResultList.Count(o => !o.IsSuccess) > 0; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Clears all errors.
        /// </summary>
        public void ClearAllErrors()
        {
            List<string> properties = new List<string>();

            foreach (var result in this._validationResultList)
            {
                foreach (var member in result.MemberNames)
                {
                    if (!properties.Contains(member))
                        properties.Add(member);
                }
            }

            _validationResultList.Clear();
            this.OnErrorsChanged("");

            foreach (string property in properties)
            {
                this.OnPropertyChanged(property);
            }
        }

        /// <summary>
        ///     Clears the error.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void ClearError(string propertyName)
        {
            var emptyValidationResult = new List<ValidationResult>();

            foreach (var result in _validationResultList)
            {
                if (result.MemberNames.Contains(propertyName))
                {
                    ((IList)result.MemberNames).Remove(propertyName);

                    if (!result.MemberNames.Any())
                        emptyValidationResult.Add(result);
                }
            }

            foreach (var result in emptyValidationResult)
            {
                _validationResultList.Remove(result);
            }

            this.OnErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Gets all errors.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValidationResult> GetAllErrors()
        {
            return _validationResultList.Where(o => !o.IsSuccess).ToList();
        }

        /// <summary>
        ///     Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property to retrieve validation errors for; or null or
        ///     <see cref="F:System.String.Empty" />, to retrieve entity-level errors.
        /// </param>
        /// <returns>
        ///     The validation errors for the property or entity.
        /// </returns>
        public IEnumerable GetErrors(string propertyName)
        {
            var validationResult = new List<ValidationResult>();

            foreach (var result in _validationResultList)
            {
                if ((string.IsNullOrEmpty(propertyName) || result.MemberNames.Contains(propertyName)) && !result.IsSuccess)
                    validationResult.Add(result);
            }

            return validationResult;
        }

        /// <summary>
        ///     Called when [errors changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnErrorsChanged(string propertyName)
        {
            this.OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Raises the <see cref="E:ErrorsChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DataErrorsChangedEventArgs" /> instance containing the event data.</param>
        private void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            var handler = this.ErrorsChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Raises the <see cref="E:PropertyChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var propertyChanged = this.PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, e);
        }

        /// <summary>
        ///     Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void RaisePropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(propertyName);
        }

        /// <summary>
        ///     Sets the error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="propertyName">Name of the property.</param>
        public void SetError(string errorMessage, string propertyName)
        {
            _validationResultList.Add(new ValidationResult(errorMessage, propertyName));

            this.OnErrorsChanged(propertyName);
            this.OnPropertyChanged(propertyName);
        }

        /// <summary>
        ///     Validate this instance.
        /// </summary>
        public virtual void Validate()
        {
        }

        #endregion

        #region INotifyDataErrorInfo Members

        /// <summary>
        ///     Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}