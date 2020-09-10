# MonoAmp
Windows\Mono EXE\Service For Monoprice Multizone AMP.
The Service Hosts an mobile iPhone\Android Web App and Webapi to allow remote control of the Amp Connected via RS-232\USB. 
AMP also marketed under Monopice 6 zone amp, Monoprice 4 zone amp, Factor V-66, MAP-1200HD

Description of EXE's

AmpConfig:
Used to set the number of Amps connected and the Rs232, HTTP and WebAPI Ports for use with Either AmpMixer or AmpServer. Config needs to run as an admin inorder to open the HTTP Namespace URL’s.(One API and one WEB) (3rd Party NameSpace Manager still included for verification or maintenance)


AmpMixer.exe
Full GUI realtime Mixer to completely control the Monoprice amp locally and allows for Web control via mobile webapp on configured ports.


AmpServer.exe
Allows for Non GUI via mobile web app as either Task Bar or Windows Service. Amp Server when ran as administrator will allow you to configure itself as a windows service allowing it to run prior to login. When ran as a normal user it will run as a process which can be minimized to the task tray.

Note: In Order to get the "Install\Uninstall Service" options, you must run AmpServer as an Admin, after you can run with normal rights to Start\Stop or monitor the service depending on the non admin users rights.


Note: If your running the Service then you can't run the Mixer at the same time because they both are using the same Rs232\IP Ports. The next version will try to integrate the mixer by having it talk to the service instead of directly.


For mobile clients like iPhone or android:
Launch with http://127.0.0.1:50230/Default.html(or the Servers IP) with either the Process\Service\or Mixer running.


If you run it remotely (which is typical), you need to change the IP address inside the Web\Default.html file with Notepad to the Configured webapi address.

Edit line to point to public WebApi Port

var ip = 'http://127.0.0.1:50231'


WINAPI Self Host Commands:

Example:
http://localhost:50231/api/Command?unit=1&channel=4&command=Power&value=1

Commands: Public_Address,   Power,  Mute,   Do_Not_Disturb,  Volume,  Treble,  Bass,  Balance,  Source, Connected
Property:"MU,VO,BS,TR,CH,BL,PR"

http://localhost:50230/api/keypad?chan=3
{"BL":10,"BS":10,"CH":3,"DT":0,"ID":12,"LS":1,"MU":0,"Name":"Keypad 14","PA":0,"PR":0,"TR":11,"VO":17}

  http://localhost:50231/api/Value?Channel=1&command=Volume&value=10
  http://localhost:50231/api/Value?Channel=1&Property=VO&value=10

  http://localhost:50231/api/ValueUp?Channel=1&command=Volume
  http://localhost:50231/api/ValueUp?Channel=1&Property=VO

  http://localhost:50231/api/ValueDn?Channel=1&command=Volume
  http://localhost:50231/api/ValueDn?Channel=1&Property=VO

New  Commands for ALL Keypads
  http://localhost:50231/api/Command?command=Power&value=0

Older (still active Query)
http://localhost:50231/api/keypad?chan=2

Older (probably going to get rid of UNIT Based commands)
http://localhost:50231/api/Command?unit=1&channel=1&command=Volume&value=10


Regards,
Mike Pisano
