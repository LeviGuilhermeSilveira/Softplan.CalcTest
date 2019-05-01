using System;

namespace Softplan.CalcTest.Shared
{
    public class Validator
    {
        private bool _isValid = true;
        private string _errorMessage;

        public bool IsValid { get { return _isValid; } }
        public string ErrorMessage { get { return _errorMessage; } }       

        public void Validate(Action<Validate> action)
        {
            var validate = new Validate();
            try
            {
                action.Invoke(validate);
            }
            catch (Exception ex)
            {
                _isValid = false;
                _errorMessage = ex.Message;
            }
        }
    }
}
