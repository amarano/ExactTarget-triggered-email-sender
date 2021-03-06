﻿using System;
using ExactTarget.TriggeredEmail.Creation;
using ExactTarget.TriggeredEmail.Trigger;
using NUnit.Framework;

namespace ExactTarget.TriggeredEmail.Test.Integration
{
    public class PasteHtmlTriggeredEmailNoLayoutTest : TestBase
    {
        [Test]
        public void Create_And_Send_A_PasteHtml_Triggered_Email_With_No_Layout()
        {
            var externalKey = Guid.NewGuid().ToString();
            Create(externalKey);
            Send(externalKey);
        }

        private void Create(string externalKey)
        {
            var triggeredEmailCreator = new TriggeredEmailCreator(Config);

            Assert.DoesNotThrow(() => triggeredEmailCreator.CreateTriggeredSendDefinitionWithPasteHtml(
                                        externalKey,
                                        Priority.High),
                                    "Failed to create Triggered Email");

            Assert.DoesNotThrow(() => triggeredEmailCreator.StartTriggeredSend(externalKey), "Failed to start Triggered Send");
        }

        private void Send(string externalKey)
        {
            var triggeredEmail = new ExactTargetTriggeredEmail(externalKey, TestRecipientEmail);
            triggeredEmail.AddReplacementValue("Subject", "Test email");
            triggeredEmail.AddReplacementValue("Head", "<style>.green{color:green}</style>");
            triggeredEmail.AddReplacementValue("Body", "<p class='green'>Some test copy here...in green</p>" +
                                                       "<p> This is a Paste HTML email without layout");

            var emailTrigger = new EmailTrigger(Config);
            
            Assert.DoesNotThrow( () =>  emailTrigger.Trigger(triggeredEmail), "Failed to send email");
        }

    }
}
