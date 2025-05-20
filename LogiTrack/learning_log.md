using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack
{
    class learning_log
    # info or my thoughts                       // draft
   
    {

     
     #this project will be hard on me i know from now but i want to think big this time.

     #it is a management system where crud is done on entities but: 
      the heart of the project is that there will be logging system that detects changes and save logs after successfully updating the database

     #the first question in my mind was : will i save changes that added locally or remotely? i wanted remotely because the updated database is my 
      final product and i am not making a logger as the developer's draft, decide and apply and then there will be something worth saving in my logger!

      #today i will structure the project well and may be build entities first, let's gooooooooooooooooooooooooo

      //well i have the entities folder of course with 3 or 4 entities: A company will need: Employee, Project, Department
        then will i have the tracking folder or what i don't know (cryingggggggggggggggggg)
        //i want to be a very good developer right now i can't
        okay,Contexts folder, CLASSIC
        Users folder, to tell in my log who did what ( keeping static now i think)
        //services filder and menus folder???  chatgpt recommended those as addition but i see i made a big progress


       #made baseEntity to avoid repetition and now i am thinking in the m2m between employee and proeject, manual or automatic by ICollection nsvigation property???
       
       #when i was dicussing with chatgpt about why do we have to configure explicit m2m relations iby fluent apis even if we did it using Navigation properties
        and it told me that is to ensure that ef make the relations as we want because navigation properties aren't the best option with explicit m2m joins
        gonna read about that today and tell you results tommorrow 
        ( if i has the time), 
        (no i will read about it , iam kidding), 
        ( we will see , you lazy)



        #i read about seeding abit and now i am refining my database entities(creating manager role for projects and departments)
        then i will start to seed data

        //for now i want to seed data manually but then i have to validate data inputs

        //i added one more [Employee-Project] relationship where i added a Role Enum and only employees marked with Manager can have one to one relation with a project

        ################# SEEDING

         Now that was a whole topic that i didn't know anthung about and didn't even know it exists
         it may sound easy idea, i just want to run the proejct with some preloaded data to work on
         well it wasn't that easy for me, after i wrote most of the logic i talked with chatgpt (as a senior developer) and he told me i need to 
           add conditions to check if tables are empty first to avoid trying to add already added values

        And here the fun part ended for me!

        Endless errors about Foreign keys and stuff. 
        It started with ef shouting that i am the one responsible for creating IDs and she had a right to shout
        Then i found myself in a circle of errors that don't dissapear whatever i did and it lasted 2 days!
        Finally i commented everything and started to seed one table at a time and it has 2 or 3 errors then resolved
        OMG
        The problem was related with dependent relationships and how can i as EF core to add project without an employee while actually if he 
          gave himself the chance and added the employee , he would find employees to assign projects to them!!!!

        Calm down! I will write more about seeding for reference in following projects in my new collected files. learn with me where i talk about 
         pieces of codes, designs, topics, and best practices that i learned along the way

        Now to playing with my database alittle bit, to CRUD ***********
         
        ################# CRUD
       






Add a new project to a department

Assign employee(s) to a project

Change role of an employee in a project

Soft-delete a project or employee

List all employees in a department/project





        

        Summary
Never assume identity column IDs before inserting — the DB decides.

Insert parent entities first, save changes.

Query saved entities to get real IDs.

Use those real IDs when inserting dependent entities (with foreign keys).
    }
}
