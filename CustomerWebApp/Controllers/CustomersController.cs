using CustomerWebApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerWebApp.Controllers
{
    public class CustomersController : ApiController
    {
        public IEnumerable<Customer> GetCustomers()
        {
            List<Customer> CustomerList = null;
            try
            {
                var getResult = consumeAPI_GET("https://getinvoices.azurewebsites.net/api/Customers");//List URL
                CustomerList = JsonConvert.DeserializeObject<List<Customer>>(getResult);// get List
            }
            catch (Exception)
            {
              
            }

            return CustomerList;
        }

        public Customer GetCustomersById(int custId)
        {
            Customer customerObj = new Customer();
            try
            {
                 var getResult = consumeAPI_GET("https://getinvoices.azurewebsites.net/api/Customer/"+ custId);
                 customerObj = JsonConvert.DeserializeObject<Customer>(getResult);//Single By Id
            }
            catch (Exception)
            {
                throw;
            }            
            return customerObj;
        }

        [HttpPost]
        public string SaveCustomer([FromBody] Customer customerObj)
        {
            try
            {
                var addData = new Customer();
                addData.id = customerObj.id;
                addData.firstname = customerObj.firstname;
                addData.lastname = customerObj.lastname;
                addData.email = customerObj.email;
                addData.phone_Number = customerObj.phone_Number;
                addData.country_code = customerObj.country_code;
                addData.gender = customerObj.gender;
                addData.balance = customerObj.balance;

                var DATA = JsonConvert.SerializeObject(addData);
                var URL = "https://getinvoices.azurewebsites.net/api/Customer";
                var getResult = consumeAPI_Post(URL, DATA);
                return "successfully added";
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public string UpdateCustomers([FromBody] Customer customerObj)
        {            
            try
            {
                var DATA = JsonConvert.SerializeObject(customerObj);
                var URL = "https://getinvoices.azurewebsites.net/api/Customer" + customerObj.id;
                var getResult = consumeAPI_Post(URL, DATA);

                
            }
            catch (Exception)
            {
                throw;
            }
            return "successfully updated";
        }

        [HttpDelete]
        public string DeleteCustomersById(int custId)
        {
            try
            {
                var getResult = consumeAPI_DELETE("https://getinvoices.azurewebsites.net/api/Customer/" + custId);
            }
            catch (Exception)
            {
                throw;
            }
            return "data deleted successfully";

        }


        public static string consumeAPI_Post(string URL, string DATA)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.ContentLength = DATA.Length;
            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(DATA);
            }
            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();

                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    return response;
                }
            }
            catch (Exception e)
            {

            }
            return "No response";
        }

        public static string consumeAPI_GET(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";
            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    return response;
                }
            }
            catch (Exception e)
            {

            }
            return "No response";
        }

        public static string consumeAPI_DELETE(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "DELETE";
            request.ContentType = "application/json";
            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    return response;
                }
            }
            catch (Exception e)
            {

            }
            return "No response";
        }
    }
}
