# Business Template

The Business template is a Crosslight application that is a barebone, yet full-featured that provides ready-to-use features commonly found in mobile enterprise business apps.

## Supported Platforms

This template works on the following platforms:

* iOS: iOS 8 and above
* Android: 4.0.3 and above
* Windows Phone: 7 and above

## Project Structure

* App.iOS: Crosslight iOS project, works on iPhones and iPad with Storyboard support.
* App.Android: Crosslight Android project, works on phones and tablets.
* App.Core: Shared Portable Class Library project running Profile78.
* App.DomainModels: Shared Portable Class Library project that contains business object models and Entity Framework domain model.
* App.WebApi: Microsoft WebAPI project that acts as the mobile server.
* App.WinPhone: Crosslight Windows Phone project, works on Windows Phone 7 and above.
* App.WinRT: Crosslight Windows Store project, supports various Windows Store visual states.

## Features

The Business template consists of the following key features:

* Drawer navigation
* Push notification support
* User authentication with social network
* Full integration with WebAPI
* Built-in inventory template to showcase CRUD operations.
* User profile and settings management
* Change password
* Persistent login session
* Full data synchronization support

## Running the template
To see the Business template in action, there are several steps to be completed:

* First, you need to run the WebAPI project on Visual Studio.
* After the project has run, ensure that the network IP of the IIS Express is exposed to the local network. 
* After obtaining your IP address, open up AppService.cs located in the Core project, Infrastructure folder. Change the appSettings.WebServerUrl to the IP of the Windows machine.
* Run the Crosslight app.

After the app has loaded, you'll be presented with the login screen. Getting inside the app is simply done by registering a new user. Use the left navigation drawer to switch between different modules of the app.

## Learn more
To learn how the Business template works in detail, check out [Business Template](http://developer.intersoftsolutions.com/display/crosslight/Business+Template) in the Developer Center.