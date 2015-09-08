
# Commerce House .NET Back-End Developer Code Samples

Clone this repository and complete the 2 samples (located in each folder). Instructions for each of the samples are located in the README files in each folder.

When you have finished, submit a pull request (if you don't have a github account, you can send us a zip file).


Wes Duck sample 1 notes:
All of the requirements for isPangram.exe and sortWords.exe have been met
 - isPangram is the console app meeting the requirements for isPangram.exe
 - sortWords is the console app meeting the requirements for sortWords.exe

Let me know if something doesn't behave as expected

Wes Duck sample 2 notes:
All of the above requirements have been met, including the specific json formats for each of the various methods. 

The url for the pangram service is http://localhost/api/pangrams

I have been running MongoDB as a Windows service, please do the same to test the code that interacts with the database.

There are two pangram web service clients:
 - consoleClient is a C# console application, to test this set the startup project to this and start the solution
 - webClient is an javascript client in a simple web page, to test this set the startup project here
  -to test the limits query string parameter go to a web api url such as http://localhost/api/pangrams?limit=2, chrome is easiest to use for this because by default it converts the json into xml for viewing in the browser

The MongoDB settings are in the web.config of the pangramWebService, and are currently as follows:
      <add key="MongoServerSettings" value="mongodb://localhost:27017/local"/>
The database itself is in the mongo default, data/db, configure this however you want on your system

If an entered pangrams is EXACTLY the same as one already in the database (including spaces and punctuation), it will not be entered into the repository a second time. 

