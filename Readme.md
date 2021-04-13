# Luxury Accessories

It is an online store of luxury clothing accessories, made by renowned designers and the most famous fashion houses.

## Table of contents
* [General info](#general-info)
* [Technology](#technology)
* [Delivery format](#delivery-format)
* [Configuration notes](#Configuration-notes)
* [License](#licence)

## General info

This application is designed for academic purposes. It is a web-based solution developed with ASP.net, C#, HTML CSS, JavaScript, and JQuery, complying with given requirements and restrictions.

## Technology

* Asp web form
* .Net Framework 4.6.1
* IDE VStudio 2017
* Sql Server 2014
* C#, Ajax, Jquery, Jscript
* N layers pattern
* Bootstrap CSS framework


## Delivery format

Source code in .Net Framework 4.6.1

## Configuration notes

-Database is embeded in App_Data Folder.
-Scripts are included in Databse_Scripts folder, in case you want to use remote or local server, just switch to the second conexion string.
-Some passwords are not encrypted for academic evaluation purposes.
-Configure into web.config file, enviroment variables such as email and paswword to send passwords verifications.
```xml
<add key="admEmail" value="" />    <!--For testing use your own email account-->
<add key="admEmapsw" value="" />    <!-- For testing use your own password of the email account -->
<add key="serverApp" value="https://localhost:44397/" />
<add key="serverReset" value="https://luxuryshop.somee.com/" />
```

## License
[Private use](https://choosealicense.com/appendix/#private-use)