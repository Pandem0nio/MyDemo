# MyDemo
In this repository there are some demos that I've used during events or trainings.

Demos:
Microsoft Bot Framework demos
- BotDataMgmtTest: in this demos I show how you can use UserData and ConversationData to manage the Welcome message for a user in a conversation.
- testluisbindingaction: here you can see how to use the new LUIS Binding Action features, to easily manage the connection between a LUIS intent and a particular action.
- testAdaptive: this is a test of the new Adaptive Card, announced during //build/. Here, I created a multi-choices card and show the results in another adaptive card with an image.
- fakedata. It is a solutions with two console application project in C#. The first one, CarDeviceIdentity is used to register the other one console application as a device on the Azure IoT Hub. The second one, fakedata is a console application that sends data about a simulated car trip to the Azure IoT Hub. 
- CustomVMSS. It is a Azure Resource Manager template solution to create an Azure Virtual Machine Scale Set with a custom image and a configuration of autoscaling (in this case by the CPU Percentage usage).
- JBotLibrary. It is a cross-platform Portable Class Library in which are implemented the Microsoft Bot Framework Direct Line APIs v1.1 (todo: upgrade to v3.0). The Direct Line APIs make you able to integrate a bot in a custom channel, for example a mobile app or a web site.
- Leaderboard. It is a WebAPI project for Azure Mobile App. It exposes database calls to add a new player score and retrieve a list the first 10 players ordered by scores.
