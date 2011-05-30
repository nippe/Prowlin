Prowlin
=======

The purpose of Prowlin is to provide a .Net library and command line tool for sending notifictions to iPhone's/iPad's using the Prowl service and app, check it out at http://www.prowlapp.com.

Prowlin consists of two parts. 

1) A .Net library (dll) that can be used from any .Net code

2) A command line client that uses the library.

Installation
------------

Get the binaries [here](https://github.com/nippe/Prowlin/downloads) or get the code and build it in Visual Studio 2010. I use .Net Framework Version 4.

Usage
-----
### Code


      INotification notification = new Prowlin.Notification()
	                                 {
 	                                    Application = "Prowlin.Console",
 	                                    Description = "Testing",
 	                                    Event = "Some Event",
 	                                    Priority = NotificationPriority.High,
 	                                    Url = "http://www.nnihlen.com/blog"
 	                                };
	  notification.AddApiKey("<your-very-secret-apikey-goes-here>");

	  ProwlClient prowlClient = new ProwlClient();
	  NotificationResult notificationResult = prowlClient.SendNotification(notification);

	  System.Console.WriteLine("Remaing number of messages: {0}", notificationResult.RemainingMessageCount.ToString());
	

### Command Line

      > ProwlinCmd -k one_apikey -e "event X" -a "Application Y"
      > ProwlinCmd -k apikey_one,apikey_two,... -e "event X" -a "Application Y"

      Options:
        -k, -key                APIKEY(s)       Prowl API key, one or many separated by commas
        -a, -application        APPLICATION     Application
        -e, -event              EVENT           Event
        -d, -description        DESCRIPTION     Description
        -p, -priority           PRIORITY        Priority - VERYLOW, MODERATE, NORMAL(default), HIGH, EMERGENCY
        -u, -url                URL             Url
		-v, -verify             VERIFIY         Verification of key used in combination with -k (APIKEY)
                                                and (optional) -p (PROVIDER KEY)
        -r, -retrievetoken      RETRIVE/TOKEN   Get a registration token for use in retrieve/apikey and
                                                the associated URL for the user to approve the request. Use together with -providerkey
        -pro, -providerkey      PROVIDER KEY    ProviderKey  to use in conjunction with -r (retrieve/token)
        -n, -newkey             GET APIKEY      Get new api key  to use in conjunction with -t (token) and
                                                -pro (providerkey)
        -h, -help               HELP            This screen


Dependencies
------------

* xUnit for unit tests
* Args for parsing command line arguments
* .Net framework 4

