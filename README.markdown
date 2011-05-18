Prowlin
=======

The purpose of Prowlin is to provide a .Net library and command line tool for sending notifictions to iPhone's/iPad's using the Prowl service and app, check it out at http://www.prowlapp.com.

Prowlin consists of two parts. 

1) A .Net library (dll) that can be used from any .Net code

2) A command line client that uses the library.

Installation
------------

Get the binaries from her or get the code and build it in Visual Studio 2010. I use .Net Framework Version 4.

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
	  int remaingMessages = prowlClient.SendNotification(notification);
	
	  System.Console.WriteLine("Remaing number of messages: {0}", remaingMessages.ToString());



### Command Line

Not implemented yet.


Dependencies
------------

* xUnit for unit tests
* .Net framework 4

