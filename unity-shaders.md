Shaders Overview

Shaders in Unity can be written in one of three different ways:

Surface Shaders

Surface Shaders are your best option if your shader needs to be affected by lights and shadows. Surface shaders make it easy to write complex shaders in a compact way - it’s a higher level of abstraction for interaction with Unity’s lighting pipeline. Most surface shaders automatically support both forward and deferred lighting. You write surface shaders in a couple of lines of Cg/HLSL and a lot more code gets auto-generated from that.

Do not use surface shaders if your shader is not doing anything with lights. For Image Effects or many special-effect shaders, surface shaders are a suboptimal option, since they will do a bunch of lighting calculations for no good reason!

Vertex and Fragment Shaders

Vertex and Fragment Shaders will be required, if your shader doesn’t need to interact with lighting, or if you need some very exotic effects that the surface shaders can’t handle. Shader programs written this way are the most flexible way to create the effect you need (even surface shaders are automatically converted to a bunch of vertex and fragment shaders), but that comes at a price: you have to write more code and it’s harder to make it interact with lighting. These shaders are written in Cg/HLSL as well.

Fixed Function Shaders

Fixed Function Shaders need to be written for old hardware that doesn’t support programmable shaders. You will probably want to write fixed function shaders as an n-th fallback to your fancy fragment or surface shaders, to make sure your game still renders something sensible when run on old hardware or simpler mobile platforms. Fixed function shaders are entirely written in a language called ShaderLab, which is similar to Microsoft’s .FX files or NVIDIA’s CgFX.