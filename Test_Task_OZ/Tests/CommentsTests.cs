using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test_Task_OZ.Resources;
using System.Collections.Generic;

namespace Test_Task_OZ.Tests
{
    [TestClass]
    public class CommentsTests : JsonHelper
    {
        public CommentsTests() : base("comments")
        {
        }

        [TestMethod]
        public void Test_Case_004_VerifyEmailByCommentsBody()
        {
            try
            {
                string expectedEmail = "Marcia@name.biz"; 
                string partOfComment = "ipsum dolorem"; 


                string httpResponse = GetHttpResponse();

                List<Comments> commentsList = DeserializeResponse<Comments>(httpResponse);

                List<Comments> filteredList = commentsList.FindAll(x => x.Body.Contains(partOfComment));

                if (filteredList.Count == 0)
                    Assert.Fail($"The Comment with required part '{partOfComment}' was not found");

                int i = 0;
                bool isEmailCorrect = false;
                List<string> wrongEmails = new List<string>(); 
                foreach (var comment in filteredList)
                {
                    i++;
                    isEmailCorrect = expectedEmail.Equals(comment.Email);
                    wrongEmails.Add(comment.Email);
                    if (isEmailCorrect)
                        break;
                }

                Assert.IsTrue(isEmailCorrect, $"Experced Email: '{expectedEmail}'. Actual ({i}): '{string.Join(", ", wrongEmails.ToArray())}'");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Error: {ex}");
            }
        }
    }
}
