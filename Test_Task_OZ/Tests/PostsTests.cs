using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test_Task_OZ.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Test_Task_OZ.Tests
{
    [TestClass]
    public class PostsTests : JsonHelper 
    {
        public PostsTests() : base("posts")
        {
        }

        [TestMethod]
        public void Test_Case_001_AddNewPost()
        {
            try
            {
                string httpResponse = GetHttpResponse();

                List<Posts> postsList = DeserializeResponse<Posts>(httpResponse);

                Posts lastPost = postsList.Last();

                Posts modifiedNewPost = new Posts()
                {
                    UserId = 1,
                    Id = null,
                    Title = "Test " + DateTime.Now,
                    Body = "TestBody"
                };
                HttpContent httpContent = ConvertObjectToHttpContent(modifiedNewPost);

                HttpResponseMessage httpResponseMessage = SendPostRequest(httpContent);

                Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode, $"Failed to add new post. Actual Server Response: '{httpResponseMessage.StatusCode}'");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Error: {ex}");
            }
        }

        [TestMethod]
        public void Test_Case_005_VerifyUsernameByPostTitle()
        {
            try
            {
                string expectedTitle = "eos dolorem iste accusantium est eaque quam";
                string expectedUsername = "Patricia Lebsack";

                string httpResponse = GetHttpResponse();

                List<Posts> postsList = DeserializeResponse<Posts>(httpResponse);

                string httpUsersResponse = GetHttpResponse(UrlConstructor("users"));

                List<Users> usersList = DeserializeResponse<Users>(httpUsersResponse);

                Posts actualPost = postsList.Find(x => x.Title.Equals(expectedTitle));

                if (actualPost == null)
                    Assert.Fail($"Expected Title '{expectedTitle}' was not found");

                int actualUserID = actualPost.UserId;

                Users user = usersList.Find(x => x.Id == actualUserID);

                bool isUsernameCorrect = expectedUsername.Equals(user.Name);

                Assert.IsTrue(isUsernameCorrect, $"The author of the expected posts is: '{user.Name}'. Expected: '{expectedUsername}'");

            }
            catch (Exception ex)
            {
                Assert.Fail($"Error: {ex}");
            }
        }
    }
}
