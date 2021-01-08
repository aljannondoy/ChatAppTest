using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Plugin.CloudFirestore;
using ChatApp_Ondoy.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseAuthService))]
namespace ChatApp_Ondoy.Droid
{
    public class FirebaseAuthService : iFirebaseAuth
    {
        DataClass dataClass = DataClass.GetInstance;
        public FirebaseAuthResponseModel IsLoggedIn()
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Currently logged in." };
                if (FirebaseAuth.Instance.CurrentUser.Uid == null)
                {
                    response = new FirebaseAuthResponseModel() { Status = false, Response = "Currently logged out." };
                    dataClass.isSignedIn = false;
                    dataClass.loggedInUser = new UserModel();
                }
                else
                {
                    dataClass.loggedInUser = new UserModel()
                    {
                        uid = FirebaseAuth.Instance.CurrentUser.Uid,
                        email = FirebaseAuth.Instance.CurrentUser.Email,
                        name = dataClass.loggedInUser.name,
                        userType = dataClass.loggedInUser.userType,
                        created_at = dataClass.loggedInUser.created_at
                    };
                    dataClass.isSignedIn = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
                dataClass.isSignedIn = false;
                dataClass.loggedInUser = new UserModel();
                return response;
            }
        }

        public async Task<FirebaseAuthResponseModel> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Login successful." };
                IAuthResult result = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                if (result.User.IsEmailVerified && email == result.User.Email)
                {
                    var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("users")
                                        .GetDocument(result.User.Uid)
                                        .GetDocumentAsync();
                    var yourModel = document.ToObject<UserModel>();

                    dataClass.loggedInUser = new UserModel()
                    {
                        uid = result.User.Uid,
                        email = result.User.Email,
                        name = yourModel.name,
                        userType = yourModel.userType,
                        created_at = yourModel.created_at
                    };
                    dataClass.isSignedIn = true;
                }
                else
                {
                    FirebaseAuth.Instance.CurrentUser.SendEmailVerification();
                    response.Status = false;
                    response.Response = "Email not verified. Sent another verification email.";
                    dataClass.loggedInUser = new UserModel();
                    dataClass.isSignedIn = false;
                }

                return response;
            }
            catch (Exception ex)
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
                dataClass.isSignedIn = false;
                return response;
            }
        }

        public async Task<FirebaseAuthResponseModel> ResetPassword(string email)
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Email has been sent to your email address." };
                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
                return response;
            }
            catch (Exception ex)
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
                return response;
            }
        }

        public FirebaseAuthResponseModel SignOut()
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Sign out successful." };
                FirebaseAuth.Instance.SignOut();
                dataClass.isSignedIn = false;
                dataClass.loggedInUser = new UserModel();
                return response;
            }
            catch (Exception ex)
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
                dataClass.isSignedIn = true;
                return response;
            }
        }

        public async Task<FirebaseAuthResponseModel> SignUpWithEmailPassword(string name, string email, string password)
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Sign up successful. Verification email sent." };
                await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                FirebaseAuth.Instance.CurrentUser.SendEmailVerification();

                int ndx = email.IndexOf("@");
                int cnt = email.Length - ndx;
                string defaultName = string.IsNullOrEmpty(name) ? email.Remove(ndx, cnt) : name;

                dataClass.loggedInUser = new UserModel()
                {
                    uid = FirebaseAuth.Instance.CurrentUser.Uid,
                    email = email,
                    name = defaultName,
                    userType = 0,
                    created_at = DateTime.UtcNow
                };
                return response;
            }
            catch (Exception ex)
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
                return response;
            }
        }
    }
}