# Image Comparison Test Project
This was a tech test created to support my application at Huboo

It compares images that could be extended for use in any test project.
Can be packaged into a nuget to be used in other projects.
Can be modified to turn into a small API to pass these in as paramaters to make it language agnostic.

The tests take these paramaters
- A base image (the image you want the test to take as the 'master image' your baseline)
- A comparison image (the image taken in the test)
- A location to save an output if the comparison throws up errors
- Optional an image mask, this can be used to ignore information on the screen that we know will always change and can be excluded from the comparison


Test 1
- A test that has differences
- An output file will be saved showing the difference
- The test compares [this base image](Image1_Base.png)
- Against [this comparison image](Image1_Comparison.png)
- As we have differences an output will be created showing the difference
 
Test 2
- A test that shows how a mask can work
- The test compares [this base image](Image2_Base.png)
- Against [this comparison image](Image2_Comparison.png)
- Using [this mask](Image2_Mask.png)
- We can compare only the bits we want to check against leaving any content we're not interested in out of the comparison

Test 3
- A test that has no differences
- The test compares [this base image](Image3_Base.png)
- Against [this comparison image](Image3_Comparison.png)
- As the image is the same, we will not create any differences