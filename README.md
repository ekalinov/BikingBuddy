
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
 
 Your Ultimate Destination for Cycling Adventures and Connections!
</div>

Biking Buddy isn't just a web application; it's your all-in-one companion for organizing, discovering, and creating a universe of biking events. Picture a world where every pedal stroke takes you closer to thrilling routes, exciting challenges, and a community of fellow riders who share your passion.

🚴‍♂️ **Explore, Discover, Thrive**: Dive into a curated collection of biking events that cater to every rider's heart's desire. Whether you crave scenic trails, heart-pounding off-road escapades, or leisurely urban explorations, Biking Buddy has an event that fits your style.

🌐 **Craft Your Ride, Your Way**: Unleash your inner event creator and design biking experiences that reflect your imagination. Map out routes that wind through stunning landscapes, challenge participants with varying difficulty levels, and leave everyone with stories to tell.

👥 **Connect, Collaborate, Conquer**: Forge bonds that last a lifetime by joining existing biking teams or creating your own. Biking Buddy is more than an app; it's a thriving community of riders who support and inspire each other. Team up, conquer challenges, and share unforgettable moments.

🏅 **Achievements at Every Turn**: Your journey deserves recognition, and Biking Buddy ensures your accomplishments shine. Track your personal bests, elevation gains, and distances covered. Every ride becomes a chapter in your biking story, proudly displayed in your user account.

🎉 **Lead Your Cycling Tribe**: Elevate your passion by becoming a team manager. Biking Buddy empowers you to captain your own crew, strategize thrilling biking events, and foster a sense of unity that goes beyond the road.

With Biking Buddy, your cycling adventures are amplified. Join us on a ride to remember, where every twist and turn brings you closer to the excitement of biking events and the bonds of a true cycling community. Biking Buddy is not just an app; it's your biking partner, your inspiration, and your window into a world of two-wheeled adventures. Welcome to Biking Buddy - where every ride is an opportunity to discover, connect, and thrive.

<!-- Table of Contents -->
# :notebook_with_decorative_cover: Table of Contents

