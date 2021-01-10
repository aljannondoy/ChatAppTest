﻿using Newtonsoft.Json;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_Ondoy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        UserModel tempemail = new UserModel();

        public ChatPage()
        {
            InitializeComponent();
            var contactList = new List<ContactModel>();
            CrossCloudFirestore.Current
                .Instance
                .GetCollection("contacts")
                .WhereArrayContains("contactID", dataClass.loggedInUser.uid)
                .AddSnapshotListener((snapshot, error) =>
                {
                    loading.IsVisible = true;
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            var obj = JsonConvert.DeserializeObject<ContactModel>(json);
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    contactList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        contactList.Remove(item);
                                    }
                                    break;
                            }

                        }
                    }
                    contactsList.ItemsSource = contactList;
                    noCont.IsVisible = contactList.Count == 0;
                    contactsList.IsVisible = !(contactList.Count == 0);
                    loading.IsVisible = false;
                });
           
        }
        private async void Search_Tapped(object sender, EventArgs e)
        {
            var result = new List<UserModel>();
            var email = (Entry)sender;



            var documents = await CrossCloudFirestore.Current
                                .Instance
                                .GetCollection("users")
                                .WhereEqualsTo("email", email.Text)
                                .GetDocumentsAsync();

            foreach (var documentChange in documents.DocumentChanges)
            {

                var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                var obj = JsonConvert.DeserializeObject<UserModel>(json);


                result.Add(obj);
                tempemail = obj;
            }
            resultList.ItemsSource = result;
            
           
            if (result.Count == 0)
            {
                await DisplayAlert("", "User not found.", "Okay");
                await Navigation.PopModalAsync(true);
            }
            else
            {
                SearchEntry.IsVisible = false;
                if(contactsList.IsVisible == true || noCont.IsVisible == true)
                {
                    noCont.IsVisible = false;
                    contactsList.IsVisible = false;
                }
                CloseList.IsVisible = true;
                resultList.IsVisible = true;
            }

        }
        private void Close_Clicked(object sender, EventArgs e)
        {
           Application.Current.MainPage = new TabPage();
        }
        private async void AddContact(object sender, EventArgs e)
        {
            var result = new List<UserModel>();
        
            var documents = await CrossCloudFirestore.Current
                                .Instance
                                .GetCollection("users")
                                .WhereEqualsTo("email",dataClass.loggedInUser.email)
                                .WhereArrayContains("contacts", tempemail.uid)
                                .GetDocumentsAsync();

            foreach (var documentChange in documents.DocumentChanges)
            {

                var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                var obj = JsonConvert.DeserializeObject<UserModel>(json);
                result.Add(obj);
            }

            if (dataClass.loggedInUser.email == tempemail.email)
            {
                await DisplayAlert("Failed", "You cannot add yourself.", "Okay");
            }
            else if (result.Count != 0)
            {
                await DisplayAlert("Failed", "You both already have a connection.", "Okay");
            }
            else
            {
                bool res = await DisplayAlert("Add Contact", "Would you like to add "+tempemail.name+"?", "Yes", "No");
                if (res)
                {
                    ContactModel contact = new ContactModel()
                    {
                        id = IDGenerator.generateID(),
                        contactID = new string[] { DataClass.GetInstance.loggedInUser.uid, tempemail.uid },
                        contactEmail = new string[] { DataClass.GetInstance.loggedInUser.email, tempemail.email },
                        contactName = new string[] { DataClass.GetInstance.loggedInUser.name, tempemail.name },
                        created_at = DateTime.UtcNow

                    };
                    await CrossCloudFirestore.Current
                        .Instance
                        .GetCollection("contacts")
                        .GetDocument(contact.id)
                        .SetDataAsync(contact);
                    if (dataClass.loggedInUser.contacts == null)
                        dataClass.loggedInUser.contacts = new List<string>();
                    dataClass.loggedInUser.contacts.Add(tempemail.uid);
                    await CrossCloudFirestore.Current
                        .Instance
                        .GetCollection("users")
                        .GetDocument(dataClass.loggedInUser.uid)
                        .UpdateDataAsync(new { contacts = dataClass.loggedInUser.contacts });

                    if (tempemail.contacts == null)
                        tempemail.contacts = new List<string>();
                    tempemail.contacts.Add(dataClass.loggedInUser.uid);
                    await CrossCloudFirestore.Current
                        .Instance
                        .GetCollection("users")
                        .GetDocument(tempemail.uid)
                        .UpdateDataAsync(new { contacts = tempemail.contacts });

                    await DisplayAlert("Success", "Contact added!", "Okay");
                }
              
            }
           
        }
        private void ContactsList_Tapped(object sender, EventArgs e)
        {

        }
        public void Clear_Clicked(object sender, EventArgs e)
        {
            
            searchcon.Text = string.Empty;
            searchcon.Focus();
        }
        private void changeText(object sender, TextChangedEventArgs e)
        {

            var txt = (CustomEntry)sender;
            if(txt.Text.Length > 0)
            {
                clear.IsVisible = true;
            }
            else
            {
                clear.IsVisible = false;
            }
        }
    }
}