using Newtonsoft.Json;
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
    public partial class ConversationPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        UserModel temp = new UserModel();
        ContactModel newCon = new ContactModel();
     

        public ConversationPage(UserModel Mode, ContactModel id, List<ConversationModel> conversationList)
        {
            InitializeComponent();
            newCon = id;
            converse.Text = Mode.name;
            CrossCloudFirestore.Current
                .Instance
                .GetCollection("contacts")
                .GetDocument(id.id)
                .GetCollection("conversations")
                .OrderBy("created_at", false)
                .AddSnapshotListener((snapshot, error) =>
                {
                    conversationListview.ItemsSource = conversationList;
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            var obj = JsonConvert.DeserializeObject<ConversationModel>(json);
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    conversationList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c => c.id == obj.id).FirstOrDefault();
                                        conversationList.Remove(item);
                                    }
                                    break;
                            }
                         
                            var conv = conversationListview.ItemsSource.Cast<object>().LastOrDefault();
                            conversationListview.ScrollTo(conv, ScrollToPosition.End, false);

                        }
                    }
                    noCon.IsVisible = conversationList.Count == 0;
                    conversationListview.IsVisible = !(conversationList.Count == 0);
                });



        }
        public async void Send_Message(object sender, EventArgs e)
        {
            
            string ID = IDGenerator.generateID();
            var result = new List<ConversationModel>();
            ConversationModel conversation = new ConversationModel()
            {
                id = ID,
                converseeID = dataClass.loggedInUser.uid,
                message = editor.Text,
                created_at = DateTime.UtcNow
            };
            await CrossCloudFirestore.Current
                .Instance
                .GetCollection("contacts")
                .GetDocument(newCon.id)
                .GetCollection("conversations")
                .GetDocument(ID)
                .SetDataAsync(conversation);
                 
            editor.Text = string.Empty;
            var documents3 = await CrossCloudFirestore.Current
                                .Instance
                                .GetCollection("contacts")
                                .GetDocument(newCon.id)
                                .GetCollection("conversations")
                                .OrderBy("created_at", false)
                                .GetDocumentsAsync();

            foreach (var documentChange in documents3.DocumentChanges)
            {

                var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                var obj = JsonConvert.DeserializeObject<ConversationModel>(json);

                result.Add(obj);


            }
            conversationListview.ItemsSource = result;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }
    }
}