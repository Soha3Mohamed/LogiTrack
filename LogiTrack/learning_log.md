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





    }
}
