# BikingBuddy

Warning: 
Use App to register user with this email who will became  first Administrator 
email: admin@bbuddy.com  

NB: The IDE used for the project is JetBrainsRider 2023.1.2

TODO: 
Front End Finalizations 
Test Coverage 

 C# Web Module @SoftUni FinalProject
.

# BikingBuddy

Warning: 
Use App to register user with this email who will became  first Administrator 
email: admin@bbuddy.com  

NB: The IDE used for the project is JetBrainsRider 2023.1.2
![userDefautIcon](https://github.com/ekalinov/BikingBuddy/assets/106105805/fdcc348d-fd96-4e2c-8f07-c7c949b96000)

TODO: 
Front End Finalizations 
Test Coverage 

 C# Web Module @SoftUni FinalProject
------------------------------------------
<!--
Hey, thanks for using the awesome-readme-template template.  
If you have any enhancements, then fork this project and create a pull request 
or just open an issue with the label "enhancement".

Don't forget to give this project a star for additional support ;)
Maybe you can mention me or this repo in the acknowledgements too
-->
<div align="center">  
  <img src="https://github.com/ekalinov/BikingBuddy/assets/106105805/6bca2fad-2115-482c-856e-eb87c08dcfcb" alt="logo" width="500" height="auto" />
  <h1>BikingBuddy Worldwide</h1>
  
  <p>
    Fuel your passion for fitness!
  </p>
# Biking Buddy: Your Ultimate Destination for Cycling Adventures and Connections!

Biking Buddy isn't just a web application; it's your all-in-one companion for organizing, discovering, and creating a universe of biking events. Picture a world where every pedal stroke takes you closer to thrilling routes, exciting challenges, and a community of fellow riders who share your passion.

🚴‍♂️ **Explore, Discover, Thrive**: Dive into a curated collection of biking events that cater to every rider's heart's desire. Whether you crave scenic trails, heart-pounding off-road escapades, or leisurely urban explorations, Biking Buddy has an event that fits your style.

🌐 **Craft Your Ride, Your Way**: Unleash your inner event creator and design biking experiences that reflect your imagination. Map out routes that wind through stunning landscapes, challenge participants with varying difficulty levels, and leave everyone with stories to tell.

👥 **Connect, Collaborate, Conquer**: Forge bonds that last a lifetime by joining existing biking teams or creating your own. Biking Buddy is more than an app; it's a thriving community of riders who support and inspire each other. Team up, conquer challenges, and share unforgettable moments.

🏅 **Achievements at Every Turn**: Your journey deserves recognition, and Biking Buddy ensures your accomplishments shine. Track your personal bests, elevation gains, and distances covered. Every ride becomes a chapter in your biking story, proudly displayed in your user account.

🎉 **Lead Your Cycling Tribe**: Elevate your passion by becoming a team manager. Biking Buddy empowers you to captain your own crew, strategize thrilling biking events, and foster a sense of unity that goes beyond the road.

With Biking Buddy, your cycling adventures are amplified. Join us on a ride to remember, where every twist and turn brings you closer to the excitement of biking events and the bonds of a true cycling community. Biking Buddy is not just an app; it's your biking partner, your inspiration, and your window into a world of two-wheeled adventures. Welcome to Biking Buddy - where every ride is an opportunity to discover, connect, and thrive.

  <p>
    For further info review <strong>[Environment Variables]</strong> and <strong>[Getting Started]</strong> in Table of Contents.
  </p>
  


<br />

<!-- Table of Contents -->
# 📔 Table of Contents

- [About the Project](#star2-about-the-project)
  * [Tech Stack](#space_invader-tech-stack)
  * [Features](#dart-features)
  * [Screenshots](#camera-screenshots)
  * [Seeding](#seeding)
  * [Unit Tests](#unit-tests)
  * [Color Reference](#art-color-reference)
  * [Environment Variables](#key-environment-variables)
- [Getting Started](#toolbox-getting-started)
  * [Run Locally](#running-run-locally)
- [Usage](#eyes-usage)
- [Roadmap](#compass-roadmap)
- [License](#warning-license)
- [Certificate](#certificate)
- [Contact](#handshake-contact)
- [Acknowledgements](#gem-acknowledgements)

<!-- About the Project -->
## 🌟 About the Project
 Biking buddy is a platform for hobby cyclists and mountain bike enthusiasts! Discover a vibrant community where you can form teams, organize events, and showcase your biking gear.
  Connect with fellow riders, share insights, and embark on thrilling adventures together. Elevate your biking experience with us today!

<!-- TechStack -->
### 👾 Tech Stack
<details>
  <summary>Server</summary>
  <ul>
    <li><a href="https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0">ASP.NET Core 6.0</a></li>
    <li><a href="https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/areas?view=aspnetcore-6.0">ASP.NET Core Areas</a></li>
    <li><a href="https://learn.microsoft.com/en-us/ef/core/">Entity Framework Core 6.0</a></li>
    <li><a href="https://automapper.org/">AutoMapper</a></li>
    <li><a href="https://getbootstrap.com/">Bootstrap</a></li>
    <li><a href="https://jquery.com/">jQuery</a></li>
    <li><a href="https://nunit.org/">NUnit</a></li>
    <li><a href="https://www.nuget.org/packages/Moq">Moq</a></li>    
    <li><a href="https://github.com/CodeSeven/toastr">Toastr (non-blocking notifications)</a></li> 
    <li><a href="https://www.w3schools.com/icons/fontawesome_icons_intro.asp">FontAwesome</a></li>
  </ul>
</details>

<details>
<summary>Database</summary>
  <ul>
    <li><a href="https://www.microsoft.com/en-us/sql-server/sql-server-downloads">Microsoft SQL Server</a></li>
    <li><a href="https://learn.microsoft.com/en-us/sql/t-sql/language-reference?view=sql-server-ver16">T-SQL (Transact-SQL)</a></li>
    <li><a href="https://sqldbm.com/Home/">SqlDBM (SQL Database Modeler)</a></li>
  </ul>
</details>
 

<!-- Features -->
### 🎯 Features
There is an Administration Area in the project and Common Layer for Authenticated Users.
<details>
  <summary>Custom Authentication Flow</summary>
  <ul>
    <li>Register.</li>
    <li>Login.</li>
    <li>Remember me.</li>
    <li>Send Email for Reset Forgot Password.</li>
    <li>Reset Password.</li>
  </ul>
</details>

<details>
  <summary>Guest</summary>
  <ul>
    <li>Gusts has access to Home Page. </li>  
    <li>Gusts has access to Events Page. </li>
    <li>Gusts has access to Event Details Page. </li>
    <li>Gusts has access to Team Page. </li>
  </ul>
</details>
<details>
  <summary>Authenticated User</summary>
  <ul> 
    <li>Can view and edit his profile information such as Profile Picture, Address, Equipment and etc.</li> 
    <li>Can Join/Leave events.</li>
    <li>Can Create events.</li>
    <li>Can Edit or Delete their events.</li>
    <li>Can Cad add Gallery Photos.</li>
    <li>Can comment on events page.</li>
    <li>Can can see other participants in events where they also participate.</li>
    <li><strong>TODO: CANNOT</strong> join already ended events.</li>
    <li><strong>TODO: CANNOT</strong> leave already ended events.</li>
  </ul>
</details>

<details>
  <summary>Team Manager</summary>
  <ul>
    When user create new team he became Team Manager of this team!   
    <li>Can see all his Members requests and review their memberships</li>
    <li>Can see all his Team Members</li> 
    <li>Can edit team info.</li>
    <li>Can delete his team.</li>
    <li>Can Add and remove members.</li> 
  </ul>
</details>

<details>
  <summary>Administrator</summary>
  <ul>
    <li>Can use <strong>Authenticated User</strong> functionality.</li>
    <li>Can use <strong>Team Manager</strong> functionality.</li>
    <li>Can see all <strong>active</strong> users in the app.</li>
    <li>Can see all <strong>deleted</strong> Admins in the app.</li>
    <li>Can see all <strong>active</strong> Admins in the app.</li> 
    <li>Can make other users Admins a role.</li>
    <li>Can see all <strong>active</strong>teams in the app with their information about Members,Requests, Team Managers and etc.</li>
    <li>Can see all <strong>deleted</strong> teams in the app with their information about Members,Requests, Team Managers and etc.</li>
    <li>Can see all <strong>active</strong>Events in the app with their details.</li>
    <li>Can see all <strong>deleted</strong>Events in the app with their details.</li> 
  </ul>
</details>

<!-- Screenshots -->
### 📷 Screenshots

<h2>Database</h2>

![MyGymWorld-Database_lwgzpk](https://github.com/GeorgiKostadinovPro/MyGymWorld/assets/72508846/6817ce80-510b-44f4-b4f1-249f37ca1343)

<h2>Authentication flow</h2>
<h3>Register Page</h3>
 

<h3>Login Page</h3>
 
  

<h3>Home Page Top Part (Not Authenticated User)</h3>
   
 

<h3>User Profile Page</h3>
 
 

<h3>Edit User Profile Page</h3> 
 

<h3>Admin Dashboard</h3>
 

<h3>Manager Request Details Page (For Admin)</h3> 

<h3>Manager Roles Page (For Admin)</h3>
 

<h3>Manage Users Page (For Admin)</h3> 
<h3>Manage Gyms Page (For Admin)</h3>
 

<h3>All Gyms Filter Page</h3> 

<h3>Gym Details Page (Not Joined)</h3> 

<h3>Gym Details Page (Joined)</h3>
 
 

<h3>Gym Details Page (For Manager)</h3>
 
<h3>Comments Page</h3>
 

<h3>User Joined Gyms Page</h3> 

<h3>Gym Events Page</h3>
 
<h3>Event Details Page (Not Participated)</h3>
 

<h3>Gym Event Details Page (Participated)</h3>
 

<h3>Gym Event Details Page (For Manager)</h3>
 

<h3>User Joined Events Page</h3>
 
<h3>Gym Articles Page (Not Subscribed)</h3> 

<h3>Gym Articles Page (Subscribed)</h3> 

<h3>Gym Article Details Page</h3> 

<h3>Gym Articles Details Page (For Manager)</h3>
 
<h3>Gym Memberships Page</h3>
 

<h3>Gym Memberships Details Page</h3>
 

<h3>Gym Membership Details After Successful Payment</h3>
  
 
<h3>Create Gym Page (For Manager)</h3>  
<h3>Edit Gym Page (For Manager)</h3>
 

<h3>Manage Gyms Page (For Manager)</h3>
 

<h3>Create Event Page (For Manager)</h3>
 

<h3>Edit Event Page (For Manager)</h3>
 

<h3>Create Article Page (For Manager)</h3>
 

<h3>Edit Article Page (For Manager)</h3>
 

<h3>Create Membership Page (For Manager)</h3>
 

<h3>Edit Membership Page (For Manager)</h3>
 

<h3>Manage Gym Payments Page (For Manager)</h3>
 

<hr />

<!-- Seeding -->
### Seeding
<p>You can review the seeded data <a href="https://github.com/GeorgiKostadinovPro/MyGymWorld/tree/master/MyGymWorld.Data/Seeding">here</a>.</p>

<strong>Seeded Data</strong>
<details>
   <summary>Common Entities</summary>
    <ul>
     <li>Coutries</li>
     <li>Towns</li>
     <li>Activity Types</li>
   </ul>
</details> 
<hr />

<!-- Unit tests -->
### Unit Tests
<p>The technologies that were used for the unit testing are:</p>
<ul>
  <li><a href="https://nunit.org/">NUnit</a></li>
  <li><a href="https://www.nuget.org/packages/Moq">Moq</a></li>
  <li><a href="https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli">InMemory Database</a></li>
</ul>
<p>The business layer of the application <a href="https://github.com/GeorgiKostadinovPro/MyGymWorld/tree/master/MyGymWorld.Core">MyGymWorld.Core</a> is covered at 70%.</p>

![image](https://github.com/GeorgiKostadinovPro/MyGymWorld/assets/72508846/3c48ce04-bac6-400f-9796-4aecc3fae673)

![image](https://github.com/GeorgiKostadinovPro/MyGymWorld/assets/72508846/e69b51f1-ecdd-489e-8eda-d380228bc8d0)

<p>You can check all unit tests by going to <a href="https://github.com/GeorgiKostadinovPro/MyGymWorld/tree/master/MyGymWorld.Core.Tests">MyGymWorld.Core.Tests</a>.</p>

<hr />

<hr />

<!-- Getting Started -->
## 	🧰 Getting Started

<!-- Run Locally -->
### 🏃 Run Locally
<p>Very simple and easy</p>

<ol>
  <li>Download the project ZIP folder.</li>
  <li>Replace the data with your own. (SqlServer connection, API keys and secrets, ApplicationUrl)</li>
  <li>Ensure that you entered correct data.</li>
  <li>Start the project. (The app will automatically apply all migrations and seed data)</li>
</ol>


<hr />


<!-- Roadmap -->
## 🧭 TODO List:

* [x] Built a Solid Project Architecture.
* [x] Write a Scheme for the Database.
* [x] Create the Database.
* [x] Create extension methods for IServiceCollection and ClaimsPrinciple.
* [x] Create the Areas - Admin and Manager.
* [x] Start building the app.
* [x] Add SendGrid, Cloudinary, Stripe and QRCoder Services.
* [x] Create Notifications functionality. (Receive, Read, Delete, Pagination)
* [x] Create Authentication Flow. (UserService, AccountService, UserController, AccountController, Views)
* [x] Create User Profile. (Info, Edit, Upload Profile picture, Delete profile picture)
* [x] Create Become Manager functionality.
* [x] Create Approve and Reject Manager Request. (In Admin Area)
* [x] Create CRUD for Users, Roles and Gyms. (In Admin Area)
* [x] Create CRUD for Manager Gyms. (In Manager Area)
* [x] Create Join Gym functionality.
* [x] Create pagination, filtering, sorting and searching functionality for Gyms and Joined Gyms.
* [x] Create Likes and Dislikes functionality.
* [x] Create Comments and Reply Comments functionality.
* [x] Create CRUD for Gym Events. (In Manager Area)
* [x] Create Join and Leave Events functionality.
* [x] Create pagination, filtering, sorting and searching functionality for Gym Events and Joined Events.
* [x] Create CRUD for Gym Articles. (In Manager Area)
* [x] Create Read, Subscribe and Unsubscribe for Articles functionality.
* [x] Create pagination, filtering, sorting and searching functionality for Gym Articles.
* [x] Create CRUD for Gym Memberships. (In Admin Area)
* [x] Create Buy Memberships for Gym functionality.
* [x] Create paging, filtering, sorting and searching functionality for Gym Memberships and Bought Memberships.
* [x] Finish the Payment Flow.
* [x] Cover the project business layer with Unit Tests.
* [ ] Create Chat In Gym funationality. (In Admin Area)
* [ ] Create Chat for Joined Gym Users functionality.

<hr />


<!-- Contact -->
## 🤝 Contact

Your Name - [LinkedIn](https://www.linkedin.com/in/emilian-kalinov-2241b1230/) 

Project Link: [MyGymWorld](https://github.com/ekalinov/BikingBuddy)

<hr />

<!-- Acknowledgments -->
## 💎 Acknowledgements

My useful resources and libraries that I used to create this readme file.

 - [Shields.io](https://shields.io/)
 - [Awesome README](https://github.com/matiassingers/awesome-readme)
 - [Emoji Cheat Sheet](https://github.com/ikatyang/emoji-cheat-sheet/blob/master/README.md#travel--places)
 - [Readme Template](https://github.com/othneildrew/Best-README-Template)
 - [Louis3797 Readme Template](https://github.com/Louis3797/awesome-readme-template).
