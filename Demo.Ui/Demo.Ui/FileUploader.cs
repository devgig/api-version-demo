using Microsoft.VisualBasic.FileIO;
using RestSharp;
using System;
using System.Net;

namespace Demo.Ui
{
    public class FileUploader
    {
        public bool Upload(string uploadFile)
        {

            Parse_CSV(uploadFile);

            RestClient restClient = new RestClient("https://localhost:44308/api/v1/upload");
            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "multipart/form-data");
            restRequest.AddFile("file", uploadFile);
            var response = restClient.Execute(restRequest);
            return response.StatusCode == HttpStatusCode.OK;
        }

        static void Parse_CSV(String Filename)
        {
            using (TextFieldParser parser = new TextFieldParser(Filename))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.TrimWhiteSpace = true;
                while (!parser.EndOfData)
                {
                    string[] fieldRow = parser.ReadFields();
                    foreach (string fieldRowCell in fieldRow)
                    {
                        // todo
                    }
                }
            }
        }


    }
}
