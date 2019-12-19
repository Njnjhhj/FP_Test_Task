using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Test_Task_OZ.Resources;
using Test_Task_OZ.Data;
using System.Linq;

namespace Test_Task_OZ.Tests
{
    [TestClass]
    public class TodosTests : JsonHelper
    {
        public TodosTests() : base("todos")
        {
        }

        [TestMethod]
        public void Test_Case_007_VerifyCompletedTodos()
        {
            DataTable csvFileData = new DataTable();
            bool isCompleted = true;
            int expectedTodosNumber = 3;

            try
            {
                csvFileData = CsvHelper.GetDataTableFromCsv(CsvHelper.FileSource);
            }
            catch (Exception ex)
            {
                Assert.Inconclusive($"Error reading from csv-file: {ex}");
            }

            try
            {
                string httpResponse = GetHttpResponse();


                List<Todos> todosList = DeserializeResponse<Todos>(httpResponse);
                
                foreach (DataRow row in csvFileData.Rows)
                {
                    UserData user = new UserData();
                    user.Id = Convert.ToInt16(CsvHelper.GetCellValue(row, "Id"));
                    user.Name = CsvHelper.GetCellValue(row, "name");

                    List<Todos> completedTodosListByUser = todosList.FindAll(x => x.Completed == isCompleted && x.UserId == user.Id);

                    bool isTodosNumberCorrect = completedTodosListByUser.Count() > expectedTodosNumber;

                    Assert.IsTrue(isTodosNumberCorrect, $"User '{user.Name}' has '{completedTodosListByUser.Count()}' completed Todos. It isn't more than expected - '{expectedTodosNumber}'");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail($"Error: {ex}");
            }
            
        }

    }
}
