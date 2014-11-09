## Shader Languages Unity Can use

### Shaderlab
 * Unity's own shader language
 * Required in all unity shaders

### Cg
 * Stands for "C for graphics"
 * Deprecated language
 * Can compile to Direct X or GLSL

### GLSL
 * OpenGL Shader Langauge
 * Unity advices that you only use GLSL if you are testing for OS X or Mobile devices. Because Unity will cross compile Cg/HLSL into GLSL when on the platforms that need it.
   * You can force Unity to cross complile glsl on all platforms with #pragma glsl

## Unity Shaders Overview

Shaders in Unity can be written in one of three different ways:

### Surface Shaders

Surface Shaders are your best option if your shader needs to be affected by lights and shadows. Surface shaders make it easy to write complex shaders in a compact way - it’s a higher level of abstraction for interaction with Unity’s lighting pipeline. Most surface shaders automatically support both forward and deferred lighting. You write surface shaders in a couple of lines of Cg/HLSL and a lot more code gets auto-generated from that.

Do not use surface shaders if your shader is not doing anything with lights. For Image Effects or many special-effect shaders, surface shaders are a suboptimal option, since they will do a bunch of lighting calculations for no good reason!

These shaders are expensive to render.

### Vertex and Fragment Shaders

Vertex and Fragment Shaders will be required, if your shader doesn’t need to interact with lighting, or if you need some very exotic effects that the surface shaders can’t handle. Shader programs written this way are the most flexible way to create the effect you need (even surface shaders are automatically converted to a bunch of vertex and fragment shaders), but that comes at a price: you have to write more code and it’s harder to make it interact with lighting. These shaders are written in Cg/HLSL as well.

These are the normal kind of shaders that I am familiar with for graphics programming.

### Fixed Function Shaders

Fixed Function Shaders need to be written for old hardware that doesn’t support programmable shaders. You will probably want to write fixed function shaders as an n-th fallback to your fancy fragment or surface shaders, to make sure your game still renders something sensible when run on old hardware or simpler mobile platforms. Fixed function shaders are entirely written in a language called ShaderLab, which is similar to Microsoft’s .FX files or NVIDIA’s CgFX.

## Vertex/Fragment shaders in Unity

When writing the vertex/fragment kind of shader in Unity there is an Interface part that handles everything that connects your shader to Unity. The Interface part also has Properties so you can change the shader from the unity UI.

And of course there will be a Vertex Shader part and a Fragment Shader part

## Precision of computations

When writing shaders in Cg/HLSL, there are three basic number types: float, half and fixed (as well as vector & matrix variants of them, e.g. half3 and float4x4):

 * float: high precision floating point. Generally 32 bits, just like float type in regular programming languages.
 * half: medium precision floating point. Generally 16 bits, with a range of –60000 to +60000 and 3.3 decimal digits of precision.
 * fixed: low precision fixed point. Generally 11 bits, with a range of –2.0 to +2.0 and 1/256th precision.
Use lowest precision that is possible; this is especially important on mobile platforms like iOS and Android. Good rules of thumb are:

For colors and unit length vectors, use fixed.
For others, use half if range and precision is fine; otherwise use float.
On mobile platforms, the key is to ensure as much as possible stays in low precision in the fragment shader. On most mobile GPUs, applying swizzles to low precision (fixed/lowp) types is costly; converting between fixed/lowp and higher precision types is quite costly as well.

## SubShader
Each Unity Shader will have a list of SubShaders and when the game runs Unity will pick one of the SubShaders to use based on which platform the game is currently running on (Android, Xbox, PC, etc).

## Pass
A SubShader can have multiple passes, so that the object the shader is on will render once for each pass in the SubShader.
