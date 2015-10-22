using System;
using System.Diagnostics.Contracts;

namespace GuiSystem.Toolbox
{
    public class Operation
    {
        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }

        public bool Failure
        {
            get { return !Success; }
        }

        protected Operation(bool success, string error)
        {
            Contract.Requires(success || !string.IsNullOrEmpty(error));
            Contract.Requires(!success || string.IsNullOrEmpty(error));

            Success = success;
            ErrorMessage = error;
        }

        public static Operation Failed(string message = null)
        {
            return new Operation(false, message);
        }

        public static Operation<T> Failed<T>(T defaultValue,string message = null)
        {
            return new Operation<T>(defaultValue, false, message);
        }

        public static Operation<T> Failed<T>(string message = null)
        {
            return new Operation<T>(default(T), false, message);
        }
        
        public static Operation Succeeded()
        {
            return new Operation(true, String.Empty);
        }

        public static Operation<T> Succeeded<T>(T value)
        {
            return new Operation<T>(value, true, String.Empty);
        }

        public static Operation Combine(params Operation[] results)
        {
            foreach (Operation result in results)
            {
                if (result.Failure)
                    return result;
            }
            return Succeeded();
        }
    }


    public class Operation<T> : Operation
    {
        private T _value;

        public T Result
        {
            get
            {
                Contract.Requires(Success);

                return _value;
            }
            private set { _value = value; }
        }

        protected internal Operation(T value, bool success, string error)
            : base(success, error)
        {
            Contract.Requires(value != null || !success);

            Result = value;
        }
    }
}
