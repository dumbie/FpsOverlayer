using ArnoldVinkCode;
using ArnoldVinkStyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using static FpsOverlayer.AppVariables;
using static LibraryShared.Classes;

namespace FpsOverlayer
{
    public partial class WindowStats
    {
        //Update render api text
        void UpdateRenderApiText(RenderApiDetails renderApiDetails)
        {
            try
            {
                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    //Update render api
                    string stringRenderApi = string.Empty;
                    if (vSettings.Load("RendererShowApi", typeof(bool)))
                    {
                        if (!renderApiDetails.RenderingUI)
                        {
                            stringRenderApi = renderApiDetails.ApiName3D;
                        }
                    }

                    //Set render api text
                    if (string.IsNullOrWhiteSpace(stringRenderApi))
                    {
                        stackpanel_CurrentRenderer.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        string stringDisplay = AVFunctions.StringRemoveStart(vTitleREN + " " + stringRenderApi, " ");
                        textblock_CurrentRenderer.Text = stringDisplay;
                        stackpanel_CurrentRenderer.Visibility = Visibility.Visible;
                    }
                });
            }
            catch { }
        }

        //Get render api
        public static RenderApiDetails GetRenderApi(AVProcess targetProcess)
        {
            RenderApiDetails renderApiDetails = new RenderApiDetails();
            try
            {
                //Fix what if rendering is done in child process?
                //Fix find way to separate frontend from backend renderer
                //Fix find reliable way to determine if process renders 3D or UI

                //Check rendering api settings
                if (!vSettings.Load("RendererShowApi", typeof(bool)) && !vSettings.Load("AppShow3dOnly", typeof(bool)))
                {
                    return renderApiDetails;
                }

                //Get process modules
                List<string> processModules = targetProcess.Modules;

                //Anti cheat blocks access to modules so assume render api is used
                if (!processModules.Any())
                {
                    return renderApiDetails;
                }

                //Render variables
                List<string> renderApiNames = new List<string>();
                List<string> renderApiDirectX = new List<string>();
                bool apiFound3D = false;
                int apiCount3D = 0;

                //Lower and trim module names
                List<string> modules = processModules.Select(m => m.ToLower().Trim()).ToList();
                //foreach (string module in modules)
                //{
                //    AVDebug.WriteLine("Module file: " + module);
                //}

                //Glide (3DFX)
                string[] dllGlide = ["glide2x.dll", "glide3x.dll", "voodoo2a.dll", "voodoo2z.dll"];
                if (dllGlide.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("Glide");
                }

                //Mantle
                string[] dllMantle = ["mantle64.dll", "mantle32.dll"];
                if (dllMantle.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    renderApiNames.Add("Mantle");
                }

                //Vulkan
                string[] dllVulkan = ["vulkan-1.dll", "vulkan_lvp.dll"];
                if (dllVulkan.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("Vulkan");
                }

                //OpenGL (System)
                //string[] dllOpenGL = ["opengl.dll", "opengl32.dll", "opengl1z.dll", "opengl3z.dll"]; //glu32.dll, glew32.dll, opengl32sw.dll
                //if (dllOpenGL.Any(x => modules.Contains(x)))
                //{
                //    apiCount3D++;
                //    apiFound3D = true;
                //}

                //OpenGL ES
                string[] dllOpenGlEs = ["libegl.dll", "libglesv2.dll", "glfw3.dll"];
                bool foundOpenGlEs = dllOpenGlEs.Any(x => modules.Contains(x));
                if (foundOpenGlEs)
                {
                    apiCount3D++;
                    apiFound3D = true;
                }

                //OpenGL (Vendor)
                //Note: opengl32.dll gets loaded on pretty much every 3D application using this as workaround
                //bool dllOpenglVirtual = modules.Any(x => Regex.IsMatch(x, @"vm3dgl.*..dll"));
                bool dllOpenglAMD = modules.Any(x => Regex.IsMatch(x, @"atio.*.xx.dll")); //atig.*.pxx.dll
                bool dllOpenglNvidia = modules.Any(x => Regex.IsMatch(x, @"nvoglv.*..dll"));
                bool dllOpenglIntel = modules.Any(x => Regex.IsMatch(x, @"ig.*.icd.*..dll"));
                if (dllOpenglAMD || dllOpenglNvidia || dllOpenglIntel)
                {
                    apiCount3D++;
                    apiFound3D = true;
                    if (foundOpenGlEs)
                    {
                        renderApiNames.Add("OpenGL ES");
                    }
                    else
                    {
                        renderApiNames.Add("OpenGL");
                    }
                }

                //WebGPU
                string[] dllWebGpu = ["wgpu_native.dll", "webgpu_dawn.dll"];
                if (dllWebGpu.Any(x => modules.Contains(x)))
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

                //DirectX 1 to 7
                string[] dllDirectX1to7 = ["d3dim.dll", "d3drm.dll", "ddraw.dll"];
                if (dllDirectX1to7.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiDirectX.Add("1-7");
                }

                //DirectX 8
                string[] dllDirectX8 = ["d3d8.dll", "d3d8thk.dll"];
                if (dllDirectX8.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiDirectX.Add("8");
                }

                //DirectX 9
                string[] dllDirectX9 = ["d3d9.dll"]; //d3d9on12.dll
                if (dllDirectX9.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiDirectX.Add("9");
                }

                //DirectX 10
                string[] dllDirectX10 = ["d3d10.dll", "d3d10_1.dll"]; //d3d10core.dll, d3d10_1core.dll, d3d10level9.dll, d3d10warp.dll
                if (dllDirectX10.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiDirectX.Add("10");
                }

                //DirectX 11
                string[] dllDirectX11 = ["d3d11.dll"]; //d3d11on12.dll
                if (dllDirectX11.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiDirectX.Add("11");
                }

                //DirectX 12
                string[] dllDirectX12 = ["d3d12.dll"]; //d3d12core.dll
                if (dllDirectX12.Any(x => modules.Contains(x)))
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
                string[] dllAmdFsr = ["amd_fidelityfx_vk.dll", "amd_fidelityfx_dx12.dll", "amd_fidelityfx_upscaler_dx12.dll", "amd_fidelityfx_framegeneration_dx12.dll", "amdxcffx64.dll"];
                if (dllAmdFsr.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("FSR");
                }

                //Intel XeSS
                string[] dllIntelXess = ["libxess.dll", "libxess_dx11.dll", "libxess_fg.dll"];
                if (dllIntelXess.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("XeSS");
                }

                //nVidia DLSS
                string[] dllNvidiaDlss = ["nvngx_dlss.dll", "nvngx_dlssg.dll"];
                if (dllNvidiaDlss.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("DLSS");
                }

                //nVidia OptiX
                string[] dllNvidiaOptix = ["nvoptix.dll", "optix.1.dll"];
                if (dllNvidiaOptix.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("OptiX");
                }

                //nVidia PhysX
                string[] dllNvidiaPhysx = ["physxloader.dll", "physxloader64.dll", "physxdevice.dll", "physxdevice64.dll", "physx3_x86.dll", "physx3_x64.dll", "physx3common_x86.dll", "physx3common_x64.dll"];
                if (dllNvidiaPhysx.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("PhysX");
                }

                //nVidia HairWorks
                if (modules.Any(x => Regex.IsMatch(x, @".*.hairworks.*.win.*..dll")))
                {
                    apiCount3D++;
                    apiFound3D = true;
                    renderApiNames.Add("HairWorks");
                }

                //Unity Player / IL2CPP
                string[] dllUnity = ["unityplayer.dll", "baselib.dll"];
                if (dllUnity.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                }

                //Simple DirectMedia Layer
                string[] dllSdl = ["sdl.dll", "sdl2.dll", "sdl3.dll"];
                if (dllSdl.Any(x => modules.Contains(x)))
                {
                    apiCount3D++;
                    apiFound3D = true;
                }

                //Gaming Title Callable UI
                if (modules.Contains("gamingtcui.dll"))
                {
                    apiCount3D++;
                    apiFound3D = true;
                }

                //Graphics Device Interface
                bool renderGDI = false;
                string[] dllGDI = ["gdiplus.dll", "gdi32.dll", "gdi32full.dll"];
                if (dllGDI.Any(x => modules.Contains(x)))
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
                if (!apiFound3D || (renderWindowsUI && apiCount3D <= 1) || (renderGDI && renderDirectWrite && renderDirectXCore && renderDXGI && apiCount3D <= 1))
                {
                    renderApiDetails.RenderingUI = true;
                }

                //Combine render string
                renderApiDetails.ApiName3D = string.Join(", ", renderApiNames);

                //Return result
                AVDebug.WriteLine("Rendering api: 3D " + apiFound3D + " (" + apiCount3D + "x) / UI " + renderApiDetails.RenderingUI + " / " + renderApiDetails.ApiName3D);
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