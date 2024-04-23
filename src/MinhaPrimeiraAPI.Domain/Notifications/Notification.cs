using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MinhaPrimeiraAPI.Domain.Notifications
{
    public class Notification
    {
        #region Public Properties

        public List<string> Errors { get; private set; } = new List<string>();
        public string Message { get; private set; }
        public int Status { get; private set; }
        public bool Success => !Errors.Any();

        #endregion Public Properties

        #region Public Methods

        public Notification AddNotifications(string error, int status = (int)HttpStatusCode.BadRequest)
        {
            if (string.IsNullOrWhiteSpace(error))
            {
                new ArgumentException("A mensagem é necessária!");
            }

            Add(error);
            Status = status;

            return this;
        }

        public Notification AddNotifications(ValidationResult validation)
        {
            var listErrors = validation.Errors.Where(x => x.ErrorMessage != "No default error message has been specified").Select(x => new string($"{x.PropertyName}: {x.ErrorMessage}")).ToList();
            listErrors.ForEach(e =>
            {
                Add(e);
            });

            if (listErrors.Count > 0)
                Status = (int)HttpStatusCode.BadRequest;

            return this;
        }

        public Notification SetCreated(string message = "")
        {
            Message = message;
            Status = (int)HttpStatusCode.Created;

            return this;
        }

        public Notification SetOk(string message = "")
        {
            Message = message;
            Status = (int)HttpStatusCode.OK;

            return this;
        }

        #endregion Public Methods

        #region Private Methods

        private void Add(string error)
        {
            if (!Errors.Contains(error))
                Errors.Add(error);
        }

        #endregion Private Methods
    }
}