- [About the Project](#about-the-project)
  * [Tech Stack](#tech-stack)
  * [Features](#features)
  * [Screenshots](#screenshots)
  * [Seeding](#seeding)
  * [Unit Tests](#unit-tests)
- [Getting Started](#getting-started)
  * [Run Locally](#run-locally) 
- [ToDo List](#todo-list)
- [Contact](#handshake-contact)
- [Acknowledgements](#gem-acknowledgements)

<hr />

<!-- About the Project -->
## About the Project
 Biking buddy is a platform for hobby cyclists and mountain bike enthusiasts! Discover a vibrant community where you can form teams, organize events, and showcase your biking gear.
  Connect with fellow riders, share insights, and embark on thrilling adventures together. Elevate your biking experience with us today!

<hr />

<!-- TechStack -->
### Tech Stack
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

 <hr />

<!-- Features -->
###  Features
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
    <li>Can upload .GPX File with track info for their events.</li>  
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

<hr />

<!-- Screenshots -->
### Screenshots

<h2>Database</h2>

![BikeBuddyDb](https://github.com/ekalinov/BikingBuddy/assets/106105805/b95c4069-6c55-438b-952e-d74183173483)

<h2>Authentication flow</h2>
<h3>Register Page</h3>
 
![localhost_7194_Register](https://github.com/ekalinov/BikingBuddy/assets/106105805/c71ece72-16aa-4fdd-8057-35f8850eed98)

<h3>Login Page</h3>
 
  ![localhost_7194_Login](https://github.com/ekalinov/BikingBuddy/assets/106105805/57a662d7-f88b-4661-be00-ef20532bad0d)

<h3>Home Page</h3>
   
 ![localhost_7194_](https://github.com/ekalinov/BikingBuddy/assets/106105805/efa35e05-1ba0-4b53-acca-2d1eff981c37)

<h3>User Profile Page</h3>
 
 ![localhost_7194_User_MyProfile (1)](https://github.com/ekalinov/BikingBuddy/assets/106105805/491340f7-9d65-4375-b89a-4183a73007da)

 <h3>Edit User Page</h3>

 ![localhost_7194_User_Edit](https://github.com/ekalinov/BikingBuddy/assets/106105805/c9aef5d4-839b-4256-aed5-0eedd1f01085)

<h3>All Events Page</h3>  with tabs for Track Info/Comments/Participants

![localhost_7194_Event_Details_eventId=5BA2C5B5-1DF9-4252-8F38-59AEA44C18DB (3)](https://github.com/ekalinov/BikingBuddy/assets/106105805/58dd1de6-55ff-4fb7-9fb1-04efd46fbd5c)

<h3>Modal </h3>  For meeting point detailed map

![image](https://github.com/ekalinov/BikingBuddy/assets/106105805/99befb49-237a-49ce-9cde-27463048f416)

<h3>Add/Edit Event Page </h3> 
With colapsable elements, Leaflet map, autofill track info with JS

![localhost_7194_Event_Edit_eventId=5BA2C5B5-1DF9-4252-8F38-59AEA44C18DB (1)](https://github.com/ekalinov/BikingBuddy/assets/106105805/50b90bf9-6c0f-46c7-a7a5-8480e8b3fae0)

<h3>Team details Page</h3> 

![localhost_7194_Team_Details_teamId=58F28049-75A5-48E7-818C-2723AEABFA68](https://github.com/ekalinov/BikingBuddy/assets/106105805/f4e4e9f6-c91a-4a52-9aa2-7ff4d6969de9)

<h3>Admin Index Page</h3>

 ![localhost_7194_Administration (2)](https://github.com/ekalinov/BikingBuddy/assets/106105805/8386c95f-a76f-4640-9c9c-02c786b20602)

<h3>Admin Events Page</h3>

![localhost_7194_Administration_Events_all](https://github.com/ekalinov/BikingBuddy/assets/106105805/d44ef708-32c5-45e6-8777-a71736597af0)

<h3>Admin Event Preview Modal Page</h3>

![localhost_7194_Administration_Event_Preview_modal](https://github.com/ekalinov/BikingBuddy/assets/106105805/f77d23be-88d4-4d30-91ab-1f8175521b1f)

<h3>Admin Teams Page</h3>

![localhost_7194_Administration_Team_All_TeamsPerPage=15 SearchTerm= Sorting=0 IsDeleted=0 (1)](https://github.com/ekalinov/BikingBuddy/assets/106105805/56cd46de-b2fe-4df5-ab9e-c4125e298e10)

<h3>Admin Users/Admins Page</h3>

![localhost_7194_Administration_users_All](https://github.com/ekalinov/BikingBuddy/assets/106105805/3bc4efc1-26af-4934-a4b9-25eedf49f3df)



<hr />

<!-- Seeding -->
### Seeding
<p>You can review the seeded data <a href="https://github.com/ekalinov/BikingBuddy/tree/main/BikingBuddy/BikingBuddy.Data/Configurations/Seeders">here</a>.</p>

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

<!--
<p>The business layer of the application <a href=""></a> is covered at 70%.</p>
-->


<p>You can check all unit tests by going to <a href="https://github.com/ekalinov/BikingBuddy/tree/main/BikingBuddy/BikingBuddy.Tests">Tests</a>.</p>
<hr />
 


<!-- Run Locally -->
###  Run Locally
<p>Very simple and easy</p>

<ol>
  <li>Download the project ZIP folder.</li>
  <li>Replace the data with your own. (SqlServer connection)</li>
  <li>Ensure that you entered correct data.</li>
  <li>Start the project. (The app will automatically apply all migrations and seed data)</li>
</ol>


<hr />
<!-- Getting Started -->

##  Getting started:

Use App to register user with this email who will became  first Administrator 
email: admin@bbuddy.com 

<!-- Roadmap -->
<hr />

###  TODO List:

* [x] Move default user/event/team photo to wwwroot and make migrations for the default values with new path.
* [x] Don't allow user to Join/Leave past events. 
* [ ] When Delete user to delete all his events and teams.
* [x] User can participate in only one club.
* [x] Delete users are not allowed to login.
* [ ] Add caching to all events and teams. 
* [x] Add Location on map .
* [ ] Add info container on All Teams/Events pages.
* [x] Events can be free and paid.
* [x] Event organizer to add .gpx file for the event, if want.
* [ ] Participants to be able to download .gpx file of the event if there is such.
* [x] Visualize .gpx track on the event details page.
* [ ] Show forecast for the event location in details.

      

<hr />


<!-- Contact -->
## :handshake: Contact

Emilian Kalinov - [LinkedIn](https://www.linkedin.com/in/emilian-kalinov-2241b1230/) 

Project Link: [BikingBuddy](https://github.com/ekalinov/BikingBuddy)

<hr />

<!-- Acknowledgments -->
## :gem: Acknowledgements

My useful resources and libraries that I used to create this readme file.

 - [Shields.io](https://shields.io/)
 - [Awesome README](https://github.com/matiassingers/awesome-readme)
 - [Emoji Cheat Sheet](https://github.com/ikatyang/emoji-cheat-sheet/blob/master/README.md#travel--places)
 - [Readme Template](https://github.com/othneildrew/Best-README-Template)
 - [Louis3797 Readme Template](https://github.com/Louis3797/awesome-readme-template).
