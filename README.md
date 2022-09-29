* [Introduction](#introduction)
* [General requirements](#general-requirements)
* [Instruction to launch](#instruction-to-launch)
* [Notes](#notes)
* [Warnings and other admonitions](#warnings-and-other-admonitions)

Here you can see all the endpoints - https://holiday-api-service.herokuapp.com/swagger/index.html

# Introduction
This project was created as task for applying Mediapark company.
# General requirements
* .NET 6
* Docker

# Instruction to launch
 ```
 docker build -t HolidayApi .
 
 docker run -d -p 8080:80 --name myapp holidayapi
 ```
 Visit [localhost](http://localhost:8080/swagger/index.html)
 
# Notes

# Warnings and other admonitions 
	
