# Luxury Accessories

It is an online store of luxury clothing accessories, made by renowned designers and the most famous fashion houses.

## Table of contents
* [General info](#general-info)
* [Technology](#technology)
* [Delivery format](#delivery-format)
* [Configuration notes](#Configuration-notes)
* [Demo site](#demo-site)
* [Scope of development](#scope-of-development)
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

* Database is embeded in App_Data Folder.
* Scripts are included in Databse_Scripts folder, in case you want to use remote or local server, just switch to the second conexion string.
* Some passwords are not encrypted and others do using the SHA-512 secure hashing algorithm for academic evaluation purposes.
* Configure into web.config file, enviroment variables such as email and paswword to send passwords verifications.
```xml
<add key="admEmail" value="" />    <!--For testing use your own email account-->
<add key="admEmapsw" value="" />    <!-- For testing use your own password of the email account -->
<add key="serverApp" value="https://localhost:44397/" />
<add key="serverReset" value="https://luxuryshop.somee.com/" />
```

## Demo site

[![Luxury Accesories](https://luxuryshop.somee.com/assets/Demo.jpg "The most fashionable accessories.")](https://luxuryshop.somee.com/index.aspx)

## Scope of development
><h3>Client web site:</h3> 
>*Home<br/>
>*Product search and selection <br/>
>*Shopping cart management<br/>
>*Orders management and Invoice<br/>
>*Security module: Login, Register, Reset Password<br/>
>*About info
>*Contact info
##
><h3>Manager module:</h3> 
>*Home<br/>
>*User and customer management<br/>
>*Category management<br/>
>*Product management<br/>
>*Orders management and Invoice<br/>
>*Security module: Login, Register, Reset Password<br/>

## License
[Private use](https://choosealicense.com/appendix/#private-use)
