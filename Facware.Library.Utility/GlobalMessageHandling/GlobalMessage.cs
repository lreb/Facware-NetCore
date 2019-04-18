using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Facware.Library.Utility.GlobalMessageHandling
{
    public class GlobalMessage
    {
        public int StatusCode;
        public Result ResultCode;
        public string EventReference;
        public bool Success;
        public bool Warning;
        public bool Fail;
        public object Data;
        public string Message;
        public ICollection<MessageDetail> MessageDetail;

        public GlobalMessage()
        {

        }

        private GlobalMessage(Result resultCode, string message, int statusCode = 200)
        {
            ResultCode = resultCode;
            Message = message;
            SetResultStatus(statusCode);
        }

        private GlobalMessage(Result resultCode, string message, object data, int statusCode = 200)
        {
            ResultCode = resultCode;
            Message = message;
            Data = data;
            SetResultStatus(statusCode);
        }

        private void SetResultStatus(int statusCode) 
        {
            switch (ResultCode)
            {
                case Result.ERROR:
                    Fail = true;
                    Success = false;
                    Warning = false;
                    StatusCode = statusCode;
                    break;
                case Result.SUCCESS:
                    Fail = false;
                    Success = true;
                    Warning = false;
                    StatusCode = statusCode;
                    break;
                case Result.WARNING:
                    Fail = false;
                    Success = false;
                    Warning = true;
                    StatusCode = statusCode;
                    break;
            }
        }

        public static GlobalMessage SuccessResult(string message)
        {
            return new GlobalMessage(Result.SUCCESS, message);
        }

        public static GlobalMessage SuccessResult(string message, object data)
        {
            return new GlobalMessage(Result.SUCCESS, message, data);
        }

        public static GlobalMessage FailResult(string message)
        {
            return new GlobalMessage(Result.ERROR, message);
        }

        public static GlobalMessage FailResult(string message, object data)
        {
            return new GlobalMessage(Result.ERROR, message, data);
        }

        public static GlobalMessage WarningResult(string message)
        {
            return new GlobalMessage(Result.WARNING, message);
        }

        public static GlobalMessage WarningResult(string message, object data)
        {
            return new GlobalMessage(Result.WARNING, message, data);
        }

        public void AddMessageDetail(MessageDetail messageDetail)
        {
            if (MessageDetail == null)
            {
                MessageDetail = new List<MessageDetail>();

            }
            MessageDetail.Add(messageDetail);
        }

        /*public GlobalMessage ReturnSuccess(object data, string message) 
        {
            StatusCode = 200;
            Success = true;
            Warning = false;
            Fail = false;
            Data = data;
            Message = message;
            return this;
        }

        public GlobalMessage ReturnWarning(object data, string message)
        {
            StatusCode = 200;
            Success = false;
            Warning = true;
            Fail = false;
            Data = data;
            Message = message;
            return this;
        }

        public GlobalMessage ReturnFail(object data, string message)
        {
            StatusCode = 200;
            Success = false;
            Warning = false;
            Fail = true;
            Data = data;
            Message = message;
            return this;
        }*/

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public enum GlobalMessageType
    {
        Success,
        Warning,
        Fail
    }

    public enum Result { ERROR, SUCCESS, WARNING }

    public class MessageDetail
    {
        public Result Type;
        public string Key;
        public string Message;
        public string Detail;
        public MessageDetail()
        {

        }

        public MessageDetail(string key, string message, string messageDetail)
        {
            Key = key;
            Message = message;
            Detail = messageDetail;
        }

        public static MessageDetail CreateSuccessMessage(string key, string message, string messageDetail)
        {
            MessageDetail m = new MessageDetail(key, message, messageDetail);
            m.SetType(Result.SUCCESS);
            return m;
        }

        public static MessageDetail CreateErrorMessage(string key, string message, string messageDetail)
        {
            MessageDetail m = new MessageDetail(key, message, messageDetail);
            m.SetType(Result.ERROR);
            return m;
        }

        public static MessageDetail CreateWarningMessage(string key, string message, string messageDetail)
        {
            MessageDetail m = new MessageDetail(key, message, messageDetail);
            m.SetType(Result.WARNING);
            return m;
        }

        public string GetKey()
        {
            return Key;
        }

        public string GetMessage()
        {
            return Message;
        }

        public string GetMessageDetail()
        {
            return Detail;
        }

        public Result GetResultType()
        {
            return Type;
        }

        public void SetKey(string key)
        {
            Key = key;
        }

        public void SetMessage(string message)
        {
            Message = message;
        }

        public void SetMessageDetail(string messageDetail)
        {
            Detail = messageDetail;
        }

        public void SetType(Result type)
        {
            Type = type;
        }
        /**
         * Evaluates if message is SUCCESS
         *
         * @return True for successful messages. False otherwise
         */
        public bool IsSuccess()
        {
            return Type == Result.SUCCESS;
        }
        /**
         * Evaluates if message is ERROR
         *
         * @return True for error messages. False otherwise
         */
        public bool IsError()
        {
            return Type == Result.ERROR;
        }
        /**
         * Evaluates if message is WARNING
         *
         * @return True for warning messages. False otherwise
         */
        public bool IsWarning()
        {
            return Type == Result.WARNING;
        }
        /**
         * Creates serializable representation of the message
         *
         * @return Message data as a String
         */
        public String ToHtml()
        {
            String typeStr = "";
            switch (Type)
            {
                case Result.SUCCESS:
                    typeStr = "SUCCESS";
                    break;
                case Result.ERROR:
                    typeStr = "ERROR";
                    break;
                case Result.WARNING:
                    typeStr = "WARNING";
                    break;
                default:
                    typeStr = "";
                    break;
            }
            return $"<tr><td>{typeStr}</td><td>{Key}</td><td>{Message}</td><td>{Detail}</td>";
        }
    }
}
