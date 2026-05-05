using ArnoldVinkCode;
using System;
using System.Collections.Generic;
using System.Linq;
using static LibraryShared.Classes;

namespace FpsOverlayer
{
    public partial class WindowStats
    {
        public static RenderApiDetails GetRenderApi(AVProcess targetProcess)
        {
            RenderApiDetails renderApiDetails = new RenderApiDetails();
            try
            {
                //Fix opengl32.dll is loaded in pretty much every application
                //Fix find way to separate frontend from backend renderer
                //Fix find reliable way to determine if process renders 3D or UI

                //Render variables
                List<string> renderApiNames = new List<string>();
                List<string> renderApiDirectX = new List<string>();
                bool apiFound3D = false;
                int apiCount3D = 0;

                //Get process modules
                List<string> processModules = targetProcess.Modules;

                //Anti cheat blocks access to modules so assume render api is used
                if (!processModules.Any())
                {
                    renderApiDetails.RenderingUI = false;
                    return renderApiDetails;
                }

                //Lower and trim module names
                List<string> modules = processModules.Select(m => m.ToLower().Trim()).ToList();
                foreach (string module in modules)
                {
                    AVDebug.WriteLine("Module file: " + module);
                }

                //Glide (3DFX)
                if (modules.Contains("glide2x.dll") || modules.Contains("glide3x.dll") || modules.Contains("voodoo2a.dll") || modules.Contains("voodoo2z.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("Glide");
                }

                //Mantle
                if (modules.Contains("mantle64.dll") || modules.Contains("mantle32.dll"))
                {
                    apiCount3D++;
                    renderApiNames.Add("Mantle");
                }

                //Vulkan
                if (modules.Contains("vulkan-1.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("Vulkan");
                }

                //OpenGL
                if (modules.Contains("opengl.dll") || modules.Contains("opengl32.dll") || modules.Contains("opengl1z.dll") || modules.Contains("opengl3z.dll")) //glu32.dll, glew32.dll
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("OpenGL");
                }

                //OpenGL ES
                if (modules.Contains("libegl.dll") || modules.Contains("libglesv2.dll") || modules.Contains("glfw3.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("OpenGL ES");
                }

                //WebGPU
                if (modules.Contains("wgpu_native.dll") || modules.Contains("webgpu_dawn.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("WebGPU");
                }

                ////DirectX 2D1
                //if (modules.Contains("d2d1.dll"))
                //{
                //    apiCount3D++;
                //    apiFound3D = true;
                //    renderApiDirectX.Add("2D1");
                //}

                //DirectX 1–7
                if (modules.Contains("ddraw.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiDirectX.Add("1-7");
                }

                //DirectX 8
                if (modules.Contains("d3d8.dll")) //d3d8thk.dll
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiDirectX.Add("8");
                }

                //DirectX 9
                if (modules.Contains("d3d9.dll")) //d3d9on12.dll
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiDirectX.Add("9");
                }

                //DirectX 10
                if (modules.Contains("d3d10.dll") || modules.Contains("d3d10_1.dll")) //d3d10core.dll, d3d10_1core.dll, d3d10level9.dll, d3d10warp.dll
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiDirectX.Add("10");
                }

                //DirectX 11
                if (modules.Contains("d3d11.dll")) //d3d11on12.dll
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiDirectX.Add("11");
                }

                //DirectX 12
                if (modules.Contains("d3d12.dll")) //d3d12core.dll
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiDirectX.Add("12");
                }

                //DirectX Shader Cache
                if (modules.Contains("d3dscache.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                }

                //Combine DirectX string
                if (renderApiDirectX.Any())
                {
                    //renderApiNames.Add("DirectX " + string.Join(" ", renderApiDirectX));
                    renderApiNames.Add("DirectX");
                }

                //AMD FSR
                if (modules.Contains("amd_fidelityfx_dx12.dll") || modules.Contains("amdxcffx64.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("FSR");
                }

                //Intel XeSS
                if (modules.Contains("libxess.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("XeSS");
                }

                //nVidia DLSS
                if (modules.Contains("nvngx_dlss.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("DLSS");
                }

                //nVidia OptiX
                if (modules.Contains("nvoptix.dll") || modules.Contains("optix.1.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("OptiX");
                }

                //nVidia PhysX
                if (modules.Contains("physxloader.dll") || modules.Contains("physxloader64.dll") || modules.Contains("physxdevice.dll") || modules.Contains("physxdevice64.dll") || modules.Contains("physx3_x86.dll") || modules.Contains("physx3_x64.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("PhysX");
                }

                //Graphics Device Interface
                bool renderGDI = false;
                if (modules.Contains("gdiplus.dll") || modules.Contains("gdi32.dll") || modules.Contains("gdi32full.dll"))
                {
                    renderGDI = true;
                }

                //DirectWrite
                bool renderDirectWrite = false;
                if (modules.Contains("dwrite.dll"))
                {
                    renderDirectWrite = true;
                }

                //DirectX Core
                bool renderDirectXCore = false;
                if (modules.Contains("dxcore.dll"))
                {
                    renderDirectXCore = true;
                }

                //DirectX Graphics Infrastructure
                bool renderDXGI = false;
                if (modules.Contains("dxgi.dll"))
                {
                    renderDXGI = true;
                }

                //Windows Immersive User Interface
                bool renderWindowsUI = false;
                if (modules.Contains("windows.ui.immersive.dll"))
                {
                    renderWindowsUI = true;
                }

                //Check if rendering user interface
                if (!apiFound3D || renderWindowsUI || (renderGDI && renderDirectWrite && renderDirectXCore && renderDXGI && apiCount3D <= 1))
                {
                    renderApiDetails.RenderingUI = true;
                }

                //Combine render string
                renderApiDetails.ApiName3D = string.Join(", ", renderApiNames);

                //Return result
                AVDebug.WriteLine("Rendering api: 3D " + apiFound3D + " / UI " + renderApiDetails.RenderingUI + " / " + renderApiDetails.ApiName3D);
                return renderApiDetails;
            }
            catch (Exception ex)
            {
                AVDebug.WriteLine("Failed to get render api: " + ex.Message);
                return renderApiDetails;
            }
        }
    }
}