# Dissertation
This is my Dissertation for Masters in Advanced Computer Science from University of Leicester, UK. 

This project have been implemented to allow the Professor and Student to interact via Quiz and Feedback.
Professor can create quiz, and lectures for feedback. Student can attend the quiz and give feedback for lectures. 
Create reports and graphs based on the data. Thus it will allow the Professor and Student to Analysis their Results or Performance.

Application Architecture Design:

This is distributed application.

DB Script is there to create the Database. 

WCF Service has been developed, which contain most of the application logic.
All the data access concepts have implemented in this layer. 
Entity Framework and LINQ quesries have used to access the DB.

Web Application has been desined by using Web forms. 
It consumes all the WCF Services and offers a UI to the End user.
Bootstrap is used for Dashboards. 
