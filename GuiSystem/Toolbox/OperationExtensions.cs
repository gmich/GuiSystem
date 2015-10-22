using System;

namespace GuiSystem.Toolbox
{
    public static class OperationExtensions
    {
        public static Operation OnSuccess(this Operation result, Func<Operation> func)
        {
            if (result.Failure)
                return result;

            return func();
        }

        public static Operation OnSuccess(this Operation result, Action action)
        {
            if (result.Failure)
                return result;

            action();

            return Operation.Succeeded();
        }

        public static Operation OnSuccess<T>(this Operation<T> result, Action<T> action)
        {
            if (result.Failure)
                return result;

            action(result.Result);

            return Operation.Succeeded();
        }

        public static Operation<T> OnSuccess<T>(this Operation result, Func<T> func)
        {
            if (result.Failure)
                return Operation.Failed<T>(result.ErrorMessage);

            return Operation.Succeeded(func());
        }

        public static Operation<T> OnSuccess<T>(this Operation result, Func<Operation<T>> func)
        {
            if (result.Failure)
                return Operation.Failed<T>(result.ErrorMessage);

            return func();
        }

        public static Operation OnSuccess<T>(this Operation<T> result, Func<T, Operation> func)
        {
            if (result.Failure)
                return result;

            return func(result.Result);
        }

        public static Operation OnFailure(this Operation result, Action action)
        {
            if (result.Failure)
            {
                action();
            }

            return result;
        }

        public static Operation OnBoth(this Operation result, Action<Operation> action)
        {
            action(result);

            return result;
        }

        public static T OnBoth<T>(this Operation result, Func<Operation, T> func)
        {
            return func(result);
        }
    }
}
