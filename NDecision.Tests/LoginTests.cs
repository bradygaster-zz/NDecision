using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NDecision;
using Should;

namespace NDecision.Tests
{
    [TestFixture]
    public class LoginTests
    {
        [Test]
        public void when_no_password_then_error_is_no_password_error()
        {
            var conversation =
                new LoginConversation
                {
                    Request = new LoginRequest
                    {
                        Password = "pass"
                    }
                };

            new LoginConversationSpec()
                .GetSpec()
                    .Run(conversation);

            conversation.Response.Message.ShouldEqual(Strings.NoUsernameError);
        }

        [Test]
        public void when_no_username_then_error_is_no_username_error()
        {
            var conversation =
                new LoginConversation
                {
                    Request = new LoginRequest
                    {
                        Username = "user"
                    }
                };

            new LoginConversationSpec()
                .GetSpec()
                    .Run(conversation);

            conversation.Response.Message.ShouldEqual(Strings.NoPasswordError);
        }

        [Test]
        public void when_username_and_password_then_message_is_success()
        {
            var conversation =
                new LoginConversation
                    {
                        Request = new LoginRequest
                        {
                            Username = "user",
                            Password = "pass"
                        }
                    };

            new LoginConversationSpec()
                .GetSpec()
                    .Run(conversation);

            conversation.Response.Message.ShouldEqual(Strings.GoodLoginMessage);
        }
    }

    public class LoginConversationSpec : IHasSpec<LoginConversation>
    {
        public Spec<LoginConversation> GetSpec()
        {
            return Spec<LoginConversation>
                .When(x => string.IsNullOrEmpty(x.Request.Username))
                    .Then(x => x.Response.Message = Strings.NoUsernameError)
                .OrWhen(x => string.IsNullOrEmpty(x.Request.Password))
                    .Then(x => x.Response.Message = Strings.NoPasswordError)
                .OrWhen(x => !string.IsNullOrEmpty(x.Request.Username)
                    && !string.IsNullOrEmpty(x.Request.Password))
                .Then(x => x.Response.Message = Strings.GoodLoginMessage);
        }
    }


    public class LoginConversation
    {
        public LoginConversation()
        {
            this.Request = new LoginRequest();
            this.Response = new LoginResponse();
        }

        public LoginRequest Request { get; set; }
        public LoginResponse Response { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Message { get; set; }
    }

    public static class Strings
    {
        public static string NoPasswordError { get { return "please provide a password"; } }
        public static string NoUsernameError { get { return "please provide a username"; } }
        public static string GoodLoginMessage { get { return "login succeeded"; } }
    }
}
