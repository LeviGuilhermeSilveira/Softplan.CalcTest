using System;

namespace Softplan.CalcTest.Shared
{
    public class Validate
    {
        public UriValidation UriValidation { get { return new UriValidation(); } }
        
        public NumberValidation NumberValidation { get { return new NumberValidation(); } }
    }    

    public class UriValidation
    {
        public UriValidation ForlNotNullUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
            }
            catch (UriFormatException ex)
            {
                throw ex;
            }

            return this;
        }

        public UriValidation ForNotEmptyUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
            }
            catch (UriFormatException ex)
            {
                throw ex;
            }

            return this;
        }

        public UriValidation ForValidUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                var testValue = uri.AbsoluteUri;
            }
            catch (UriFormatException ex)
            {
                throw ex;
            }

            return this;
        }
    }
    
    public class NumberValidation
    {
        public NumberValidation IsGreaterThanZero(decimal value)
        {
            if (value <= 0)
                throw new InvalidOperationException("O valor é menor ou igual a zero.");

            return this;
        }
    }
}
