# These are self-notes
# Getting Started

Download the Meta XR All-in-one from assets store, this contains the OVRCameraRig which will serve as the main camera and the players viewport.

Window > Project Packages

## Link for the Meta XR All-In-One SDK Documentation
https://developer.oculus.com/documentation/unity/

## Unity Documentation
https://docs.unity3d.com/ScriptReference/

# Add Runtime Controllers

These controllers will appear while the user is in play mode, makes it easier for the user to see where their controllers are positioned in the VR environment, a ray can also be emanating from these controllers for easier object selection.

In the hierarchy window:

- OVRCameraRig > TrackingSpace > LeftHandAnchor > LeftControllerAnchor

Look up "OVRRuntimeControllerPrefab" in the Project window, drag this under LeftControllerAnchor, do this similarly to the RightControllerAnchor.

We also added "OVRRayHelper" in order to see where the ray is emanating from, this also goes under LeftControllerAnchor and RightControllerAnchor

# Ray Intersection for Objects and taking input from controllers

## Information about ray casting and collision detection
https://docs.unity3d.com/ScriptReference/Physics.Raycast.html

## Tutorial about taking input from controllers
https://developer.oculus.com/documentation/unity/unity-tutorial-basic-controller-input/

## Execution Order of Lifecycle Functions - Very Important - Start(), Update(), etc...
https://docs.unity3d.com/Manual/ExecutionOrder.html

For drawing ray I created an empty GameObject and added the "Line Renderer" component, unchecked "Use World Position" so it uses the position relative to the GameObject it is attached to, I also attached a script called "RenderRay" so it updates the StartPosition and EndPosition of the ray as it intersects GameObjects.








