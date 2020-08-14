using ContactWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ContactWeb.Controllers
{
    public class ContactController : Controller
    {

        const string baseAddress = "https://localhost:44372/api/";

        // GET: Contact
        public ActionResult Index()
        {
            IEnumerable<Contact> contacts = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                // Called Member default GET All records
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request  
                var responseTask = client.GetAsync("contact");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Contact>>();
                    readTask.Wait();

                    contacts = readTask.Result;
                }
                else
                {
                    //Error response received   
                    contacts = Enumerable.Empty<Contact>();
                    ModelState.AddModelError("Error", result.ReasonPhrase);
                }
            }
            return View(contacts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    // Called Member default GET All records
                    //GetAsync to send a GET request   
                    // PutAsync to send a PUT request  
                    var responseTask = client.PostAsJsonAsync(baseAddress + "contact", contact);
                    responseTask.Wait();

                    //To store result of web api response.   
                    var result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        ModelState.AddModelError("Error", result.ReasonPhrase);
                    }
                }
            }
            return View(contact);
        }

        public ActionResult Edit(int id)
        {
            Contact contact = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                // Called Member default GET All records
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request  
                var responseTask = client.GetAsync("contact?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Contact>();
                    readTask.Wait();

                    contact = readTask.Result;
                }
                else {
                    ModelState.AddModelError("Error", result.ReasonPhrase);
                }
            }

            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress + "contact");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Contact>("contact?Id=" + contact.Id, contact);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                else {
                    ModelState.AddModelError("Error", result.ReasonPhrase);
                }
            }
            return View(contact);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP POST
                var deleteTask = client.DeleteAsync("contact?Id=" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                else {
                    ModelState.AddModelError("Error", result.ReasonPhrase);
                }
            }
            return RedirectToAction("Index");
        }      



    }

}
