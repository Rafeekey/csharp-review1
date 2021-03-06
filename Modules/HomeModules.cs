using Nancy;
using System.Collections.Generic;
using AddressBook.Objects;

namespace AddressBookApp
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Contact> contactList = Contact.GetAll();
        return View["index.cshtml", contactList];
      };
      Get["/contact/form"] = _ => {
        return View["contact_form.cshtml"];
      };
      Post["/contact/new"] = _ => {
        var newContact = new Contact(
        Request.Form["contact-first-name"] + " " + Request.Form["contact-last-name"],
        Request.Form["contact-city"] + ", " + Request.Form["contact-state"] + " / " + Request.Form["contact-street"] + ", " + Request.Form["contact-zipcode"],
        Request.Form["contact-phone-number"]);
        return View["contact_new.cshtml", newContact];
      };
      Get["/contacts/{id}"] = parameters => {
        var contact = Contact.FindContact(parameters.id);
        return View["contact_view.cshtml", contact];
      };
      Post["/contacts/clear"] = _ => {
        if(Contact.CheckContacts(Request.Form["delete-contact-name"]))
        {
          Contact.DeleteContact(Request.Form["delete-contact-name"]);
          List<Contact> editedContactList = Contact.GetAll();
          return View["contact_delete.cshtml", editedContactList];
        }
        List<Contact> contactList = Contact.GetAll();
        return View["index.cshtml", contactList];
      };
    }
  }
}
