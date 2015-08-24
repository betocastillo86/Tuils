using System.Collections.Generic;

namespace Nop.Core.Domain.Messages
{
    public class EmailSubscribedEvent
    {
        private readonly string _email;
        private readonly NewsLetterSuscriptionType _suscriptionType;
        private readonly string _additionalInfo;

        public EmailSubscribedEvent(string email, NewsLetterSuscriptionType suscriptionType, string additionalInfo)
        {
            _email = email;
            _suscriptionType = suscriptionType;
            _additionalInfo = additionalInfo;
        }

        public string Email
        {
            get { return _email; }
        }

        public NewsLetterSuscriptionType SuscriptionType
        {
            get { return _suscriptionType; }
        }

        public string AdditionalInfo
        {
            get { return _additionalInfo; }
        }

        public bool Equals(EmailSubscribedEvent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._email, _email) && Equals(other._suscriptionType, _suscriptionType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(EmailSubscribedEvent)) return false;
            return Equals((EmailSubscribedEvent)obj);
        }

        public override int GetHashCode()
        {
            return (_email != null ? string.Concat(_email, _suscriptionType).GetHashCode() : 0);
        }
    }

    public class EmailUnsubscribedEvent
    {
        private readonly string _email;
        private readonly NewsLetterSuscriptionType _suscriptionType;
        

        public EmailUnsubscribedEvent(string email, NewsLetterSuscriptionType suscriptionType)
        {
            _email = email;
            _suscriptionType = suscriptionType;
        }

        public string Email
        {
            get { return _email; }
        }

        public NewsLetterSuscriptionType SuscriptionType
        {
            get { return _suscriptionType; }
        }

        public bool Equals(EmailUnsubscribedEvent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._email, _email) && Equals(other._suscriptionType, _suscriptionType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(EmailUnsubscribedEvent)) return false;
            return Equals((EmailUnsubscribedEvent)obj);
        }

        public override int GetHashCode()
        {
            return (_email != null ? string.Concat(_email, _suscriptionType).GetHashCode() : 0);
        }
    }

    /// <summary>
    /// A container for tokens that are added.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="U"></typeparam>
    public class EntityTokensAddedEvent<T, U> where T : BaseEntity
    {
        private readonly T _entity;
        private readonly IList<U> _tokens;

        public EntityTokensAddedEvent(T entity, IList<U> tokens)
        {
            _entity = entity;
            _tokens = tokens;
        }

        public T Entity { get { return _entity; } }
        public IList<U> Tokens { get { return _tokens; } }
    }

    /// <summary>
    /// A container for tokens that are added.
    /// </summary>
    /// <typeparam name="U"></typeparam>
    public class MessageTokensAddedEvent<U>
    {
        private readonly MessageTemplate _message;
        private readonly IList<U> _tokens;

        public MessageTokensAddedEvent(MessageTemplate message, IList<U> tokens)
        {
            _message = message;
            _tokens = tokens;
        }

        public MessageTemplate Message { get { return _message; } }
        public IList<U> Tokens { get { return _tokens; } }
    }
}