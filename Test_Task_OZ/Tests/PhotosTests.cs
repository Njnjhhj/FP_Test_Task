using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test_Task_OZ.Resources;
using System.Collections.Generic;

namespace Test_Task_OZ.Tests
{
    [TestClass]
    public class PhotosTests : JsonHelper
    {
        public PhotosTests() : base("photos")
        {
        }

        [TestMethod]
        public void Test_Case_006_VerifyUserEmailByPhotoTitle()
        {
            try
            {
                string expectedPhotoTitle = "ad et natus qui";
                string expectedEmail = "Sincere@april.biz";

                string httpResponse = GetHttpResponse();

                List<Photos> photosList = DeserializeResponse<Photos>(httpResponse);

                Photos photoByTitle = photosList.Find(x => x.Title.Equals(expectedPhotoTitle));

                if (photoByTitle == null)
                    Assert.Fail($"The Photo with required Title '{expectedPhotoTitle}' was not found");

                string httpAlbumsResponse = GetHttpResponse(UrlConstructor("albums"));

                List<Albums> albumsList = DeserializeResponse<Albums>(httpAlbumsResponse);

                Albums albumById = albumsList.Find(x => x.Id.Equals(photoByTitle.AlbumId));

                if (albumById == null)
                    Assert.Fail($"The Album with required Photo Title '{expectedPhotoTitle}' was not found");

                string httpUsersResponse = GetHttpResponse(UrlConstructor("users"));

                List<Users> usersList = DeserializeResponse<Users>(httpUsersResponse);

                Users userById = usersList.Find(x => x.Id.Equals(albumById.UserId));

                if (userById == null)
                    Assert.Fail($"The Owner of photo with Photo Title '{expectedPhotoTitle}' was not found");

                bool isEmailCorrect = expectedEmail.Equals(userById.Email);

                Assert.IsTrue(isEmailCorrect, $"The Email of Photo Owner is incorrect. Actual: {userById.Email}. Expected: {expectedEmail}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Error: {ex}");
            }
        }
    }
}
