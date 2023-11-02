# Sprout.Exam.WebApp


 Database : MSSQL, 
 Framework : net5.0, netcoreapp3.1, Language : C#, Javascript

* Before running the application:
  - Install SQL server/ SQL Express 2016 or up.
  - Visual Studio 2019 with .net 5 Installed
  - Updated node js
    - ( **Note**: if you are going to use node version 17 above add "NODE_OPTIONS=--openssl-legacy-provider" on start and build under scripts on package.json.).
  - Run npm --install
  - Restore SproutExamDb database backup file located on database folder .
  - Change the connection string credentials on appsetting to your local db credentials.
 
* Questions:
  - Q. If we are going to deploy this on production, what do you think is the next improvement that you will prioritize next? This can be a feature, a tech debt, or an architectural design.
    - A. We need to create new table for employee salary, also we need to upgrade to .net 6 since .net 5 is deprecated.




