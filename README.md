# DEPRECATED SeniorProject - ORGANIZE ALL

Check out the website at https://organizeall.azurewebsites.net/

Organize All is a Web Application that allows for user creation of notes, lists, reminders, events, and expenses.
Upon registrations users can login to their accounts and access these five features. Each page is unique and allows
for crud operations for the above stated user features.

Development
Organize All is a .NET Core Web Application developed in Visual Studio
The code is self documented, meaning variable, function, and file names all
directly relate to what the variable, function, or file is accomplishing

Two patterns are used in development, Model-View-Controller (MVC) and Repository-Service pattern
The base repositories contain crud functions that route to the corresponding controllers containing
api routes that are called into action via ajax calls in the javascript files. Ajax calls change the
user's view to update according to the actions they make in the database.

If you have questions or comments contact nathanslack@yahoo.com
