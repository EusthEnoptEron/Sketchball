#Sketchball

## Builds

Automated builds can be found here: http://meersi.ch/sketchball/

## Raw instruction

The   aim   of   the   project   is   to   implement   the   pin-ball-machine   game   (“flipper”)   on   the 
PC/Notebook. The issues of defining the contents and dimensions of the game area and the 
visual and acoustical effects are part of the project.

## Further description based on Dev. Team 

As the raw instructions are quite broad the development team has decided to sit together and 
work out a more specific shape of the project. It has been concluded that the perspective of the 
game should be similar to “hanging above the flipper” and looking at it such as the centre of the 
view hits the flipper in a 90° angle. The general style will be sketch like which gives the game a unique face while allowing fast development. The graphical parts will most likely be mainly hand drawn. There will be two main windows. The default window is the game interface.

The user can start a new game and play as he wishes. Over a menu option he can switch to the 
editor window which allows to define a custom Pinball-machine with given elements. As a 
minimal requirements there will be sling-shoots, Flippers, Holes, a high score screen, Bumpers 
and a launching pad. He can drag and drop those elements on the game panel as long as there 
are no overlaps. The launching pad can only be dropped on specific positions. Each element 
can be selected and has properties that can be changed. The main flipper itself has properties 
as well like slope, gravity  or dimensions. The panel on which the elements can be put on is a 
rectangle with rounded top corners. When the ball rolls around he should follow the laws of 
physics (based on slope and collisions).

## Other Requirements and Constraints

Make use of UML, and any of the following programming languages: C/C++/C#/Java.

For development  the team  should   make   use   of   the  agile   up   approach   especially  of   their 
artefacts (Vision, Use Case, Software development plan, Domain model, DCD).

The development Team will use this artefacts as agile as possible, which means facing hight 
risks early and spare on unnecessary documentation (for example just brief UC format for Use 
Cases that appear simple).

As a performance requirement it can be said that 30 fps should be achieved or in other words 
the performance should allow fluid game play.
