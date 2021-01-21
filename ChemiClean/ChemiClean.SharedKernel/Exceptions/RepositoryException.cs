using System;
using System.Runtime.Serialization;

namespace ChemiClean.SharedKernel

{
    /// <summary>
    /// Represents error that occurs during persisting or retrieving information from Repository.
    /// </summary>
    [Serializable]
    public class RepositoryException : Exception
    {
        /// <summary>
        /// Gets generic classification of access repository error.
        /// </summary>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// Gets business entity name that being persisted or retrieved.
        /// </summary>
        public string EntityName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        public RepositoryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class with specified business entity name, and error type.
        /// </summary>
        /// <param name="entityName">business entity name that being persisted or retrieved.</param>
        /// <param name="errorCode">generic classification of errors.</param>
        public RepositoryException(string entityName, int errorCode)
            : base(string.Format("Error [{0}] has been occurred while retrieving or persisting [{1}] entity.", errorCode, entityName))
        {
            this.EntityName = entityName;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class with specified business entity name, error type, error message, and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="entityName">business entity name that being persisted or retrieved.</param>
        /// <param name="errorCode">generic classification of errors.</param>
        /// <param name="message">custom message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public RepositoryException(string entityName, int errorCode, string message, Exception inner)
            : base(message, inner)
        {
            this.EntityName = entityName;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected RepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorCode = (int)info.GetValue(nameof(ErrorCode), typeof(int));
            EntityName = info.GetString(nameof(EntityName));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(ErrorCode), ErrorCode, typeof(ErrorCodes));
            info.AddValue(nameof(EntityName), EntityName);
        }
    }
}