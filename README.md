# C-Chat 2

This is a simple voice chat client, that allows a potentially unlimited number of clients to connect to voice chat servers.
**It's primary purpose is to function on a local network only.**
Those servers can be created by anyone. The person hosting that server has control over the other connected clients, so they can kick and mute other people.

## How to use
Download and run CrapApp2.exe. It will open a windows form that has the basic controls.
First, choose a username. It is important to press the **[Confirm Name]** button for the name to be updated. You will not be able to do anything else until you have done this step.

![Enter Name](https://github.com/Epicguru/CChat2/blob/master/Guide%20Images/Name.png)

Now the app will automatically scan the local network for open servers. The port that it scans on can be changed here:

![Scanning Port](https://github.com/Epicguru/CChat2/blob/master/Guide%20Images/Port%20and%20Refresh.png)

Remember to press refresh when you change this port number.
If you see the name of the server you want to connect to, click on it in the list and then press the **[Connect]** button.
You should now be connected. Say hello!

You can also host a server. To do this, disconnect from any server that you were connected to (if any).
Now type the name of the new server, and the port number you want to host it on:

![Server Name and Port](https://github.com/Epicguru/CChat2/blob/master/Guide%20Images/Host%20Port%20and%20Name.png)

Press the **[Create New Server]** button. You should now be hosting a new server. There are some important things to note about server hosting:
1. Only one server can be hosted per device per port. Attempting to open more than one server with the same port on the same device will most likely crash the app.
2. Servers can have duplicate names. This can be confusing, so try to give them unique names.
3. There are no port restrictions imposed by the app. Despite this, you should never use ports below 1024. These are reserved for system use and attempting to use them could mess up you device.

Other users can now join the server.

## Download
You can download the most recent build in the releases section on github ([here](https://github.com/Epicguru/CChat2/releases)).
>_**When downloading, your browser may warn you about suspicious or harmful files. This is just because the download contains .dll and .exe files. There is nothing harmful about this program.**_

>_**Your antivirus may also flag this program. Similarly, this is just because it is a relatively unknown file, and it connects to (and scans) the local network. Again, not harmful in any way.**_

If you don't trust me, feel free to inspect every line of code and compile it yourself.

## Permissions
Available under the MIT liscence.
Feel free to use this wherever and however you see fit. You can download the code, edit it, and compile it in Visual Studio 2017 if you want to. Any contributions here on github are very welcome. Make a pull request and I'll merge it as soon as I can. 

## Advantages
* By default uses high quality audio sampling and sending. No compression is used, since it is intended for LAN connections.
* Unlimited users and servers.
* Quite easy to use, even though the UI isn't very pretty.
* Good for LAN parties where people are separated by rooms and internet connection can't be used or isn't desirable.
* Very lightweight. All files required for execution total to under 1MB. Downloading the ZIP file will take seconds even on the slowest internet connection.

## Limitations & Problems
* Uncompressed, high sample rate audio may bog down slow networks. I intend to allow the server to change audio sampling rate in the future.
* Currently only supports LAN connections. The users will have to all be connected to the same WiFi network, or otherwise be connected directly.
* Sudden disconnections may not be handled correctly, leaving ghost users left behind in servers.
* Not extensively tested. May crash unexpectedly under certain conditions.

## Why the name?
I was bored when I started this project. I didn't have any clever names so this is the result. I may change it at some point in the future.

## Plans
If you have any suggestions, open an Issue [here](https://github.com/Epicguru/CChat2/issues) and tag it appropriately. I'm open to making changes or adding simple stuff if it makes sense.
* Improve the general UI.
* Attempt to use port forwarding to make the app work over the internet. So this thing will actually work as a chat client, such as skype or discord.
* Implement better error reporting.
* Add text chat, so that connected users can type and send out messages.
* Add an option to connect directly.
* Add optional passwords to servers.
* Add optional encryption to servers?
* Allow individual clients to both mute themselves and mute others. (currently only the host can do this)
* Add a few different volume controls. A main, master volume slider, and ideally individual sliders for each connected user. I also want to make it possible for users individual user's volumes to be 'boosted' because some people have really terrible mics.
* Once chat is implemented, allow users to share files? Files would be uploaded to the server and then distributed to all other clients. I could make it efficient enough to transmit huge files if I wanted to... Could be useful for LAN parties.
