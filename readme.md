### Phone the Web

This project is in response to the COVID-19 pandemic. A lot of important information on both the virus itself and related information is most easily available over the internet. If you have a family member or friend who cannot access the internet for whatever reason this information can be difficult to obtain - particularly from trustworthy sources like health services and governments. Phone the Web is a service which allows you to share webpages with people who can't use the internet as a phone number and code. You enter a webpage, we transcribe it and return a number to you. You then tell your family member or friend to call our phone number from any landline or mobile and enter the number. The service will then read out the page for them.

This project uses 3 languages. C# for the Call Handler with Twilio and for the backend to the website, Python for scraping and transcribing the webpages, and Typescript for the public website. It will be deployed to Azure.

The initial priority for the project is the UK, and therefore the highest priority sites to enable are the NHS website, the gov.uk domains and the BBC. These could change as we progress of course.

![Architecture](phonethewebarchitecture.png)