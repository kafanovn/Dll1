#pragma once

#ifdef MATHLIBRARY_EXPORTS
#define MATHLIBRARY_API __declspec(dllexport)
#else
#define MATHLIBRARY_API __declspec(dllimport)
#endif
extern "C" __declspec(dllimport) void vms_exp(int n, float a[], float y[], float result[], float& max_rel, float& max_pos, float& max_value);
extern "C" __declspec(dllimport) void vmd_exp(int n, double a[], double y[], double result[], double& max_rel, double& max_pos, double& max_value);

