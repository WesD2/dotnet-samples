# Instructions

Create a ReST-based web service with the following endpoint. You may use any web framework you are comfortable with. You 
will also need to setup a local Mongo database. You can develop in Visual Studio or Xamarin Studio.

------

`http://localhost:8080/pangrams`

(The hostname and port are not important - you may use whatever is easiest to set up).

This endpoint should accept a POST request with a parameter named "sentence". It must return a JSON object response of the following format:

    {
        "isPangram": [true / false]
    }

If the sentence is a pangram, you must save it to a collection called "pangrams".

The same endpoint should also accept a GET request and a query parameter named "limit", and return a JSON array response
containing a list of every pangram sentence that has been POSTed to the endpoint, limited by the "limit" query
parameter.

`http://localhost:8080/pangrams?limit=5`

    {
        "pangrams": [
            "The quick brown dog jumps over the lazy dog",
            "A wizard's job is to quickly vex chumps in fog",
            "Pack my box with five dozen liquor jugs",
            "Sphinx of black quartz, judge my vow.",
            "Waltz, nymph, for quick jigs vex Bud."
        ]
    }


Wes Duck notes:
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


