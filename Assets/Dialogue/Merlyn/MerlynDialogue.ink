Hello there! #speaker:Marlyn #portrait:NPC_GIRL_2 #layout:right
-> main

=== main ===
Did you hear about the legend of dragons?
+ [Yeah, i hear it!]
    .........
    For me its so scary though, look you must be careful out there when you saw one!
+ [Nope, I didn't hear it!]
    Let me tell you, there is some legend dragon out there thats look scary, so you must be careful until you saw one!
+ [Not interested!]
    Well its up to you, but you must be careful until you saw one!
    -> END

- I already warn yoou!!
-> more

=== more ===
Well, do you have any more questions?
+ [Yes]
    -> main
+ [No]
     Goodbye then!
     -> END
+ [Goodspeed]
    Otay!
    -> END