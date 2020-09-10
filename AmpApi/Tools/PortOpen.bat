@echo off
rem Httpcfg query iplisten
rem httpcfg query urlacl
rem httpcfg set urlacl /u http://127.0.0.1:50230/api/ /a "D:(A;;GX;;;WD)"
rem Httpcfg set iplisten -i 127.0.0.1:50230
rem Httpcfg delete iplisten -i 127.0.0.1:50230
rem Httpcfg delete urlacl /u http://127.0.0.1:50230/api/


httpcfg set urlacl /u http://+:50230/ /a "D:(A;;GX;;;WD)"
httpcfg query urlacl
pause