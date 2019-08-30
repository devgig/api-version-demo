using System;

namespace Demo.Ui
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
