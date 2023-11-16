# Resume Revamp

## Table of Contents
- [Table of Contents](#table-of-contents)
- [About the Project](#about-the-project)
- [Getting Started](#getting-started)
- [Learning Goals](#learning-goals)
- [Tech Stack](#tech-stack)
- [Developer](#developer)

## About the Project
Resume Revamp is a ASP.NET web app that aims to solve the problem of helping job seekers create more impactful and compelling resumes using 3rd Party API calls to  WordsAPI.  This app helps you effortlessly find powerful synonyms and expertly craft your professional documents, making you stand out in the competitive job market. 
With Resume Revamp, you'll leave a lasting impression on potential employers by showcasing your skills and experience with precision and impact.

Resume Revamp was designed and built over the course of five days using agile development including wireframes, user stories, and stand ups. The developer is a former university career advisor and engineering recruiter and therefore the significant influence that the choice of impactful words can have in shaping an effective resume.

<img width="956" alt="image" src="https://github.com/skylarbsandler/ResumeRevamp/assets/95989203/f27dabbf-de29-49b9-a356-36c010d5e366">
<img width="956" alt="image" src="https://github.com/skylarbsandler/ResumeRevamp/assets/95989203/e0242840-0efc-4e68-bb33-e65eb1883ebe">

## Getting Started
Ensure you have the following installed prior to installing the TasteBuddies App:
- [Visual Studio](https://visualstudio.microsoft.com/)
- [pgAdmin](https://www.pgadmin.org/)

<h3>Installation</h3>

1. **Fork or clone a copy of the respository** <br>
`https://github.com/skylarbsandler/ResumeRevamp`

2. **Set up the database** <br>
To run the migration and create the database, open your Package Manage Console and run: <br>
`update-database`

3. **Add DBConnections String** <br>
In `appsettings.json` or User Secrets folder, add: <br>
`{"RESUMEREVAMP_DBCONNECTIONSTRING": "Server=localhost;Database=ResumeRevamp;Port=5432;Username=YOURPGADMINUSERNAME;Password=YOURPGADMINPASSWORD"}`
## Learning Goals
- Create a successful, full-stack web application from a student-led project idea to solve a real world problem
- Implement a GET request to fetch data from an external API, and a way of displaying the fetched data to users
- Develop wireframes and user stories
- Refine the repo through refactoring, logging, and error handling

## Tech Stack
- **Software:** C#, HTML, CSS, Bootstrap, SQL, Javascript, 3rd Party APIs
- **Frameworks/Packages:** Microsoft ASP .NET Core, Microsoft Entity Framework Core, PostgreSQL, XUnit Testing, Serilog

## Developer
**Skylar Sandler** is a student in the Launch program at the Turing School of Software & Design graduating in January 2024. Launch is a 9-month program focused primarily on C#/.NET. Turing School is an accredited, non-profit, fully-remote computer programming school.
[GitHub Profile](https://github.com/skylarbsandler) - [LinkedIn](https://www.linkedin.com/in/skylarbsandler/)
