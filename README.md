# Physical-Structure
Arduous and currently inaccurate modeling of a physical structure composed of nodes.


Physical Structure is composed of Physical Nodes, where each Physical Node contains its connections to other PNs, distances to those other PNs, angles to those other PNs, and its own tensile, compressive, and shear strength. All PN properties are currently dictated by the programmer.

Truss is a Physical Structure Object with a *lot* of constants to make the future coder's life easier (and have lines that are 4x shorter due to how many characters it takes to instantiate a Tuple with only two items...). I also included a NodeSize property, of which I am not sure if I should add to the Physical Node class (and hence make *every* PN have it). I could have put the constants in a different file but it's hard looking for a file in a haystack simply to see how a constant was implemented.

In the future, I plan on implementing a sliding bar for strengths and colored structural failures in an interactive GUI like West Point Bridge Designer. I plan on using Physical Structures to create a Grain class (imagine magnetic grains in Iron). I also plan on adding Size (NodeSize mentioned in prev para) and MagneticReceptance (how much a magnet affects an individual PN) properties to individual PNs. Useful suggestions are welcome!

**WARNING**: DO NOT USE THIS CODE FOR ANYTHING IMPORTANT AS IT IS NOT ENTIRELY TESTED AND BENCHMARKED. I NEED TO ADD CORRECT ANGLE AND DIRECTION FUNCTIONALITY.

THIS PROJECT IS A WORK IN PROGRESS.
